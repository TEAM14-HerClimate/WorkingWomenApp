using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkingWomenApp.BLL.Interfaces;
using WorkingWomenApp.BLL.UnitOfWork;
using WorkingWomenApp.Database.DTOs.UserDtos;
using WorkingWomenApp.Database.DTOs.ViewModels;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.Controllers
{
    public class SecurityUsersController : Controller
    {
        private readonly ISecurityService _securityService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SecurityUsersController(ISecurityService securityService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _securityService = securityService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var users = _unitOfWork.UserRepository.Set<ApplicationUser>().ToList();
            return View(users);
        }
        public async Task<ActionResult> Details(Guid? id)
        {
            var user =  _unitOfWork.UserRepository.Set<ApplicationUser>().FirstOrDefault(r => r.Id == id);
            var userDto = _mapper.Map<UserCreateDto>(user);

            return View(userDto);  // will automatically look in the views folder
        }

        public async Task<ActionResult> UserRoleMapping(Guid? id)
        {
            var user = _unitOfWork.UserRepository.Set<UserRoleMapping>().FirstOrDefault(r => r.UserId == id);
            

            return View(user);  // will automatically look in the views folder
        }
    }
}
