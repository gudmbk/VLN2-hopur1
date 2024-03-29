﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace klukk_social.Models
{
    public class Friendship
    {
        public int Id { get; set; }
        public string FromUserId { get; set; }
        [ForeignKey("FromUserId")]
        public User FromUser { get; set; }
        public string ToUserId { get; set; }
        [ForeignKey("ToUserId")]
        public User ToUser { get; set; }
        public DateTime Date { get; set; }

        public Friendship()
        {
            Id = 0;
            Date = DateTime.Now;
        }
    }
}