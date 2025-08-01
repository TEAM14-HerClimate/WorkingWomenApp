using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkingWomenApp.BLL.UnitOfWork;
using WorkingWomenApp.Database.DTOs.ViewModels;
using WorkingWomenApp.Database.Models.Climate;
using System.Reflection;

namespace WorkingWomenApp.Controllers
{
    public class ClimateController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ClimateController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var articles = await _unitOfWork.ArticleRepository.GetAllAsync();
            //var articlesDTOs = _mapper.Map<IEnumerable<ArticleDto>>(articles);
            return View(articles);
        }

        // allow nullable
        public async Task<ActionResult> Details(Guid? id)
        {

            var article = await _unitOfWork.ArticleRepository.GetAsync(x => x.Id == id, includeProperties: ""); ;
            var articleDto = _mapper.Map<ArticleDto>(article);

            return View(articleDto);  // will automatically look in the views folder
        }

        [HttpPut]
        [HttpPost]
       public async Task<ActionResult> Details(Guid? id , ArticleDto? articleDto = null)
        {
            //if (articleDto==Guid.Empty(Empty))
            //{
            //    articleDto.Id = (Guid)id;
            //}
                
            var article = _mapper.Map<Article>(articleDto);
            await _unitOfWork.ArticleRepository.AddAsync(article);
            await _unitOfWork.SaveChangesAsync();
            return Redirect("/Home/Index");
        }


    }
}
