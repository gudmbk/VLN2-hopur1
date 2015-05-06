using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class Post
    {
        private int id { get; set; }
        private int fromUserId { get; set; }
        private int toUserId { get; set; }
        private int groupId { get; set; }
        private string photoUrl { get; set; }
        private string videoUrl { get; set; }
        private string text { get; set; }
        private string htmlText { get; set; }
        private DateTime date { get; set; }

        public Post()
        {
            id = 0;
            fromUserId = 0;
            toUserId = 0;
            groupId = 0;
            photoUrl = System.String.Empty;
            videoUrl = System.String.Empty;
            text = System.String.Empty;
            htmlText = System.String.Empty;
            date = DateTime.Now;

        }
    }
}