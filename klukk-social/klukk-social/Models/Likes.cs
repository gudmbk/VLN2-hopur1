using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class Likes
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime Date { get; set; }

        public Likes()
        {
            Id = 0;
            UserId = 0;
            PostId = 0;
            Date = System.DateTime.Now;
        }
    }
}