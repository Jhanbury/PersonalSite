using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Site.Application.BlogPosts.Models;
using Site.Application.BlogPosts.Queries.GetUserBlogPosts;
using Site.Application.Entities;
using Site.Application.GithubRepos.Models;
using Site.Application.GithubRepos.Queries.GetAllGithubRepos;
using Site.Application.Interfaces;

namespace Site.Application.Tests.BlogPosts
{
    [TestFixture]
    public class GetUserBlogPostsQueryHandlerTests
    {
        private GetUserBlogPostsQueryHandler _blogPostsQueryHandler;
        private Mock<IRepository<UserBlogPost, string>> _repository;
        private Mock<IMapper> _mapper;
        private List<UserBlogPost> getBlogs()
        {
            var itemsToReturn = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            return null;
        }

        [OneTimeSetUp]
        public void Setup()
        {
            _repository = new Mock<IRepository<UserBlogPost, string>>();

            _repository.Setup(x => x.GetIncluding(It.IsAny<Expression<Func<UserBlogPost,bool>>>(), It.IsAny<Expression<Func<UserBlogPost, object>>>()))
                .ReturnsAsync(getBlogs());

            _mapper = new Mock<IMapper>();
            _mapper.Setup(x => x.Map<UserBlogPostDto>(It.IsAny<UserBlogPost>()))
                .Returns<UserBlogPost>((blog) => new UserBlogPostDto()
                {
                    Id = blog.BlogId,
                    UserId = blog.UserId,
                    Teaser = blog.Teaser,
                    Title = blog.Title,
                    Url = blog.Url,
                    ImageUrl = blog.ImageUrl
                });

            _blogPostsQueryHandler = new GetUserBlogPostsQueryHandler(_repository.Object, _mapper.Object);
        }


        [Test]
        public async Task TestGetUserBlogs()
        {
            var query = new GetUserBlogPostsQuery(1);
            var actual = await _blogPostsQueryHandler.Handle(query, CancellationToken.None);
            var blogs = getBlogs();
            Assert.IsNotNull(actual);
            Assert.IsInstanceOf<List<UserBlogPostDto>>(actual);
            Assert.AreEqual(blogs.Count, actual.Count);
        }
    }
}
