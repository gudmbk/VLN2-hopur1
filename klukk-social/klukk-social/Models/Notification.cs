using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class Notification
    {
        private int id { get; set; }
        private int userId { get; set; }
        private int postLikeId { get; set; }
        private int postId { get; set; }
        private int commentLikeId { get; set; }
        private int commentId { get; set; }
        private DateTime date { get; set; }

        public Notification()
        {
            id = 0;
            userId = 0;
            postLikeId = 0;
            postId = 0;
            commentId = 0;
            commentLikeId = 0;
            date = DateTime.Now;
        }
    }
}