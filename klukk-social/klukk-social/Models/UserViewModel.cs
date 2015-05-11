using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace klukk_social.Models
{
    [NotMapped]
    public class UserViewModel
    {
        public User Person { get; set; }

        public List<Post> Feed { get; set; }
		public readonly bool Friends;
		public List<User> AllChildren { get; set; }
		public Comment Comment { get; set; }
        public FriendRequest Request;
        
		public UserViewModel()
		{

		}
        public UserViewModel(bool friend)
        {
            Friends = friend;
        }
    }
}
