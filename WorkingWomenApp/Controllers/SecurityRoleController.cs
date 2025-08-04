using Microsoft.AspNetCore.Mvc;
using WorkingWomenApp.Attribute;
using WorkingWomenApp.BLL.Implementation;
using WorkingWomenApp.BLL.Interfaces;
using WorkingWomenApp.Database.DTOs.ViewModels;
using WorkingWomenApp.Database.enums;
using WorkingWomenApp.Helpers;

namespace WorkingWomenApp.Controllers
{
    public class SecurityRoleController : Controller
    {
        private readonly ISecurityService _securityService;
        public SecurityRoleController(ISecurityService securityService)
        {
            _securityService = securityService;
        }
        [ProtectAction(SecurityModule.Settings, SecuritySubModule.SecurityRoles, SecuritySystemAction.ViewItem)]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ProtectAction(SecurityModule.Settings, SecuritySubModule.SecurityRoles, SecuritySystemAction.ViewItem)]
        public async Task<ActionResult>  EditRolePermissionById(Guid id)
        {
            var permissionTypes = PermissionHelper.GetPermissionTypes();
            var response =  _securityService.EnqueuePermissions(permissionTypes);
            return View(Index);
        }
    }
}
