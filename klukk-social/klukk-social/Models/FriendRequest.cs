using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class FriendRequest
    {
        private int id { get; set; }
        private int fromUserId { get; set; }
        private int toUserId { get; set; }
    }
}