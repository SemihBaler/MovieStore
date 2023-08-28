using MovieStore_ApplicationCore.Interfaces;
using MovieStore_WEB.Models;
using MovieStore_WEB.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MovieStore_WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMovieCategoryService _movieCategoryService;

        public HomeController(IMovieService movieService, IMovieCategoryService movieCategoryService)
        {
            _movieService = movieService;
            _movieCategoryService = movieCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetFilteredListAsync
                (
                    select: x => new GetMovieVM
                    {
                        Id = x.Id,
                        Name = x.Name, 
                        Description = x.Description,
                        DirectorName = x.Director.FirstName + " " + x.Director.LastName,
                        Image = x.Image != null ? x.Image : "noimage.png",
                        Year = x.Year,
                        CreatedDate = x.CreatedDate,
                        Categories = _movieCategoryService.GetCategoryNamesByMovieId(x.Id)
                    },
                    where: x => x.Status != MovieStore_ApplicationCore.Entities.Abstract.Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                    join: x => x.Include(z => z.Director)
                );

            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}