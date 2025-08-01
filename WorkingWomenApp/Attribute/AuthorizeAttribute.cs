using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Net;
using System.Text;
using WorkingWomenApp.BLL.UnitOfWork;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.Attribute
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeTestAttribute : System.Attribute, IAuthorizationFilter
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorizeTestAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (ApplicationUser)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }

        public void OnAPIAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var authorization = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                if (authorization == null)
                {
                    throw new Exception("Authorization Header Missing!");
                }

                var parameters = Encoding.UTF8.GetString(Convert.FromBase64String(authorization.Split(' ').Last())).Split(':');

                if (parameters.Length != 2)
                {
                    throw new Exception("Invalid Credentials Parameters!");
                }

                var _signInManager = (SignInManager<ApplicationUser>)context.HttpContext.RequestServices.GetService(typeof(SignInManager<ApplicationUser>));
                var _userManager = (UserManager<ApplicationUser>)context.HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));

                var dbUserTask = _userManager.FindByNameAsync(parameters[0]);

                Task.WaitAll(dbUserTask);

                var result = _signInManager.PasswordSignInAsync(dbUserTask.Result?.UserName ?? parameters[0], parameters[1], false, true);

                Task.WaitAll(result);

                if (result.Result.Succeeded)
                {
                    var username = parameters[0];
                    var dbUser = dbUserTask.Result;



                }
                else if (result.Result.IsLockedOut)
                {
                    throw new Exception("Your Account has been Locked Out!");
                }
                else
                {
                    throw new Exception("Invalid UserName Or Password!");
                }
            }
            catch (Exception e)
            {
                context.Result = ControllerBaseHelper.GenerateContentResult(HttpStatusCode.Unauthorized, e.ExtractInnerExceptionMessage(), PromiseCRMResponseType.PlainText);
            }
        }
    }

}
