using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WorkingWomenApp.BLL.Interfaces;
using WorkingWomenApp.BLL.UnitOfWork;
using WorkingWomenApp.Database.enums;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.BLL.Implementation
{
    public class SessionService: ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        private List<Guid> SecurityRoleIds = new List<Guid>();
        private List<SessionServicePermission> Permissions = new List<SessionServicePermission>();

        private readonly RoleManager<SecurityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        private ApplicationUser _user;

        private bool _IsInSuperRole;
        private bool _IsPromiseCRMAdminRole;
        private SecurityModule? _module;
        private SecuritySubModule? _submodule;
        private SecuritySystemAction? _systemaction;

        public SessionService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,
            RoleManager<SecurityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
      
            _roleManager = roleManager;
            _userManager = userManager;
         

            if (_httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false)
            {

                var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                var user = _userManager.FindByNameAsync(userName).Result;
                _user =  user;
                 InitializeUserPermissions( user);


            }
        }
  

        public void InitializeUserPermissions(ApplicationUser user)
        {
            
            if (user != null)
            {
                var userid = _user.Id;
                
                var userRoles = _repository.Set<UserRoleMapping>().Where(r => r.UserId == user.Id);
                await _unitOfWork..GetAsync(x => x.Id == id, includeProperties: "");


                SecurityRoleIds = userRoles.Select(r => r.SecurityRoleId).ToList();
                    var rolesPermission =
                        _repository.Set<RolePermission>().Where(r => SecurityRoleIds.Contains(r.RoleId));
                        

                    Permissions = rolesPermission
                        .Select(r => new SessionServicePermission
                        {
                            SystemAction = r.Permission.SystemAction,
                            Module = r.Permission.Module,
                            SubModule = r.Permission.SubModule
                        })
                        .Distinct().ToList();
                   
            }
            
        }
        public void InitializeForApiUser(ApplicationUser user)
        {
            if (user == null) return;

            _user = user;

        }

        public List<Guid> GetSecurityRoleIds() => SecurityRoleIds;
     
        public Guid GetUserId()
        {
            return _user.Id;
        }
        public string GetUserName()
        {
            return _user?.UserName;
        }

        //public bool IsSuperAdmin()
        //{
        //    return _user?.IsSuperUser() ?? false;
        //}

        public bool IsInSuperRole()
        {
            return _IsInSuperRole;
        }
        public bool IsAdmin()
        {
            return _IsPromiseCRMAdminRole;
        }

        public ApplicationUser GetUser()
        {
            return _user;
        }


        public bool HasAccessToPermission(SecurityModule Module, SecuritySubModule SubModule, SecuritySystemAction Action, bool IgnoreSystemAdminPriviledge = false)
        {
            if (_IsInSuperRole && !IgnoreSystemAdminPriviledge)
            {
                return true;
            }
            else if (Permissions.Any(r => r.Module == Module && r.SubModule == SubModule && r.SystemAction == Action))
            {
                return true;
            }
            return false;
        }

       

   

  

        public void SetProtectedAction(SecurityModule Module, SecuritySubModule SubModule, SecuritySystemAction Action)
        {
            this._module = Module;
            this._submodule = SubModule;
            this._systemaction = Action;
        }


    }
    public class SessionServicePermission : ISecurityPermission
    {
        public Guid PermissionId { get; set; }
        public bool Enabled { get; set; }
        public SecurityModule Module { get; set; }
        public SecuritySubModule SubModule { get; set; }
        public SecuritySystemAction SystemAction { get; set; }
    }
}
