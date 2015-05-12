﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string PosterName { get; set; }
        public int GroupId { get; set; }
        public string PhotoUrl { get; set; }
        public string VideoUrl { get; set; }
        public string Text { get; set; }
        public string HtmlText { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public List<Comment> Comments = new List<Comment>();
        public List<Likes> Likes = new List<Likes>();

        public Post()
        {
            Id = 0;
            FromUserId = String.Empty;
            ToUserId = String.Empty;
            GroupId = 0;
            PhotoUrl = String.Empty;
            VideoUrl = String.Empty;
            PosterName = String.Empty;
            Text = String.Empty;
            HtmlText = String.Empty;
            Date = DateTime.Now;
            Likes = new List<Likes>();
        }
        public Post(string toUserId, string fullName)
        {
            Id = 0;
            FromUserId = String.Empty;
            ToUserId = toUserId;
            GroupId = 0;
            PhotoUrl = String.Empty;
            VideoUrl = String.Empty;
            PosterName = fullName;
            Text = String.Empty;
            HtmlText = String.Empty;
            Date = DateTime.Now;
        }
    }
}