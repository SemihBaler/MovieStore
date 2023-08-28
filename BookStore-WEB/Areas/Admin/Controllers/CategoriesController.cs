using AutoMapper;
using MovieStore_ApplicationCore.DTO_s.CategoryDTO;
using MovieStore_ApplicationCore.Entities.Concrete;
using MovieStore_ApplicationCore.Interfaces;
using MovieStore_WEB.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieStore_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetFilteredListAsync
                (
                    select: x => new GetCategoryVM
                    { 
                        Id = x.Id,
                        Name = x.Name,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        Status = x.Status
                    },
                    where: x => x.Status != MovieStore_ApplicationCore.Entities.Abstract.Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate)
                );

            return View(categories);
        }

        [HttpGet]
        public IActionResult AddCategory() => View();

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(model);
                await _categoryService.AddAsync(category);
                TempData["Success"] = "Kategori eklenmiştir!!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Lütfen kurallara uyunuz!!";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category != null)
            {
                var model = _mapper.Map<UpdateCategoryDTO>(category);
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(model);
                await _categoryService.UpdateAsync(category);
                TempData["Success"] = "Kategori güncellendi!!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!!";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category != null)
            {
                await _categoryService.DeleteAsync(category);
                TempData["Success"] = "Kategori silinmiştir!!";
                return RedirectToAction("Index");   
            }
            TempData["Error"] = "Bir şeyler ters gitti :(";
            return NotFound();
        }
    }
}
