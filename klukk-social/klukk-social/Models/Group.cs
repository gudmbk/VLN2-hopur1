using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class Group
    {
        private int id { get; set; }
        private int userId { get; set; }
        private string name { get; set; }
        private string description { get; set; }
        private DateTime date { get; set; }

        public Group()
        {
            id = 0;
            userId = 0;
            name = System.String.Empty;
            description = System.String.Empty;
            date = DateTime.Now;
        }
    }
}