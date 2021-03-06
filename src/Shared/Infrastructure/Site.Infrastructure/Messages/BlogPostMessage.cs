﻿using Site.Application.Interfaces.Messaging;

namespace Site.Infrastructure.Messages
{
    public class BlogPostMessage : Message
    {
        public int UserId { get; set; }
        public BlogPostMessage() : base("BlogPostsJob", MessageType.UserBlogPostsUpdate)
        {
        }
    }
}