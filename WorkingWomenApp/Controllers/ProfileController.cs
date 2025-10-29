using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkingWomenApp.Attribute;
using WorkingWomenApp.BLL.Interfaces;
using WorkingWomenApp.BLL.UnitOfWork;
using WorkingWomenApp.Database.DTOs.ProfileDtos;
using WorkingWomenApp.Database.enums;
using WorkingWomenApp.Database.Models.Climate;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;

        public ProfileController(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sessionService = sessionService;
        }

        [ProtectAction(SecurityModule.Profile, SecuritySubModule.UserProfile, SecuritySystemAction.ViewItem)]
        public async Task<IActionResult> Index()
        {
            var articles = await _unitOfWork.ProfileRepository.GetAllAsync();
            return View();
        }

        [ProtectAction(SecurityModule.Profile, SecuritySubModule.UserProfile, SecuritySystemAction.ViewItem)]
        public async Task<ActionResult> Details(Guid? userId)
        {
        
            var profile = await _unitOfWork.ProfileRepository.GetAsync(x => x.Id == userId, includeProperties: "User");
            var profileDtos = _mapper.Map<UserProfileDtos>(profile);
            // strongly typed view - by putting object into the view vs. ViewBag.ComicBook = comicBook;
            return View(profileDtos);  // will automatically look in the views folder
        }

        [HttpPut]
        [HttpPost]
        [ProtectAction(SecurityModule.Profile, SecuritySubModule.UserProfile, SecuritySystemAction.CreateAndEdit)]
        public async Task<ActionResult> Details( UserProfileDtos? profileDtos = null)
        {

            var profile = _mapper.Map<UserProfile>(profileDtos);
            if (profileDtos.Id == Guid.Empty)
            {
                await _unitOfWork.ProfileRepository.AddAsync(profile);
                
            }
            else
            {
                await _unitOfWork.ProfileRepository.UpdateAsync(profile);
            }
            await _unitOfWork.SaveChangesAsync();
            return Redirect("/Profile");
        }
        public async Task<ActionResult> UserProfile()
        {
            var id = _sessionService.GetUser().Id;
            var profile=await _unitOfWork.ProfileRepository.GetAsync(x => x.UserId == id, includeProperties: "User");
            var profiledto = _mapper.Map<UserProfileDtos>(profile);

            if (profiledto == null)
            {
                profiledto = new UserProfileDtos
                { 
                    // Assign user ID as profile ID
                    UserId = id
                };

            }
            return View(profiledto);
           
        }

    }
}
