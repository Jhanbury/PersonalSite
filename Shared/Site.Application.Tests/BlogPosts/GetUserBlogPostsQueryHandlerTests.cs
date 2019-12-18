using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using Moq;
using NUnit.Framework;
using Site.Application.BlogPosts.Models;
using Site.Application.BlogPosts.Queries.GetUserBlogPosts;
using Site.Application.Interfaces;
using Site.Application.Users.Models;
using Site.Domain.Entities;

namespace Site.Application.Tests.BlogPosts
{
    [TestFixture]
    public class GetUserBlogPostsQueryHandlerTests
    {
        
        private GetUserBlogPostsQueryHandler _blogPostsQueryHandler;
        private Mock<IRepository<UserBlogPost, string>> _repository;
        private Mock<IMapper> _mapper;
        private IEnumerable<UserBlogPostDto> _blogs;
        //private List<UserBlogPost> getBlogs()
        //{
        //    var itemsToReturn = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        //    return null;
        //}

        [OneTimeSetUp]
        public void Setup()
        {
          
          var fakeAddress = new Faker<Address>()
            .RuleFor(x => x.Title, x => x.Random.String())
            .RuleFor(x => x.Country, x => x.Address.Country())
            .RuleFor(x => x.City, x => x.Address.City())
            .RuleFor(x => x.AddressLine1, x => x.Address.BuildingNumber())
            .RuleFor(x => x.AddressLine3, x => x.Address.StreetAddress())
            .RuleFor(x => x.AddressLine2, x => x.Address.SecondaryAddress());

          var fakeUserModel = new Faker<User>()
            .RuleFor(x => x.FirstName, x => x.Person.FirstName)
            .RuleFor(x => x.LastName, x => x.Person.LastName)
            .RuleFor(x => x.AddressId, x => x.Random.Number(3))
            .RuleFor(x => x.Address, x => fakeAddress)
            .RuleFor(x => x.PersonalStatement, x => x.Lorem.Paragraph());

          var blogModel = new Faker<UserBlogPost>()
            .RuleFor(x => x.Title, x => x.Random.String())
            .RuleFor(x => x.Teaser, x => x.Random.String())
            .RuleFor(x => x.Url, x => x.Internet.Url())
            .RuleFor(x => x.ImageUrl, x => x.Internet.Url())
            .RuleFor(x => x.UserId, x => x.Random.Number(1000))
            .RuleFor(x => x.BlogId, x => x.Random.Number(1000))
            .RuleFor(x => x.User, x => fakeUserModel)
            .RuleFor(x => x.SourceId, x => x.Random.AlphaNumeric(5));

            _repository = new Mock<IRepository<UserBlogPost, string>>();

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
            var blogs = blogModel.GenerateLazy(4);
            _blogs =  blogs.Select(x => _mapper.Object.Map<UserBlogPostDto>(x));

            _repository.Setup(x => x.GetIncluding(It.IsAny<Expression<Func<UserBlogPost,bool>>>(), It.IsAny<Expression<Func<UserBlogPost, object>>>()))
                .ReturnsAsync(blogs);
            _blogPostsQueryHandler = new GetUserBlogPostsQueryHandler(_repository.Object, _mapper.Object);
        }


        [Test]
        public async Task TestGetUserBlogs()
        {
            var query = new GetUserBlogPostsQuery(1);
            var actual = await _blogPostsQueryHandler.Handle(query, CancellationToken.None);
            
            Assert.IsNotNull(actual);
            Assert.IsInstanceOf<List<UserBlogPostDto>>(actual);
            Assert.AreEqual(_blogs.Count(), actual.Count);
        }
    }
}
