using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumBlog.Web.ApiServices.Interfaces
{
    public interface IImageApiService
    {
        Task<string> GetBlogImageByIdAsync(int id);
    }
}
