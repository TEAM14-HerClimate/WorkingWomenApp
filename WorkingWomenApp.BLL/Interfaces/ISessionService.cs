using WorkingWomenApp.Database.enums;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.BLL.Interfaces
{
    public interface ISessionService
    {
        Guid GetUserId();
        string GetUserName();
        //bool IsSuperAdmin();
        bool IsInSuperRole();
        /// <summary>
        /// Initialize UIA admin
        /// </summary>
        /// <param name="_user"></param>
        bool IsAdmin();
        ApplicationUser GetUser();
        List<Guid> GetSecurityRoleIds();
       //string Ge

   

        bool HasAccessToPermission(SecurityModule Module, SecuritySubModule SubModule, SecuritySystemAction Action, bool IgnoreSystemAdminPriviledge = false);
        /// <summary>
        /// Initialize the user
        /// </summary>
        /// <param name="_user"></param>
        void InitializeForApiUser(ApplicationUser _user);

        /// <summary>
        /// Initialize the User currently logged in.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userId"></param>
        void InitializeUserPermissions(ApplicationUser user);

       
        void SetProtectedAction(SecurityModule Module, SecuritySubModule SubModule, SecuritySystemAction Action);

    }
}
