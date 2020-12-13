using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumBlog.Web.Models
{
    public class AppUserLoginModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı alanı gereklidir.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Parola alanı gereklidir.")]
        public string Password { get; set; }
    }
}
