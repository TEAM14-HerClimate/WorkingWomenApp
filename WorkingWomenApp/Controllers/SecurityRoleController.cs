using Microsoft.AspNetCore.Mvc;
using WorkingWomenApp.Attribute;
using WorkingWomenApp.BLL.Implementation;
using WorkingWomenApp.BLL.Interfaces;
using WorkingWomenApp.BLL.Repository;
using WorkingWomenApp.BLL.UnitOfWork;
using WorkingWomenApp.Database.DTOs.ViewModels;
using WorkingWomenApp.Database.enums;
using WorkingWomenApp.Database.Models.Users;
using WorkingWomenApp.Helpers;
using WorkingWomenApp.Views.SecurityRole;

namespace WorkingWomenApp.Controllers
{
    public class SecurityRoleController : Controller
    {
        private readonly ISecurityService _securityService;
        private readonly IUnitOfWork _unitOfWork;

        public SecurityRoleController(ISecurityService securityService, IUnitOfWork unitOfWork)
        {
            _securityService = securityService;
            _unitOfWork = unitOfWork;
        }

        [ProtectAction(SecurityModule.Settings, SecuritySubModule.SecurityRoles, SecuritySystemAction.ViewItem)]
        public IActionResult Index()
        {
            var roles = _unitOfWork.UserRepository.Set<SecurityRole>().ToList();
            return View(roles);
        }

        [ProtectAction(SecurityModule.Settings, SecuritySubModule.SecurityRoles, SecuritySystemAction.ViewItem)]
        public async Task<ActionResult> EditRole(Guid id)
        {
            var permissionTypes = PermissionHelper.GetPermissionTypes();
            await _securityService.EnqueuePermissions(permissionTypes);

            RoleContainerModel roleContainer = await RoleContainerModel.GetRoleDetails(_unitOfWork, id);
            return View(roleContainer);
        }
        [HttpPost]
        [HttpPut]
        public async Task<ActionResult> EditRole(RoleContainerModel model)
        {
            var success = await _securityService.SaveSecurityRoleAsync(model);
            if (success.Item1)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                success.Item2 = model.ErrorMessage;
                return View(model);
            }
        }
    }
}
