using System.ComponentModel.DataAnnotations.Schema;

namespace klukk_social.Models
{
    public class GroupRequest
    {
        public int Id { get; set; }
        public string FromUserId { get; set; }
        [ForeignKey("FromUserId")]
        public virtual User FromUser { get; set; }
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Togroup { get; set; }
    }
}