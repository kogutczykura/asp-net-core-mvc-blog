using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Post
    {
        public long Id { get; set; }
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public string Description { get; set; }
        public string CreatedById { get; set; }
        public IdentityUser CreatedBy { get; set; }
        public string ImageUrl { get; set; }
    }
}
