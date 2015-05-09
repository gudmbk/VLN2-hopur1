using System.ComponentModel.DataAnnotations.Schema;

namespace klukk_social.Models
{
    public class GroupUsers
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}