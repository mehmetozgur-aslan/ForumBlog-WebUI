using ForumBlog.Web.ApiServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumBlog.Web.ViewComponents
{
    public class LastFiveBlog : ViewComponent
    {
        private readonly IBlogApiService _blogApiService;

        public LastFiveBlog(IBlogApiService blogApiService)
        {
            _blogApiService = blogApiService;
        }

        public IViewComponentResult Invoke()
        {
            return View(_blogApiService.GetLastFiveAsync().Result);
        }
    }
}
