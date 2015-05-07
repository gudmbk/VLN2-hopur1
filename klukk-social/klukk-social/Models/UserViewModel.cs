using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using klukk_social.Models;

namespace klukk_social.Models
{
    [NotMapped]
    public class UserViewModel
    {
        public User Person { get; set; }
        public List<Post> Feed { get; set; }
    }
}