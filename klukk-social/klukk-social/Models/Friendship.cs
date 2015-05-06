using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class Friendship
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public DateTime Date { get; set; }

        public Friendship()
        {
            Id = 0;
            FromUserId = 0;
            ToUserId = 0;
            Date = DateTime.Now;
        }
    }
}