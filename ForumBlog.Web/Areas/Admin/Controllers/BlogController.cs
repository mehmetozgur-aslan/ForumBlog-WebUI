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


        public async Task<IActionResult> AssignCategory(int id, [FromServices] ICategoryApiService categoryApiService)
        {
            var categories = await categoryApiService.GetAllAsync();

            var blogCategories = await _blogApiService.GetCategories(id);

            TempData["blogId"] = id;

            List<AssignCategoryModel> list = new List<AssignCategoryModel>();

            foreach (var category in categories)
            {
                AssignCategoryModel model = new AssignCategoryModel();

                model.CategoryId = category.Id;
                model.CategoryName = category.Name;
                model.Exists = blogCategories.Contains(category);

                list.Add(model);
            }

            return View(list);

        }

        [HttpPost]
        public async Task<IActionResult> AssignCategory(List<AssignCategoryModel> list)
        {

            int id = (int)TempData["blogId"];

            foreach (var item in list)
            {
                if (item.Exists)
                {
                    CategoryBlogModel model = new CategoryBlogModel();
                    model.BlogId = id;
                    model.CategoryId = item.CategoryId;
                    await _blogApiService.AddToCategoryAsync(model);
                }
                else
                {
                    CategoryBlogModel model = new CategoryBlogModel();
                    model.BlogId = id;
                    model.CategoryId = item.CategoryId;
                    await _blogApiService.RemoveFromCategoryAsync(model);
                }
            }

            return RedirectToAction("Index");
        }

    }
}
