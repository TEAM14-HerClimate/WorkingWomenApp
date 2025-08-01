using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkingWomenApp.BLL.UnitOfWork;
using WorkingWomenApp.Database.DTOs.ViewModels;
using WorkingWomenApp.Database.Models.Climate;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProfileController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var articles = await _unitOfWork.ProfileRepository.GetAllAsync();
            return View();
        }

        public async Task<ActionResult> Details(Guid? id)
        {

            var profile = await _unitOfWork.ProfileRepository.GetAsync(x => x.Id == id, includeProperties: "User"); 

            // strongly typed view - by putting object into the view vs. ViewBag.ComicBook = comicBook;
            return View();  // will automatically look in the views folder
        }

        [HttpPut]
        [HttpPost]
        public async Task<ActionResult> Details(Guid? id, UserProfileDtos? profileDtos = null)
        {
            var profile = _mapper.Map<UserProfile>(profileDtos);
            if (id == Guid.Empty)
            {
                await _unitOfWork.ProfileRepository.AddAsync(profile);
                
            }
            else
            {
                await _unitOfWork.ProfileRepository.UpdateAsync(profile);
            }
            await _unitOfWork.SaveChangesAsync();
            return Redirect("/Index");
        }
        public async Task<ActionResult> UserProfile(Guid id)
        {
                var profile=await _unitOfWork.ProfileRepository.GetAsync(x => x.Id == id, includeProperties: "User"); 
                var profiledto = _mapper.Map<UserProfileDtos>(profile);
          
            return View(profiledto);
        }

    }
}
