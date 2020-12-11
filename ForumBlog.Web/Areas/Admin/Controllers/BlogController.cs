using ForumBlog.Web.ApiServices.Interfaces;
using ForumBlog.Web.Filters;
using ForumBlog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogApiService _blogApiService;

        public BlogController(IBlogApiService blogApiService)
        {
            _blogApiService = blogApiService;
        }

        [JwtAuthorize]
        public async Task<IActionResult> Index()
        {
            return View(await _blogApiService.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogAddModel blogAddModel)
        {
            if (ModelState.IsValid)
            {
                await _blogApiService.AddAsync(blogAddModel);
                return RedirectToAction("Index");
            }
            return View(blogAddModel);
        }

        public async Task<IActionResult> Update(int id)
        {
            var blogList = await _blogApiService.GetByIdAsync(id);

            return View(new BlogUpdateModel()
            {
                Id = blogList.Id,
                Description = blogList.Description,
                ShortDescription = blogList.ShortDescription,
                Title = blogList.Title
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(BlogUpdateModel blogUpdateModel)
        {
            if (ModelState.IsValid)
            {
                await _blogApiService.UpdateAsync(blogUpdateModel);
                return RedirectToAction("Index");
            }
            return View(blogUpdateModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
           await _blogApiService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
