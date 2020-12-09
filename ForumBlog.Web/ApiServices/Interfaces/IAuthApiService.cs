using ForumBlog.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumBlog.Web.ApiServices.Interfaces
{
    public interface IAuthApiService
    {
        Task<bool> SignIn(AppUserLoginModel model);
    }
}
