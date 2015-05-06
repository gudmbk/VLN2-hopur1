using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace klukk_social.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public int GroupId { get; set; }
        public string PhotoUrl { get; set; }
        public string VideoUrl { get; set; }
        public string Text { get; set; }
        public string HtmlText { get; set; }
        public DateTime Date { get; set; }

        public Post()
        {
            Id = 0;
            FromUserId = 0;
            ToUserId = 0;
            GroupId = 0;
            PhotoUrl = System.String.Empty;
            VideoUrl = System.String.Empty;
            Text = System.String.Empty;
            HtmlText = System.String.Empty;
            Date = DateTime.Now;

        }
    }
}