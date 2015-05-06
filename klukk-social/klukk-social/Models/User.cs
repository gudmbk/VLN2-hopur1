using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class User
    {
        private int id { get; set; }
        private int userId { get; set; }
        private string userName { get; set; }
        private string password { get; set; }
        private string firstName { get; set; }
        private string middleName { get; set; }
        private string lastName { get; set; }
        private string profilePic { get; set; }
        private DateTime birthDate { get; set; }
        private DateTime creationDate { get; set; }
    }
}