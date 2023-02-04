using Microsoft.AspNetCore.Mvc;

namespace Blog.WebUI.Site.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index(string slug)
        {
            return View();
        }
    }
}
