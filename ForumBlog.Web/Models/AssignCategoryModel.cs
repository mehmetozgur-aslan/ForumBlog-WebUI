﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumBlog.Web.Models
{
    public class AssignCategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool Exists { get; set; }

    }
}
