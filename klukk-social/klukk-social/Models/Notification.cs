using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int PostLikeId { get; set; }
        public int PostId { get; set; }
        public int CommentLikeId { get; set; }
        public int CommentId { get; set; }
        public DateTime Date { get; set; }

        public Notification()
        {
            Id = 0;
            PostLikeId = 0;
            PostId = 0;
            CommentId = 0;
            CommentLikeId = 0;
            Date = DateTime.Now;
        }
    }
}