using Blog.Data;
using Blog.WebUI.Infrastructure.Cache;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Blog.WebUI.Site.Controllers
{
    public class HomeController : Controller
    {
        CacheHelper cacheHelper;
        public HomeController(CacheHelper cacheHelper)
        {
            this.cacheHelper = cacheHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult ClearCache()
        {
            cacheHelper.SettingClear();
            cacheHelper.NewContentsClear();
            cacheHelper.RolePageClear();
            cacheHelper.AuthorsClear();
            cacheHelper.CategoriesClear();

            return Json(true);
        }
    }
}
