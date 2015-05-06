using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class GroupUsers
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }
}