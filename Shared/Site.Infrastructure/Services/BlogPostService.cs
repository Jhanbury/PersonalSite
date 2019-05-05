using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Site.Application.Entities;
using Site.Application.Infrastructure.Models;
using Site.Application.Interfaces;

namespace Site.Infrastructure.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IRepository<UserBlogPost, string> _blogRepository;
        //private readonly IRepository<User, int> _userRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        private string _blogsite = "https://www.bytesizedprogramming.com";
        private string _clientId = "ghost-frontend";
        private string _clientSecret = "1146deda3d2e";
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
            using (var client = _httpClientFactory.CreateClient())
            {
                var url = $"{_blogsite}/ghost/api/v0.1/posts?client_id={_clientId}&client_secret={_clientSecret}";
                var response = await client.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();
                var blogs = JsonConvert.DeserializeObject<BlogApiResponse>(responseString);
                //todo use AutoMapper
                var models = blogs.Posts.Select(x => new UserBlogPost
                {
                    Id = x.Id,
                    Title = x.Title,
                    Url = $"{_blogsite}{x.Url}",
                    ImageUrl = $"{_blogsite}{x.ImageUrl}",
                    Teaser = x.Teaser,
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
                var dbBlogs = await _blogRepository.Get(x => x.UserId.Equals(id));
                AddBlogs(dbBlogs,models,id);
                await RemoveBlogs(dbBlogs, models);
                await UpdateBlogs(dbBlogs, models);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
        }

        public IEnumerable<string> BlogsToRemove(IEnumerable<UserBlogPost> dbRepos, IEnumerable<UserBlogPost> apiRepos)
        {
            var dbIds = dbRepos.Select(x => x.Id);
            var githubIds = apiRepos.Select(x => x.Id);
            return dbIds.Except(githubIds);
        }

        public IEnumerable<UserBlogPost> BlogsToUpdate(IEnumerable<UserBlogPost> dbRepos, IEnumerable<UserBlogPost> apiRepos)
        {
            return apiRepos.Where(x => dbRepos.Any(y => y.Id.Equals(x.Id)));
        }

        public IEnumerable<UserBlogPost> BlogsToAdd(IEnumerable<UserBlogPost> dbRepos, IEnumerable<UserBlogPost> apiRepos)
        {
            return apiRepos.Where(x => !dbRepos.Any(y => y.Id.Equals(x.Id)));
        }

        public void AddBlogs(IEnumerable<UserBlogPost> dbblogs, IEnumerable<UserBlogPost> apiblogs, int userId)
        {
            var blogsToAdd = BlogsToAdd(dbblogs, apiblogs);
            foreach (var blog in blogsToAdd)
            {
                var model = _mapper.Map<UserBlogPost>(blog);
                model.UserId = userId;
                var createdModel = _blogRepository.Add(model);
                //_logger.LogInformation($"Github Repo added to DB: {createdModel.Name}");
            }
        }

        public async Task RemoveBlogs(IEnumerable<UserBlogPost> dbBlogs, IEnumerable<UserBlogPost> apiBlogs)
        {
            var blogsToDelete = BlogsToRemove(dbBlogs, apiBlogs);
            foreach (var repo in blogsToDelete)
            {
                var model = await _blogRepository.GetSingle(x => x.Id.Equals(repo));
                if (model != null)
                {
                    _blogRepository.Delete(model);
                    //_logger.LogInformation($"Github Repo deleted to DB: {model.Description}");
                }
            }
        }

        public async Task UpdateBlogs(IEnumerable<UserBlogPost> dbBlogs, IEnumerable<UserBlogPost> apiBlogs)
        {
            var blogsToUpdate = BlogsToUpdate(dbBlogs, apiBlogs);
            foreach (var repo in blogsToUpdate)
            {
                var existingModel = await _blogRepository.GetSingle(x => x.Id.Equals(repo.Id));
                var existingId = existingModel.Id;
                var existingUserId = existingModel.UserId;
                _mapper.Map(repo, existingModel);
                existingModel.Id = existingId;
                existingModel.UserId = existingUserId;
                //_logger.LogInformation(existingModel.Description);
                _blogRepository.Update(existingModel);
            }
        }
    }
}