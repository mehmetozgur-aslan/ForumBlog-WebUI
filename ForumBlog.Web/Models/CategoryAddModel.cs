using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumBlog.Web.Models
{
    public class CategoryAddModel
    {
        [Required(ErrorMessage ="Kategori Ad alanı gereklidir.")]
        [Display(Name="Kategori Ad :")]
        public string Name { get; set; }

    }
}
