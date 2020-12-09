using ForumBlog.Web.ApiServices.Interfaces;
using ForumBlog.Web.Filters;
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
    }
}
