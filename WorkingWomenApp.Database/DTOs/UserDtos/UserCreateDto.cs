using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WorkingWomenApp.Database.DTOs.UserDtos
{
    public class UserCreateDto: UserDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        public Guid? RoleId { get; set; }
        public string RedirectUrl { get; set; }
        
        
    }

    public class ChangePasswordDto
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "Required!")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Required!")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Required!")]
        public string ConfirmPassword { get; set; }
        public string ErrorMessage { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool isSuperUser { get; set; }
    }
  

    public class LockoutDto
    {
        public string Id { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class PasswordResetDto
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
       
        public string ErrorMessage { get; set; }
        public string UserId { get; set; }
    }


}
