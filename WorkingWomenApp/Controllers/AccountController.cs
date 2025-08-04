using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkingWomenApp.BLL.UnitOfWork;
using WorkingWomenApp.Database.DTOs.UserDtos;
using WorkingWomenApp.Database.enums;
using WorkingWomenApp.Database.Models.Users;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace WorkingWomenApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<SecurityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            RoleManager<SecurityRole> roleManager,
            SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUrl = null)
        {

            returnUrl ??= Url.Content("~/");

            LoginDto loginVM = new()
            {
                RedirectUrl = returnUrl
            };

            return View(loginVM);
            //return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginVM)
        {
            if (ModelState.IsValid)
            {
               
                //var applicationUser = _unitOfWork.UserRepository.Set<ApplicationUser>().Where(r => r.Email == loginVM.Email)
                //    .FirstOrDefault();
                var user = await _userManager.FindByEmailAsync(loginVM.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, true);


                    if (result.Succeeded)
                    {
                        
                        if (string.IsNullOrEmpty(loginVM.RedirectUrl))
                        {
                            return RedirectToAction("Dashboard", "Home");
                            //return Redirect($"Home/Dashboard");
                        }
                        else
                        {
                            return LocalRedirect(loginVM.RedirectUrl);
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                    }
                }
            }

            return View(loginVM);
        }

        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateDto registerVM)
        {
            
            ApplicationUser user = new()
            {
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
                NormalizedEmail = registerVM.Email.ToUpper(),
                EmailConfirmed = true,
                UserName = registerVM.Email,
                PassWord = registerVM.Password
            };

            var result = await _userManager.CreateAsync(user, registerVM.Password);

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "Woman").Wait();
             

                await _signInManager.SignInAsync(user, isPersistent: false);
                if (string.IsNullOrEmpty(registerVM.RedirectUrl))
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    return LocalRedirect(registerVM.RedirectUrl);
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(registerVM);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
