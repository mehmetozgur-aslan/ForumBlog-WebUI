using ForumBlog.Web.ApiServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumBlog.Web.ViewComponents
{
    public class GetCategoryName : ViewComponent
    {

        private readonly ICategoryApiService _categoryApiService;

        public GetCategoryName(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        public IViewComponentResult Invoke(int categoryId)
        {
            return View(_categoryApiService.GetByIdAsync(categoryId).Result);
        }
    }
}
