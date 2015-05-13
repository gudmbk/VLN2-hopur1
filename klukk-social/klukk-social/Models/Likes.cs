using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace klukk_social.Models
{
    public class Likes
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public virtual int PostId { get; set; }
        public DateTime Date { get; set; }

        public Likes()
        {
            Id = 0;
            Date = DateTime.Now;
        }
    }
    public class CommentLikes
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public virtual int CommentId { get; set; }
        public DateTime Date { get; set; }

        public CommentLikes()
        {
            Id = 0;
            Date = DateTime.Now;
        }
    }
}
