using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Contrib.HttpClient;
using Newtonsoft.Json;
using NUnit.Framework;
using Site.Application.Infrastructure.Models;
using Site.Application.Interfaces;
using Site.Domain.Entities;
using Site.Infrastructure.Services;

namespace Site.Infrastructure.Tests
{
    [TestFixture]
    public class BlogPostsServiceTests
    {
        private IBlogPostService _blogPostService;
        private Mock<IRepository<UserBlogPost, string>> _blogRepository;
        //private readonly IRepository<User, int> _userRepository;
        //private readonly Mock<IHttpClientFactory> _httpClientFactory;
        private Mock<IMapper> _mapper;
        private Mock<ILogger<BlogPostService>> _logger;
        private IEnumerable<UserBlogPost> _dbPosts = new List<UserBlogPost>()
        {
            new UserBlogPost()
            {
                BlogId = 1
            },
            new UserBlogPost()
            {
                BlogId = 2
            },
        };
        private IEnumerable<UserBlogPost> _dbRemovePosts = new List<UserBlogPost>()
        {
            new UserBlogPost(){
                BlogId = 1
            },
            new UserBlogPost(){
                BlogId = 2
            },
            new UserBlogPost(){
                BlogId = 3
            },
        };
        private BlogApiResponse _apiResponse = new BlogApiResponse()
        {
            
            Posts = new List<BlogPostResponse>
            {
                new BlogPostResponse()
                {
                    Id = "1"
                },
                new BlogPostResponse(){
                    Id = "2"
                },
                new BlogPostResponse(){
                    Id = "3"
                },
            }
        };
        private BlogApiResponse _removeApiResponse = new BlogApiResponse()
        {
            Posts = new List<BlogPostResponse>
            {
                new BlogPostResponse(){
                    Id = "1"
                },new BlogPostResponse(){
                    Id = "2"
                },
            }
        };

        [TearDown]
        public void TearDown()
        {
            _blogRepository.Invocations.Clear();
        }

        [OneTimeSetUp]
        public void Setup()
        {
            _blogRepository = new Mock<IRepository<UserBlogPost, string>>();
            _blogRepository
                .Setup(x => x.Get(It.IsAny<Expression<Func<UserBlogPost, bool>>>()))
                .ReturnsAsync(_dbPosts);
            _blogRepository
                .Setup(x => x.GetSingle(It.IsAny<Expression<Func<UserBlogPost, bool>>>()))
                .ReturnsAsync(new UserBlogPost());
            _blogRepository.Setup(x => x.Add(It.IsAny<UserBlogPost>())).Returns(new UserBlogPost());
            _blogRepository.Setup(x => x.Update(It.IsAny<UserBlogPost>()));
            _blogRepository.Setup(x => x.Delete(It.IsAny<UserBlogPost>()));
            _mapper = new Mock<IMapper>();
            _mapper.Setup(x => x.Map<UserBlogPost>(It.IsAny<BlogApiResponse>())).Returns(new UserBlogPost());
            _mapper.Setup(x => x.Map<UserBlogPost>(It.IsAny<UserBlogPost>())).Returns(new UserBlogPost());
            _logger = new Mock<ILogger<BlogPostService>>();
            _logger.SetupAllProperties();
            var handler = new Mock<HttpMessageHandler>();
            handler.SetupAnyRequest().ReturnsResponse(JsonConvert.SerializeObject(_apiResponse), "application/json");
            var clientFactory = handler.CreateClientFactory();
            _blogPostService = new BlogPostService(_blogRepository.Object,clientFactory,_mapper.Object,_logger.Object);

        }

        //[Test]
        //public async Task TestGetUserBlogPosts()
        //{
        //    var actual = await _blogPostService.GetUserBlogPosts(1);
        //    Assert.IsNotNull(actual);
        //    Assert.IsInstanceOf<IEnumerable<UserBlogPost>>(actual);
        //    Assert.AreEqual(actual.Count(),_apiResponse.Posts.Count);
        //}

        [Test]
        public async Task TestUpdateBlogPosts_AddItems()
        {
            SetupAddData();
            await _blogPostService.UpdateBlogPostsForUser(1);
            _blogRepository.Verify(x => x.Add(It.IsAny<UserBlogPost>()), Times.Once);

        }

        private void SetupAddData()
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.SetupAnyRequest().ReturnsResponse(JsonConvert.SerializeObject(_apiResponse), "application/json");
            var clientFactory = handler.CreateClientFactory();
            _blogRepository
                .Setup(x => x.Get(It.IsAny<Expression<Func<UserBlogPost, bool>>>()))
                .ReturnsAsync(_dbPosts);
            _blogPostService = new BlogPostService(_blogRepository.Object, clientFactory, _mapper.Object, _logger.Object);
        }

        [Test]
        public async Task TestUpdateBlogPosts_RemoveItems()
        {
            SetupRemoveData();
            await _blogPostService.UpdateBlogPostsForUser(1);
            _blogRepository.Verify(x => x.Delete(It.IsAny<UserBlogPost>()),Times.Once);

        }

        private void SetupRemoveData()
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.SetupAnyRequest().ReturnsResponse(JsonConvert.SerializeObject(_removeApiResponse), "application/json");
            var clientFactory = handler.CreateClientFactory();
            _blogRepository
                .Setup(x => x.Get(It.IsAny<Expression<Func<UserBlogPost, bool>>>()))
                .ReturnsAsync(_dbRemovePosts);
            _blogPostService = new BlogPostService(_blogRepository.Object, clientFactory, _mapper.Object, _logger.Object);
        }

        [Test]
        public async Task TestUpdateBlogPosts_UpdateItems()
        {
            SetupUpdateData();
            await _blogPostService.UpdateBlogPostsForUser(1);
            _blogRepository.Verify(x => x.Update(It.IsAny<UserBlogPost>()), Times.Exactly(_dbPosts.Count()));


        }

        private void SetupUpdateData()
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.SetupAnyRequest().ReturnsResponse(JsonConvert.SerializeObject(_removeApiResponse), "application/json");
            var clientFactory = handler.CreateClientFactory();
            _blogRepository
                .Setup(x => x.Get(It.IsAny<Expression<Func<UserBlogPost, bool>>>()))
                .ReturnsAsync(_dbPosts);
            _blogPostService = new BlogPostService(_blogRepository.Object, clientFactory, _mapper.Object, _logger.Object);
        }
    }
}
