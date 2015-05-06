using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class Likes
    {
        private int id { get; set; }
        private int userId { get; set; }
        private int postId { get; set; }
        private DateTime date { get; set; }

        public Likes()
        {
            id = 0;
            userId = 0;
            postId = 0;
            date = System.DateTime.Now;
        }
    }
}