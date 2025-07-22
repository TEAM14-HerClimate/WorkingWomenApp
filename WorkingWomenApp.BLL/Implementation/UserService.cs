using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingWomenApp.BLL.Interfaces;
using WorkingWomenApp.BLL.UnitOfWork;
using WorkingWomenApp.Data;
using WorkingWomenApp.Database.DTOs;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.BLL.Implementation
{
  
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;

    


       

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
           

            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Register(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);

            try
            {
                if (user != null)
                {
                    return false;
                }

                IdentityResult identityResult = await _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = userName,
                    Email = $"{userName}@test.com"
                }, password);

                return identityResult.Succeeded;
            }
            catch (Exception ex)
            {
              

                return false;
            }
        }

        public async Task<bool> Login(string userName, string password)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(userName, password, false, false);

            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }


            
    
    }
}
