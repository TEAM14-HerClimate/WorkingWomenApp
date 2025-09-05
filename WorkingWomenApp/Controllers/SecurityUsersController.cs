using Microsoft.AspNetCore.Mvc;
using WorkingWomenApp.BLL.Interfaces;
using WorkingWomenApp.BLL.UnitOfWork;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.Controllers
{
    public class SecurityUsersController : Controller
    {
        private readonly ISecurityService _securityService;
        private readonly IUnitOfWork _unitOfWork;

        public SecurityUsersController(ISecurityService securityService, IUnitOfWork unitOfWork)
        {
            _securityService = securityService;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var users = _unitOfWork.UserRepository.Set<ApplicationUser>().ToList();
            return View(users);
        }
    }
}
