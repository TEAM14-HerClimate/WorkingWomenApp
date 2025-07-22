using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WorkingWomenApp.Database.DTOs
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
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }

    }

}
