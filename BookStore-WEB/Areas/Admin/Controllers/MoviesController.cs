using AutoMapper;
using MovieStore_ApplicationCore.DTO_s.MovieDTO;
using MovieStore_ApplicationCore.Entities.Concrete;
using MovieStore_ApplicationCore.Interfaces;
using MovieStore_WEB.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MovieStore_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMovieCategoryService _movieCategoryService;
        private readonly ICategoryService _categoryService;
        private readonly IDirectorService _directorService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MoviesController(IMovieService movieService, IMovieCategoryService movieCategoryService, ICategoryService categoryService, IDirectorService directorService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _movieService = movieService;
            _movieCategoryService = movieCategoryService;
            _categoryService = categoryService;
            _directorService = directorService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
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
                        Year = x.Year,
                        DirectorName = x.Director.FirstName + " " + x.Director.LastName,
                        Image = x.Image != null ? x.Image : "noimage.png",
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        Status = x.Status,
                        Categories = _movieCategoryService.GetCategoryNamesByMovieId(x.Id)
                    },
                    where: x => x.Status != MovieStore_ApplicationCore.Entities.Abstract.Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate),
                    join: x => x.Include(z => z.Director)
                );
            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> AddMovie()
        {
            ViewBag.Categories = new SelectList
                (
                    await _categoryService.GetAllAsync(), "Id", "Name"
                );

            ViewBag.Directors = new SelectList
                (
                    await _directorService.GetDirectors(), "Id", "FullName"
                );
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(AddMovieDTO model)
        {
            ViewBag.Categories = new SelectList
                (
                    await _categoryService.GetAllAsync(), "Id", "Name"
                );

            ViewBag.Directors = new SelectList
                (
                    await _directorService.GetDirectors(), "Id", "FullName"
                );

            if (ModelState.IsValid)
            {
                string imageName = "noimage.png";
                if (model.UploadImage is not null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images/movies");
                    imageName = $"{Guid.NewGuid()}_{model.UploadImage.FileName}";
                    string filePath = Path.Combine(uploadDir, imageName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    await model.UploadImage.CopyToAsync(fileStream);
                    fileStream.Close();
                }

                var movie = _mapper.Map<Movie>(model);
                movie.Image = imageName;
                await _movieService.AddAsync(movie);

                foreach (var categoryId in model.Categories)
                {
                    var movieCategory = new MovieCategory { MovieId = movie.Id, CategoryId = Convert.ToInt32(categoryId) };
                    await _movieCategoryService.AddMovieCategory(movieCategory);
                }

                TempData["Success"] = "Film Eklendi!!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Aşağıdaki kurallara uyunuz!!";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMovie(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie is not null)
            {
                var director = await _directorService.GetByIdAsync(movie.DirectorId);
                ViewBag.Directors = new SelectList
                    (
                        await _directorService.GetDirectors(), "Id", "FullName", director
                    );

                List<Category> hasCategories = new List<Category>();
                List<Category> hasNotCategories = new List<Category>();

                foreach (var category in await _categoryService.GetAllAsync())
                {
                    if (await _movieCategoryService.AnyMovieCategory(movie.Id, category.Id))
                    {
                        hasCategories.Add(category);
                    }
                    else
                    {
                        hasNotCategories.Add(category);
                    }
                }
                var model = _mapper.Map<UpdateMovieDTO>(movie);
                model.HasCategories = hasCategories;
                model.HasNotCategories = hasNotCategories;
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMovie(UpdateMovieDTO model)
        {
            var director = await _directorService.GetByIdAsync(model.DirectorId);
            ViewBag.Directors = new SelectList
                (
                    await _directorService.GetDirectors(), "Id", "FullName", director
                );
            List<Category> hasCategories = new List<Category>();
            List<Category> hasNotCategories = new List<Category>();

            foreach (var category in await _categoryService.GetAllAsync())
            {
                if (await _movieCategoryService.AnyMovieCategory(model.Id, category.Id))
                {
                    hasCategories.Add(category);
                }
                else
                {
                    hasNotCategories.Add(category);
                }
            }
            model.HasCategories = hasCategories;
            model.HasNotCategories = hasNotCategories;

            if (ModelState.IsValid)
            {
                string imageName = model.Image;
                if (model.UploadImage is not null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images/movies");

                    if (!string.Equals(model.Image, "noimage.png"))
                    {
                        string oldPath = Path.Combine(uploadDir, model.Image);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    imageName = $"{Guid.NewGuid()}_{model.UploadImage.FileName}";
                    string filePath = Path.Combine(uploadDir, imageName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    await model.UploadImage.CopyToAsync(fileStream);
                    fileStream.Close();
                }

                foreach (var categoryId in model.AddIds ?? new string[] { })
                {
                    var movieCategory = new MovieCategory { MovieId = model.Id, CategoryId = Convert.ToInt32(categoryId) };
                    await _movieCategoryService.AddMovieCategory(movieCategory);
                }

                foreach (var categoryId in model.DeleteIds ?? new string[] { })
                {
                    await _movieCategoryService.DeleteMovieCategory(model.Id, Convert.ToInt32(categoryId));
                }

                var movie = _mapper.Map<Movie>(model);
                movie.Image = imageName;
                await _movieService.UpdateAsync(movie);
                TempData["Success"] = "Film güncellendi!!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!!";
            return View(model);
            
        }

        [HttpGet]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie is not null)
            {
                await _movieService.DeleteAsync(movie);
                TempData["Success"] = "Film silinmiştir!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Film bulunamadı!!";
            return NotFound();
        }
    }
}
