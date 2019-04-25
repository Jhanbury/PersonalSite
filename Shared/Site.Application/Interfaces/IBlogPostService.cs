using System.Collections.Generic;
using System.Threading.Tasks;
using Site.Application.Entities;

namespace Site.Application.Interfaces
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPost>> GetUserBlogPosts(int userId);
        Task UpdateBlogPostsForUser(int id);
    }
}