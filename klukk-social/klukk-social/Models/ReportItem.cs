using System;

namespace klukk_social.Models
{
    public class ReportItem
    {
        public int Id { get; set; }
        public bool IsPost { get; set; }
        public virtual string ParentId { get; set; }
        public virtual Post PostItem { get; set; }
        public virtual Comment CommentItem { get; set; }
        public DateTime Date { get; set; }
    }
}