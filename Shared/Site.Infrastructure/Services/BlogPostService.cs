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
        private readonly IRepository<BlogPost, string> _blogRepository;
        private readonly IRepository<User, int> _userRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        private string _blogsite = "https://www.bytesizedprogramming.com";
        private string _clientId = "ghost-frontend";
        private string _clientSecret = "1146deda3d2e";
        public BlogPostService(IRepository<BlogPost, string> blogRepository, IRepository<User, int> userRepository, IHttpClientFactory httpClientFactory, IMapper mapper, ILogger<BlogPostService> logger)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<BlogPost>> GetUserBlogPosts(int userId)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var url = $"{_blogsite}/ghost/api/v0.1/posts?client_id={_clientId}&client_secret={_clientSecret}";
                var response = await client.GetAsync(url);
                var responseString = await response.Content.ReadAsStringAsync();
                var blogs = JsonConvert.DeserializeObject<BlogApiResponse>(responseString);
                var models = blogs.Posts.Select(x => new BlogPost
                {
                    Id = x.Id,
                    Title = x.Title,
                    Url = $"{_blogsite}{x.Url}",
                    ImageUrl = $"{_blogsite}{x.ImageUrl}",
                });
                return models;
            }
        }

        public async Task UpdateBlogPostsForUser(int id)
        {
            try
            {
                var models = await GetUserBlogPosts(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
        }
    }
}