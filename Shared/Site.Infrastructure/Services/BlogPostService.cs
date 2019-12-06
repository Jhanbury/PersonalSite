using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Site.Application.Interfaces;
using Site.Domain.Entities;
using Site.Infrastructure.Models.Blogs;

namespace Site.Infrastructure.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IRepository<UserBlogPost, string> _blogRepository;
        //private readonly IRepository<User, int> _userRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        
        public BlogPostService(IRepository<UserBlogPost, string> blogRepository, IHttpClientFactory httpClientFactory, IMapper mapper, ILogger<BlogPostService> logger)
        {
            _blogRepository = blogRepository;
            //_userRepository = userRepository;
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<UserBlogPost>> GetUserBlogPosts(int userId)
        {
            using (var client = _httpClientFactory.CreateClient("dev.to"))
            {
                var url = "api/articles/me/unpublished";
                var response = await client.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();
                var blogs = JsonConvert.DeserializeObject<IEnumerable<DevtoBlogApiResponse>>(responseString);
                //todo use AutoMapper
                var models = blogs.Select(x => new UserBlogPost
                {
                    SourceId = x.Id.ToString(),
                    Source = BlogSite.DevTo,
                    Title = x.Title,
                    Url = x.Url,
                    ImageUrl = x.CoverImage,
                    Teaser = x.Description,
                    UserId = userId                    
                });
                return models;
            }
        }

        public async Task UpdateBlogPostsForUser(int id)
        {
            try
            {
                var models = await GetUserBlogPosts(id);
                await UpdateDatabase(models);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
        }

        public async Task UpdateDatabase(IEnumerable<UserBlogPost> blogs)
        {
          foreach (var blog in blogs)
          {
            if(_blogRepository.Any(x => x.SourceId.Equals(blog.BlogId) && x.Source.Equals(blog.Source)))
            {
                var model = await _blogRepository.GetSingle(x => x.BlogId.Equals(blog.BlogId));
                model.ImageUrl = blog.ImageUrl;
                model.Teaser = blog.Teaser;
                model.Title = blog.Title;
                model.Url = blog.Url;
                _blogRepository.Update(model);
            }
            else
            {
                _blogRepository.Add(blog);
            }
          }
        }

        //public IEnumerable<string> BlogsToRemove(IEnumerable<UserBlogPost> dbRepos, IEnumerable<UserBlogPost> apiRepos)
        //{
        //    var dbIds = dbRepos.Select(x => x.Id);
        //    var githubIds = apiRepos.Select(x => x.Id);
        //    return dbIds.Except(githubIds);
        //}

        //public IEnumerable<UserBlogPost> BlogsToUpdate(IEnumerable<UserBlogPost> dbRepos, IEnumerable<UserBlogPost> apiRepos)
        //{
        //    return apiRepos.Where(x => dbRepos.Any(y => y.Id.Equals(x.Id)));
        //}

        //public IEnumerable<UserBlogPost> BlogsToAdd(IEnumerable<UserBlogPost> dbRepos, IEnumerable<UserBlogPost> apiRepos)
        //{
        //    return apiRepos.Where(x => !dbRepos.Any(y => y.Id.Equals(x.Id)));
        //}

        //public void AddBlogs(IEnumerable<UserBlogPost> dbblogs, IEnumerable<UserBlogPost> apiblogs, int userId)
        //{
        //    var blogsToAdd = BlogsToAdd(dbblogs, apiblogs);
        //    foreach (var blog in blogsToAdd)
        //    {
        //        var model = _mapper.Map<UserBlogPost>(blog);
        //        model.UserId = userId;
        //        var createdModel = _blogRepository.Add(model);
        //        //_logger.LogInformation($"Github Repo added to DB: {createdModel.Name}");
        //    }
        //}

        //public async Task RemoveBlogs(IEnumerable<UserBlogPost> dbBlogs, IEnumerable<UserBlogPost> apiBlogs)
        //{
        //    var blogsToDelete = BlogsToRemove(dbBlogs, apiBlogs);
        //    foreach (var repo in blogsToDelete)
        //    {
        //        var model = await _blogRepository.GetSingle(x => x.Id.Equals(repo));
        //        if (model != null)
        //        {
        //            _blogRepository.Delete(model);
        //            //_logger.LogInformation($"Github Repo deleted to DB: {model.Description}");
        //        }
        //    }
        //}

        //public async Task UpdateBlogs(IEnumerable<UserBlogPost> dbBlogs, IEnumerable<UserBlogPost> apiBlogs)
        //{
        //    var blogsToUpdate = BlogsToUpdate(dbBlogs, apiBlogs);
        //    foreach (var repo in blogsToUpdate)
        //    {
        //        var existingModel = await _blogRepository.GetSingle(x => x.Id.Equals(repo.Id));
        //        var existingId = existingModel.Id;
        //        var existingUserId = existingModel.UserId;
        //        _mapper.Map(repo, existingModel);
        //        existingModel.Id = existingId;
        //        existingModel.UserId = existingUserId;
        //        //_logger.LogInformation(existingModel.Description);
        //        _blogRepository.Update(existingModel);
        //    }
        //}
    }
}
