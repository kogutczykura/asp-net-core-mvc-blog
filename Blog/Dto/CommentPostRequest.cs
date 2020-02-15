using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Dto
{
    public class CommentPostRequest
    {
        public long PostId { get; set; }
        public string Content { get; set; }

    }
}
