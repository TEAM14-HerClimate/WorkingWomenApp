using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Net;

using Microsoft.AspNetCore.Mvc;
using WorkingWomenApp.BLL.Interfaces;
using WorkingWomenApp.BLL.Repository;
using WorkingWomenApp.BLL.UnitOfWork;
using WorkingWomenApp.Database.Models.Users;
using WorkingWomenApp.Database.enums;

namespace WorkingWomenApp.Attribute
{
    public class ProtectActionAttribute: ActionFilterAttribute, IProtectActionAttribute
    {

        private readonly IUserRepository _repository;
        public SecurityModule Module { get; set; }
        public SecuritySubModule SubModule { get; set; }
        public SecuritySystemAction SystemAction { get; set; }
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        public ProtectActionAttribute(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IUserRepository repository)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _repository = repository;


        }
        public static readonly SecurityModule[] AccessibleModules = new[] {
                
                SecurityModule.Settings,
                SecurityModule.Climate,
                SecurityModule.Account,
                SecurityModule.Health,
                SecurityModule.Profile,


            };

        public ProtectActionAttribute(SecurityModule Module, SecuritySubModule SubModule, SecuritySystemAction Action)
        {
            this.Module = Module;
            this.SubModule = SubModule;
            SystemAction = Action;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var _sessionService = (ISessionService)filterContext.HttpContext.RequestServices.GetService(typeof(ISessionService));

            if (!ModuleActivated() || !UserHasPermissionToAccessAction(_sessionService))
            {
                var userId = _sessionService.GetUserId();
                var user = _sessionService.GetUser();
                if (user != null && !user.IsSuperUser )
                {
                    throw new Exception("You do not have permission to access this resource.");
                }
            }

            _sessionService.SetProtectedAction(Module, SubModule, SystemAction);
        }

        public IEnumerable<SecurityModule> GetAccessibleModules()
        {
            return AccessibleModules;
        }

        private bool UserHasPermissionToAccessAction(ISessionService sessionService)
        {
            var hasPermission = sessionService.HasAccessToPermission(Module, SubModule, SystemAction);
            return hasPermission;
        }

        private bool ModuleActivated()
        {
            var activeModule= AccessibleModules.Contains(Module);
            return activeModule;
        }


        public void OnPageHandlerExecuted(PageHandlerExecutedContext filterContext)
        {

            // throw new System.NotImplementedException();
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            //throw new System.NotImplementedException();
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext filterContext)
        {
            var _sessionService = (ISessionService)filterContext.HttpContext.RequestServices.GetService(typeof(ISessionService));
    

            if (!ModuleActivated() || !UserHasPermissionToAccessAction(_sessionService))
            {
                var userId = _sessionService.GetUserId();
                var user = _repository.Set<ApplicationUser>().Where(r => r.Id == userId);
               

            }

            _sessionService.SetProtectedAction(Module, SubModule, SystemAction);
        }
        
    }
}
