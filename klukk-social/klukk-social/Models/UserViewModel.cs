using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace klukk_social.Models
{
    [NotMapped]
    public class UserViewModel
    {
        public User Person { get; set; }
        public readonly bool friends;
        public List<Post> Feed  = new List<Post>();
		public List<User> AllChildren = new List<User>();

        public UserViewModel(bool friend)
        {
            friends = friend;
        }
    }
}
