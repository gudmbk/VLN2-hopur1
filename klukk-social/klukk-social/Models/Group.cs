using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace klukk_social.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string Name { get; set; }
        //public bool OpenGroup { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public Group()
        {
            Id = 0;
            Name = String.Empty;
            Description = String.Empty;
            Date = DateTime.Now;
        }
    }
}