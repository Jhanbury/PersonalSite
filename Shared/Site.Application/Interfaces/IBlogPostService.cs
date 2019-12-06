using System.Threading.Tasks;

namespace Site.Application.Interfaces
{
    public interface IBlogPostService
    {        
        Task UpdateBlogPostsForUser(int id);    
        
    }
}
