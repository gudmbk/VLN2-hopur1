using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace klukk_social.Models
{
    [NotMapped]
    public class UserViewModel
    {
        public User Person { get; set; }
        public List<Post> Feed { get; set; }
		public List<User> AllChildren { get; set; }
    }
}
