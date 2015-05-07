using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class Likes
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int PostId { get; set; }
        public DateTime Date { get; set; }

        public Likes()
        {
            Id = 0;
            PostId = 0;
            Date = System.DateTime.Now;
        }
    }
}