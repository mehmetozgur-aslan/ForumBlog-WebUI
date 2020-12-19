using ForumBlog.Web.ApiServices.Interfaces;
using ForumBlog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumBlog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogApiService _blogApiService;

        public HomeController(IBlogApiService blogApiService)
        {
            _blogApiService = blogApiService;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            if (categoryId.HasValue)
            {
                ViewBag.ActiveCategory = categoryId;
                return View(await _blogApiService.GetAllByCategoryIdAsync((int)categoryId));
            }

            return View(await _blogApiService.GetAllAsync());
        }

        public async Task<IActionResult> BlogDetail(int id)
        {
            ViewBag.Comments = await _blogApiService.GetCommentAsync(id, null);
            return View(await _blogApiService.GetByIdAsync(id));
        }

        public async Task<IActionResult> AddToComment(CommentAddModel commentAddModel)
        {
            await _blogApiService.AddToComment(commentAddModel);
            return RedirectToAction("BlogDetail", new { id = commentAddModel.BlogId });
        }
    }
}
