using System.Collections.Generic;
using System.Threading.Tasks;
using Site.Application.Entities;

namespace Site.Application.Interfaces
{
    public interface IBlogPostService
    {
        Task<IEnumerable<UserBlogPost>> GetUserBlogPosts(int userId);
        Task UpdateBlogPostsForUser(int id);
        IEnumerable<string> BlogsToRemove(IEnumerable<UserBlogPost> dbRepos, IEnumerable<UserBlogPost> apiRepos);
        IEnumerable<UserBlogPost> BlogsToUpdate(IEnumerable<UserBlogPost> dbRepos, IEnumerable<UserBlogPost> apiRepos);
        IEnumerable<UserBlogPost> BlogsToAdd(IEnumerable<UserBlogPost> dbRepos, IEnumerable<UserBlogPost> apiRepos);
        void AddBlogs(IEnumerable<UserBlogPost> dbBlogs, IEnumerable<UserBlogPost> apiBlogs, int userId);
        Task RemoveBlogs(IEnumerable<UserBlogPost> dbBlogs, IEnumerable<UserBlogPost> apiBlogs);
        Task UpdateBlogs(IEnumerable<UserBlogPost> dbBlogs, IEnumerable<UserBlogPost> apiBlogs);
    }
}