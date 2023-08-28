using AutoMapper;
using MovieStore_ApplicationCore.DTO_s.DirectorDTO;
using MovieStore_ApplicationCore.Entities.Concrete;
using MovieStore_ApplicationCore.Interfaces;
using MovieStore_WEB.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieStore_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class DirectorsController : Controller
    {
        private readonly IDirectorService _directorService;
        private readonly IMapper _mapper;

        public DirectorsController(IDirectorService directorService, IMapper mapper)
        {
            _directorService = directorService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var directors = await _directorService.GetFilteredListAsync
                (
                    select: x => new GetDirectorVM
                    {
                        Id = x.Id,
                        FullName = x.FirstName + " " + x.LastName,
                        BirthDate = x.BirthDate,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        Status = x.Status
                    },
                    where: x => x.Status != MovieStore_ApplicationCore.Entities.Abstract.Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate )
                );

            return View(directors);
        }

        [HttpGet]
        public IActionResult AddDirector() => View();

        [HttpPost]
        public async Task<IActionResult> AddDirector(AddDirectorDTO model)
        {
            if (ModelState.IsValid)
            {
                var director = _mapper.Map<Director>(model);
                await _directorService.AddAsync(director);
                TempData["Success"] = "Yönetmen eklendi!!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Aşağıdaki kurallara uyunuz!!";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDirector(int id)
        {
            var director = await _directorService.GetByIdAsync(id);
            if (director != null)
            {
                var model = _mapper.Map<UpdateDirectorDTO>(director);
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDirector(UpdateDirectorDTO model)
        {
            if (ModelState.IsValid) 
            {
                var director = _mapper.Map<Director>(model);
                await _directorService.UpdateAsync(director);
                TempData["Success"] = "Yönetmen güncellendi!!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!!";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            var director = await _directorService.GetByIdAsync(id);
            if (director != null)
            {
                await _directorService.DeleteAsync(director);
                TempData["Success"] = "Yönetmen silindi!!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Yönetmen bulunamadı!!";
            return NotFound();
        }

    }
}
