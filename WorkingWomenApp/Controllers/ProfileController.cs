using Microsoft.AspNetCore.Mvc;
using WorkingWomenApp.Database.Models.Climate;

namespace WorkingWomenApp.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Details(Guid? id)
        {

            var profile = await _unitOfWork.ArticleRepository.GetAsync(x => x.Id == id, includeProperties: ""); ;

            // strongly typed view - by putting object into the view vs. ViewBag.ComicBook = comicBook;
            return View(comicBook);  // will automatically look in the views folder
        }

        [HttpPut]
        [HttpPost]
        public async Task<ActionResult> Details(Guid? id, Article? article = null)
        {
            if (article != null)
                article.Id = (Guid)id;

            await _unitOfWork.ArticleRepository.AddAsync(article);
            await _unitOfWork.SaveChangesAsync();
            return Redirect("/Index");
        }

    }
}
