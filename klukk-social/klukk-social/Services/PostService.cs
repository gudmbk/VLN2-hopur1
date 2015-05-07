using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using klukk_social.Models;

namespace klukk_social.Services
{
    public class PostService
    {
        public List<Post> GetAllPosts()
        {
            List<Post> listi = new List<Post>();
            Post p = new Post();
            p.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce quis arcu a ex egestas pharetra a a magna. Curabitur tristique, ligula non mattis rhoncus, dui orci interdum nisl, et interdum diam mi et erat.";
			p.Date = DateTime.Now;
			for (int i = 0; i < 10; i++)
            {
                listi.Add(p);
            }
            return listi;
        }
    }
}