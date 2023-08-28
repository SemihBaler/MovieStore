using MovieStore_ApplicationCore.DTO_s.RoleDTO;
using MovieStore_ApplicationCore.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MovieStore_WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class RolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        public IActionResult CreateRole() => View();

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDTO model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole(model.Name);
                IdentityResult result = await _roleManager.CreateAsync(role);
                if (result.Succeeded) 
                {
                    TempData["Success"] = "Rol kaydedildi!";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Rol kaydedilemedi!!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> AssignedUser(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            List<ApplicationUser> hasRole = new List<ApplicationUser>();
            List<ApplicationUser> hasNotRole = new List<ApplicationUser>();

            foreach (var user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? hasRole : hasNotRole;
                list.Add(user);
            }

            AssignedRoleDTO model = new AssignedRoleDTO
            {
                Role = role,
                HasNotRole = hasNotRole,
                HasRole = hasRole,
                RoleName = role.Name
            };

            return View(model); 
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignedUser(AssignedRoleDTO model)
        {
            IdentityResult result = new IdentityResult();

            foreach (var userId in model.AddIds ?? new string[] { })
            {
                var user = await _userManager.FindByIdAsync(userId);
                result = await _userManager.AddToRoleAsync(user, model.RoleName);
            }

            foreach (var userId in model.DeleteIds ?? new string[] { })
            {
                var user = await _userManager.FindByIdAsync(userId);
                result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
            }

            if (result.Succeeded)
            {
                TempData["Success"] = "Role atama ve çıkarma işlemleri başarılı bir şekilde yapıldı!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Bir şeyler ters gitti!!";
            return View(model);
        }

        public async Task<IActionResult> RemoveRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            IdentityResult result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                TempData["Success"] = "Rol başarılı bir şekilde silindi!";
            }
            else
            {
                TempData["Error"] = "Bir şeyler ters gitti!!";
            }
            return RedirectToAction("Index");
        }
    }
}
