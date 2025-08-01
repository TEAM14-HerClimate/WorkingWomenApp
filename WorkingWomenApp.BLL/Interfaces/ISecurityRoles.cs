using WorkingWomenApp.Database.enums;

namespace WorkingWomenApp.BLL.Interfaces
{
    public interface ISecurityRoles
    {
        Guid Id { get; set; }
        string Name { get; set; }
        bool IsNewRecord { get; set; }
        string ErrorMessage { get; set; }
        List<ISecurityPermission> GetPermissions();
    }

    public interface ISecurityPermission
    {
        Guid PermissionId { get; set; }
        bool Enabled { get; set; }
        SecurityModule Module { get; set; }
        SecuritySubModule SubModule { get; set; }
        SecuritySystemAction SystemAction { get; set; }
    }
}
