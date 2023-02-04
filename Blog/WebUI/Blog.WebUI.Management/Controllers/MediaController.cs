using Blog.Data;
using Blog.Model;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Blog.WebUI.Extensions.Models;
using Blog.WebUI.Extensions.Helpers;
using Blog.WebUI.Authorize;

namespace Blog.WebUI.Extensions.Controllers
{
    [Authorize]
    public class MediaController : Controller
    {
        MediaData _mediaData;
        ContentData _contentData;

        public MediaController(MediaData _mediaData, ContentData _contentData)
        {
            this._mediaData = _mediaData;
            this._contentData = _contentData;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var medias = _mediaData.GetAll();
            return View(medias);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var media = new Model.Media();
            return View(media);
        }

        [HttpPost]
        public IActionResult Add(Model.Media media, IFormFile file)
        {
            var errors = new List<string>();

            var local_image_dir = $"wwwroot/_uploads/images";
            var local_image_path = $"{local_image_dir}/{file.FileName}";

            media.MediaUrl = $"{local_image_path}";
            media.FileSlug = Path.GetFileNameWithoutExtension(file.FileName).ToSlug();
            media.Alt = media.Alt ?? "";
            media.Title = media.Title ?? "";

            var operationResult = _mediaData.Insert(media);
            if (operationResult.IsSucceed)
            {
                ViewBag.Result = new ViewModelResult(true, "Yeni media eklendi");
                return View(new Model.Media());
            }

            ViewBag.Result = new ViewModelResult(false, operationResult.Message);
            return View(media);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var result = true;

            var media = _mediaData.GetByKey(id);
            if (media == null)
                return RedirectToAction("Index", "Media", new { q = "media-bulunamadi" });

            var media_url = media.MediaUrl;

            if (System.IO.File.Exists(media_url))
                System.IO.File.Delete(media_url);

            result = _mediaData.DeleteByKey(id).IsSucceed;

            if (result)
            {
                var contents = _contentData.GetBy(x => x.MediaId == media.Id);
                foreach (var c in contents)
                {
                    c.MediaId = -1;
                    _contentData.Update(c);
                }
            }

            return RedirectToAction("Index", "Media", new { q = "media-silindi" });
        }
    }
}
