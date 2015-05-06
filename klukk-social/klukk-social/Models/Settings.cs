using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class Settings
    {
        private int id { get; set; }
        private int userId { get; set; }
        private bool likes { get; set; }
        private bool comments { get; set; }
        private bool statuses { get; set; }
        private bool friendRequests { get; set; }
    }
}