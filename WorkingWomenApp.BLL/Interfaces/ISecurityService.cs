using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingWomenApp.Database.DTOs.UserDtos;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.BLL.Interfaces
{
    public interface ISecurityService
    {
        Task<SecurityRole> GetRoleById(Guid id);
        Task<(bool, string)> SaveSecurityRoleAsync(SecurityRoleDto model );

        Task<IEnumerable<SecurityRole>> FetchAllSecurityRolesAsync();
        Task<SecurityRole> FetchOneSecurityRoleswithPermissionAsync(Guid id);

       
        Task EnqueuePermissions(List<PermissionType> protectedActionAttributes);

        Task _UpdatePermissions(List<PermissionType> protectedActionAttributes);
        Task<UserRoleMapping> FetchUserAndRolesAsync(Guid userId);
     
        Task<IEnumerable<RolePermission>> FetchAllRolePermissions(Guid roleId);
    }
}
