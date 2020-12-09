using ForumBlog.Web.ApiServices.Interfaces;
using ForumBlog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumBlog.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthApiService _authApiService;

        public AccountController(IAuthApiService authApiService)
        {
            _authApiService = authApiService;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> SignIn(AppUserLoginModel appUserLoginModel)
        {
            if (await _authApiService.SignIn(appUserLoginModel))
            {
                return RedirectToAction("Index", "Home", new { @area = "Admin" });
            }

            return View();
        }
    }
}
