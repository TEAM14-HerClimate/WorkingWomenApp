using Microsoft.AspNetCore.Http;

namespace WorkingWomenApp.Database.DTOs.UserDtos
{
    public class UserCreateDto: UserDto
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public Guid? RoleId { get; set; }
    }
}
