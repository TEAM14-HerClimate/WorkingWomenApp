
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Options;

using WorkingWomenApp.Database.Core.CustomEntities;
using WorkingWomenApp.Database.Models.Users;
using WorkingWomenApp.BLL.Interfaces;
using WorkingWomenApp.BLL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkingWomenApp.Database.DTOs.UserDtos;
using WorkingWomenApp.Database.enums;

namespace WorkingWomenApp.BLL.Implementation
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(IUnitOfWork unitOfWork, IOptions<PaginationOptions>options, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
            _userManager = userManager;
        }
        public async Task<bool> ChangePasswordAsync(ChangePasswordDto model)
        {
            try
            {
                //validation
                if (string.Compare(model.NewPassword, model.ConfirmPassword) != 0)
                {
                    throw new Exception("The Two New Passwords MUST match!");
                }

                var dbUser = _unitOfWork.UserRepository.Set<ApplicationUser>().Find(model.UserId);
                if (dbUser == null)
                {
                    throw new Exception("User Not Found!");
                }
               

                IdentityResult result = null;
                if (model.isSuperUser)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(dbUser);

                    result = await _userManager.ResetPasswordAsync(dbUser, token, model.NewPassword);
                }
                else
                {

                    result = await _userManager.ChangePasswordAsync(dbUser, model.CurrentPassword, model.NewPassword);
                }

                if (result.Succeeded)
                {

                }
                else
                {

                    throw new Exception($"Password Change Error(s): {string.Join(",", result.Errors.Select(r => $"{r.Code}: {r.Description}"))}");
                }


                return true;
            }
            catch (Exception e)
            {
                model.ErrorMessage = e.Message;
            }

            return false;
        }

        public async Task<(bool, string)> AddUser(ApplicationUser model) //
        {
            string errorMessage = null;
            bool success = false;


            try
            {
               
                    var dbSet = await _unitOfWork.UserRepository.Set<ApplicationUser>().Where(r => (r.UserName == model.UserName || r.Email == model.Email)).FirstOrDefaultAsync();
                    if (dbSet != null)
                    {
                        dbSet.EmailConfirmed = true;
                        await _unitOfWork.SaveChangesAsync();
                        success = true;
                    }
                    else
                    {
                        errorMessage = "The Code entered  is wrong, please check it first.";
                    }

              

            }
            catch (Exception e)
            {
                errorMessage = e.Message.ToString();
            }

                  return (success, errorMessage);
        }



    }
}
