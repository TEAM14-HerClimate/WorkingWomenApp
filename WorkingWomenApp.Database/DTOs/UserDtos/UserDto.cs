using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingWomenApp.Database.Models.Users;


namespace WorkingWomenApp.Database.DTOs.UserDtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public List<UserRoleDto> Roles { get; set; } = new List<UserRoleDto>();
    }

    public class UserRoleDto
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
       
        public List<RoleMappingDto> Roles { get; set; } = new List<RoleMappingDto>();
    }


    public class RoleMappingDto
    {

        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
