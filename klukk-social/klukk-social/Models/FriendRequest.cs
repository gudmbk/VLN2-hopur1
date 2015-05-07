using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class FriendRequest
    {
        public int Id { get; set; }
        public string FromUserId { get; set; }
        [ForeignKey("FromUserId")]
        public User FromUser { get; set; }
        public string ToUserId { get; set; }
        [ForeignKey("ToUserId")]
        public User ToUser { get; set; }
    }
}
