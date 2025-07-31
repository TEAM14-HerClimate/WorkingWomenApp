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
}
