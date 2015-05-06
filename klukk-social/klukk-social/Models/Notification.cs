using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostLikeId { get; set; }
        public int PostId { get; set; }
        public int CommentLikeId { get; set; }
        public int CommentId { get; set; }
        public DateTime Date { get; set; }

        public Notification()
        {
            Id = 0;
            UserId = 0;
            PostLikeId = 0;
            PostId = 0;
            CommentId = 0;
            CommentLikeId = 0;
            Date = DateTime.Now;
        }
    }
}