using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }

        public Comment()
        {
            Id = 0;
            PostId = 0;
            UserId = String.Empty;
            Date = DateTime.Now;
        }
    }
}