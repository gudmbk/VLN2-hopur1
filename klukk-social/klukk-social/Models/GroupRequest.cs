using System.ComponentModel.DataAnnotations.Schema;

namespace klukk_social.Models
{
    public class GroupRequest
    {
        public int Id { get; set; }
        public string FromUserId { get; set; }
        [ForeignKey("FromUserId")]
        public User FromUser { get; set; }
        public string GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Togroup { get; set; }
    }
}