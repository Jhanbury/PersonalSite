using System.Collections.Generic;
using System.Threading.Tasks;
using Site.Application.Entities;

namespace Site.Application.Interfaces
{
    public interface IBlogPostService
    {
        Task<IEnumerable<UserBlogPost>> GetUserBlogPosts(int userId);
        Task UpdateBlogPostsForUser(int id);
    }
}