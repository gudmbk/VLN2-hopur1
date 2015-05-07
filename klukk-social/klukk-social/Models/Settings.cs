using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class Settings
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public bool Likes { get; set; }
        public bool Comments { get; set; }
        public bool Statuses { get; set; }
        public bool FriendRequests { get; set; }
    }
}