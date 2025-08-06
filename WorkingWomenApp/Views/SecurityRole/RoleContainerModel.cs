using Microsoft.EntityFrameworkCore;
using WorkingWomenApp.Attribute;
using WorkingWomenApp.BLL.Interfaces;
using WorkingWomenApp.BLL.UnitOfWork;
using WorkingWomenApp.Database.enums;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.Views.SecurityRole
{
    public class RoleContainerModel : ISecurityRole
    {
        public bool IsNewRecord { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }

        public string ErrorMessage { get; set; }

        public List<RolePermissionViewModel> Permissions { get; set; } = new List<RolePermissionViewModel>();

        public RoleContainerModel()
        {
        }

        public List<ISecurityPermission> GetPermissions()
        {
            return Permissions.Select(r => (ISecurityPermission)r).ToList();
        }

        public static async Task<RoleContainerModel> GetRoleDetails(IUnitOfWork unitOfWork, Guid Id)
        {
            var dbRole = await unitOfWork.UserRepository.Set<Database.Models.Users.SecurityRole>().Where(r=>r.Id==Id).FirstOrDefaultAsync();
            bool IsNew = false;

            if (dbRole == null)//Initialize New Record
            {
                IsNew = true;
                dbRole = new Database.Models.Users.SecurityRole();
            }

            //Build Header
            RoleContainerModel model = new RoleContainerModel
            {
                Id = Id,
                Name = dbRole.Name,
                IsNewRecord = IsNew
            };

            //Fetch existing role permissions
            var dbRolePermissions = await unitOfWork.RolePermissionRepository.Set<RolePermission>().Where(r => r.RoleId == Id).ToListAsync();


            //Map Global Permissions to Role Permissions
            var accessibleModules = ProtectActionAttribute.AccessibleModules;
            var accessiblePermissions = await unitOfWork.UserRepository.Set<PermissionType>()
                .Where(r => accessibleModules.Contains(r.Module)).ToListAsync();

            foreach (var permission in accessiblePermissions)
            {
                model.Permissions.Add(new RolePermissionViewModel
                {
                    SystemAction = permission.SystemAction,
                    Module = permission.Module,
                    PermissionId = permission.Id,
                    SubModule = permission.SubModule,
                    Enabled = dbRolePermissions.Any(r => r.PermissionId == permission.Id)
                });
            }

            return model;
        }
    }

    public class RolePermissionViewModel : ISecurityPermission
    {
        public Guid
            PermissionId { get; set; }
        public bool Enabled { get; set; }
        public SecurityModule Module { get; set; }
        public SecuritySubModule SubModule { get; set; }
        public SecuritySystemAction SystemAction { get; set; }
    }
}
