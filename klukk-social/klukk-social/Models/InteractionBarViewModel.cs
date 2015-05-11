using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace klukk_social.Models
{
    [NotMapped]
    public class InteractionBarViewModel
    {
		public string PostOwner { get; set; }
		public bool IsPost { get; set; }
        public int ItemId { get; set; }

		public InteractionBarViewModel()
		{
			PostOwner = "";
			IsPost = false;
			ItemId = -1;
		}
		public InteractionBarViewModel(string postOwner, bool isPost, int itemId)
		{
			PostOwner = postOwner;
			IsPost = isPost;
			ItemId = itemId;
		}
    }
}
