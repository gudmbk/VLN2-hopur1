using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace klukk_social.Models
{
    [NotMapped]
    public class InteractionBarViewModel
    {
		public User Person { get; set; }
		public List<Post> Feed { get; set; }
		public Comment Comment { get; set; }
		public bool IsPost { get; set; }

		public InteractionBarViewModel()
		{
			Person = new User();
			IsPost = false;
			Feed = new List<Post>();
			Comment = new Comment();
		}
    }
}
