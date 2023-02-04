using Blog.Model;
using Blog.WebUI.Extensions.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Blog.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Blog.WebUI.Authorize;

namespace Blog.WebUI.Extensions.Controllers
{
    public class HomeController : Controller
    {
        AuthorData _authorData;

        public HomeController(AuthorData _authorData)
        {
            this._authorData = _authorData;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            var test = _authorData.GetAll();
            var model = new LoginModel()
            {
                Password = "",
                Username = "",
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var errors = new List<string>();
            var return_model = new LoginModel();

            if (string.IsNullOrEmpty(username)) errors.Add("Kullanıcı Boş Bırakılamaz");
            if (string.IsNullOrEmpty(password)) errors.Add("Şifre Boş Bırakılamaz");
            if (errors.Count() > 0)
            {
                ViewBag.Result = new ViewModelResult(false, "Hata Oluştu", errors);
                return View(return_model);
            }

            var author = _authorData.GetBy(x => x.Username == username && x.Password == password && x.IsActive && !x.IsDeleted).FirstOrDefault();
            if (author == null)
            {
                ViewBag.Result = new ViewModelResult(false, "Böyle bir kullanıcı bulunamadı");
                return_model.Username = username;
                return View(return_model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, author.FullName),
                new Claim("AuthorId", author.Id.ToString()),
                new Claim(ClaimTypes.Role, author.RoleId.ToString())
            };

            var clasim_identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var auth_properties = new AuthenticationProperties
            {
                ExpiresUtc = System.DateTimeOffset.UtcNow.AddMinutes(60),
            };

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(clasim_identity),
                auth_properties
            );

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public IActionResult _403()
        {
            return View();
        }
    }
}
