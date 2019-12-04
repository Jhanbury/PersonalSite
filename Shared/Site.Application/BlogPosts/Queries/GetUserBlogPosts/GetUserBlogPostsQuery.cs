using System.Collections.Generic;
using MediatR;
using Site.Application.BlogPosts.Models;

namespace Site.Application.BlogPosts.Queries.GetUserBlogPosts
{
    public class GetUserBlogPostsQuery : IRequest<List<UserBlogPostDto>>
    {
        public int UserId { get; set; }

        public GetUserBlogPostsQuery(int userId)
        {
            UserId = userId;
        }
    }
}
