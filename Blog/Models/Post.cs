using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Post
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string CreatedById { get; set; }
        [ForeignKey("FK_Posts_AspNetUsers_CreatedById")]
        public IdentityUser CreatedBy { get; set; }
    }
}
