using WorkingWomenApp.Database.enums;

namespace WorkingWomenApp.Database.DTOs.UserDtos
{
    public class SecurityRoleDto
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public bool IsNewRecord { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }

    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    
        public List<RolePermissionDto> RolePermissions { get; set; }
    }


    public class RolePermissionDto
    {
        public Guid Id { get; set; }
        public Guid PermissionId { get; set; }
        public Guid RoleId { get; set; }
        public PermissionTypesDto PermissionType { get; set; }
    }

    public class PermissionTypesDto
    {
        public Guid Id { get; set; }
        public SecurityModule Module { get; set; }
        public SecuritySubModule SubModule { get; set; }
        public SecuritySystemAction SystemAction { get; set; }
    }
}
