using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class Group
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public Group()
        {
            Id = 0;
            UserId = 0;
            Name = System.String.Empty;
            Description = System.String.Empty;
            Date = DateTime.Now;
        }
    }
}