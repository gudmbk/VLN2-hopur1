using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using klukk_social.Models;
using Microsoft.AspNet.Identity;

namespace klukk_social.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string FromUserId { get; set; }
        [ForeignKey("FromUserId")]
        public User FromUser { get; set; }
        public string ToUserId { get; set; }
        [ForeignKey("ToUserId")]
        public User ToUser { get; set; }
        public int GroupId { get; set; }
        public string PhotoUrl { get; set; }
        public string VideoUrl { get; set; }
        public string Text { get; set; }
        public string HtmlText { get; set; }
        public DateTime Date { get; set; }

        public Post()
        {
            Id = 0;
            FromUserId = String.Empty;
            ToUserId = String.Empty;
            GroupId = 0;
            PhotoUrl = String.Empty;
            VideoUrl = String.Empty;
            Text = String.Empty;
            HtmlText = String.Empty;
            Date = DateTime.Now;

        }
    }
}