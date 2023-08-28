using MovieStore_ApplicationCore.DTO_s.AccountDTO;
using MovieStore_ApplicationCore.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace MovieStore_WEB.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
        }

        //Herkesin bu sayfaya erişimine izin ver.
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Kayıt başarılı giriş ekranına yönlendirildiniz!";
                    return RedirectToAction("LogIn");
                }
                TempData["Error"] = "Kayıt yapılamadı!!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult LogIn() => View();

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInDTO model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        TempData["Success"] = "Hoşgeldiniz " + user.UserName;
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Kullanıcı adı veya şifre yanlış!!";
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!!";
            return View(model);
        }

        //Bir şey yazılmazsa default olarak HTTPGET'dir.
        [HttpGet]
        public async Task<IActionResult> EditUser()
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null) 
            {
                var model = new EditUserDTO(user);
                return View(model);
            }
            return NotFound();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserDTO model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                user.UserName = model.UserName;
                if (model.Password != null)
                {
                    user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
                }
                user.Email = model.Email;

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Profil bilgileri güncellendi!";
                }
                else
                {
                    TempData["Error"] = "Profil bilgilerinin güncellenemedi!!";
                }
                return View(model);
            }
            TempData["Error"] = "Lütfen aşağıdaki kurallara uyunuz!";
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            TempData["Succes"] = "Çıkış Yapıldı";
            return RedirectToAction("LogIn");
        }
    }
}
