using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class Friendship
    {
        private int id { get; set; }
        private int fromUserId { get; set; }
        private int toUserId { get; set; }
        private DateTime date { get; set; }

        public Friendship()
        {
            id = 0;
            fromUserId = 0;
            toUserId = 0;
            date = DateTime.Now;
        }
    }
}