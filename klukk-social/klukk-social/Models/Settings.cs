using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class Settings
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Likes { get; set; }
        public bool Comments { get; set; }
        public bool Statuses { get; set; }
        public bool FriendRequests { get; set; }
    }
}