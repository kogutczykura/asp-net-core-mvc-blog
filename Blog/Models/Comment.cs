using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Comment
    {
        public long Id { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public long PostId { get; set; }
        public Post post { get; set; }

        
    }
}
