using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SecurityUsersController(ISecurityService securityService, IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _securityService = securityService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
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
         
        public async Task<IActionResult> Details(UserCreateDto registerVM)
        {
            var user = _mapper.Map<ApplicationUser>(registerVM);
            var result = await _userService.AddUser(user);
            if (result.Item1)
            {
                return RedirectToAction("Index", "SecurityUsers");
            }
            else
            {
                return View(registerVM);
            }
        }
        public async Task<ActionResult> UserRoleMapping(Guid? id)
        {
            var user = _unitOfWork.UserRepository.Set<UserRoleMapping>().Include(r => r.SecurityRole).Where(r => r.UserId == id);
            var userDto = _mapper.Map<UserMappingDto>(user);

            return View();  // will automatically look in the views folder
        }

        [HttpPost]
        public async Task<ActionResult> UserRoleMapping(UserMappingDto mapping)
        {
            //var user = _unitOfWork.UserRepository.Set<UserRoleMapping>().Include(r=>r.SecurityRole).Where(r => r.UserId == mapping.Id);

            return View();  // will automatically look in the views folder
        }
    }
}
