using Microsoft.AspNetCore.Identity;
using WorkingWomenApp.Database.Models.Users;
using WorkingWomenApp.Database.Constants;

namespace WorkingWomenApp.Database.SeedData
{
    public class DataIntialiser
    {
         public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<SecurityRole> roleManager)
            {
                SeedRoles(roleManager);
                SeedUsers(userManager);
            }
        private static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
           
            if (userManager.FindByNameAsync(Constants.Constants.SuperUserName).Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.Id = Guid.NewGuid();
                user.UserName = Constants.Constants.SuperUserName;
                user.FirstName = "Super";
                user.LastName = "Admin";
                user.Email = "monicaiyb+1@gmail.com";
                user.EmailConfirmed = true;
                user.IsSuperUser = true;
                user.PassWord = "Cl1mateH3r?";

                IdentityResult result = userManager.CreateAsync(user, user.PassWord).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.Constants.SuperRoleName).Wait();
                }
                else
                {
                    throw new Exception($"Default User Creation Error(s): {string.Join(",", result.Errors.Select(r => $"{r.Code}: {r.Description}"))}");
                }
            }
        }

        private static void SeedRoles(RoleManager<SecurityRole> roleManager)
        {
            
            if (!roleManager.RoleExistsAsync(Constants.Constants.HerClimateAdministrator).Result)
            {
                SecurityRole role = new SecurityRole(Constants.Constants.HerClimateAdministrator);
                role.Id = Guid.NewGuid();
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;

                if (!roleResult.Succeeded)
                {

                    throw new Exception($"Default Role Creation Error(s): {string.Join(",", roleResult.Errors.Select(r => $"{r.Code}: {r.Description}"))}");
                }
            }

            if (!roleManager.RoleExistsAsync(Constants.Constants.SuperRoleName).Result)
            {
                SecurityRole role = new SecurityRole(Constants.Constants.SuperRoleName);
                role.Id = Guid.NewGuid();
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;

                if (!roleResult.Succeeded)
                {
                    throw new Exception($"Default Role Creation Error(s): {string.Join(",", roleResult.Errors.Select(r => $"{r.Code}: {r.Description}"))}");
                }
            }
        }
    }
}
