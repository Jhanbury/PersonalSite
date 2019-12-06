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
    public class GithubRepoServiceTests
    {
        private IGithubService _githubService;
        private Mock<IRepository<GithubRepo, int>> _repository;
        private Mock<IHttpClientFactory> _httpClientFactory;
        private Mock<IMapper> _mapper;
        private Mock<HttpMessageHandler> handler;
        private Mock<ILogger<GithubRepoServices>> _logger;
        private readonly List<GithubRepoApiResultDto> _apiRepos = new List<GithubRepoApiResultDto>()
        {
            new GithubRepoApiResultDto
            {
                GithubId = 1,
                Name = "Test Repo"
            },
            new GithubRepoApiResultDto
            {
                GithubId = 2,
                Name = "Test Repo 2"
            },
        };
        private readonly List<GithubRepoApiResultDto> _apiRemoveRepos = new List<GithubRepoApiResultDto>()
        {
            new GithubRepoApiResultDto
            {
                GithubId = 1,
                Name = "Test Repo"
            },
            //new GithubRepoApiResultDto
            //{
            //    GithubId = 2,
            //    Name = "Test Repo 2"
            //},
        };
        private readonly List<GithubRepo> _dbRepos = new List<GithubRepo>()
        {
            new GithubRepo()
            {
                GithubId = 1,
                Name = "Test Repo"
            },
            new GithubRepo()
            {
                GithubId = 2,
                Name = "Test Repo 2"
            },
        };
        private readonly List<GithubRepo> _dbAddRepos = new List<GithubRepo>()
        {
            new GithubRepo()
            {
                GithubId = 1,
                Name = "Test Repo"
            },
            //new GithubRepo()
            //{
            //    GithubId = 2,
            //    Name = "Test Repo 2"
            //},
        };
        
        [OneTimeSetUp]
        public void Setup()
        {
            _repository = new Mock<IRepository<GithubRepo, int>>();
            _repository
                .Setup(x => x.Get(It.IsAny<Expression<Func<GithubRepo, bool>>>()))
                .ReturnsAsync(_dbRepos);
            _repository
                .Setup(x => x.GetSingle(It.IsAny<Expression<Func<GithubRepo, bool>>>()))
                .ReturnsAsync(new GithubRepo());
            _repository
                .Setup(x => x.Add(It.IsAny<GithubRepo>()))
                .Returns<GithubRepo>(x => x);
            _repository.Setup(x => x.Update(It.IsAny<GithubRepo>()));
            _repository.Setup(x => x.Delete(It.IsAny<GithubRepo>()));

            handler = new Mock<HttpMessageHandler>();
            handler.SetupAnyRequest()
                .ReturnsResponse(JsonConvert.SerializeObject(_apiRepos), "application/json");
            var clientFactory = handler.CreateClientFactory();
            
            _mapper = new Mock<IMapper>();
            _mapper.Setup(x => x.Map<GithubRepo>(It.IsAny<GithubRepoApiResultDto>()))
                .Returns(new GithubRepo());
            _logger = new Mock<ILogger<GithubRepoServices>>();
            //_logger.Setup(x => x.LogInformation(It.IsAny<string>()));
            _githubService = new GithubRepoServices(_repository.Object, clientFactory, _mapper.Object,_logger.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _repository.Invocations.Clear();
        }
        [Test]
        [TestCase(1,"JHanbury")]
        public async Task TestUpdateGithubRepos_Update(int id, string user)
        {
            await _githubService.UpdateGithubReposForUser(id, user);
            _repository.Verify(x => x.Get(It.IsAny<Expression<Func<GithubRepo, bool>>>()), Times.Once);
            _repository.Verify(x => x.Update(It.IsAny<GithubRepo>()), Times.Once);
        }

        [Test]
        [TestCase(1,"JHanbury")]
        public async Task TestUpdateGithubRepos_AddRepo(int id, string user)
        {
            AddRepoSetup();
            await _githubService.UpdateGithubReposForUser(id, user);
            _repository.Verify(x => x.Get(It.IsAny<Expression<Func<GithubRepo, bool>>>()), Times.Once);
            _repository.Verify(x => x.Add(It.IsAny<GithubRepo>()), Times.Once);
        }

        [Test]
        [TestCase(1,"JHanbury")]
        public async Task TestUpdateGithubRepos_DeleteRepo(int id, string user)
        {
            DeleteRepoSetup();
            await _githubService.UpdateGithubReposForUser(id, user);
            _repository.Verify(x => x.Get(It.IsAny<Expression<Func<GithubRepo, bool>>>()), Times.Once);
            _repository.Verify(x => x.Delete(It.IsAny<GithubRepo>()), Times.Once);
        }

        public void AddRepoSetup()
        {
            _repository
                .Setup(x => x.Get(It.IsAny<Expression<Func<GithubRepo, bool>>>()))
                .ReturnsAsync(_dbAddRepos);
            _githubService = new GithubRepoServices(_repository.Object, handler.CreateClientFactory(), _mapper.Object, _logger.Object);
        }
        public void DeleteRepoSetup()
        {
            _repository
                .Setup(x => x.Get(It.IsAny<Expression<Func<GithubRepo, bool>>>()))
                .ReturnsAsync(_dbRepos);
            handler.SetupAnyRequest()
                .ReturnsResponse(JsonConvert.SerializeObject(_apiRemoveRepos), "application/json");
            var clientFactory = handler.CreateClientFactory();

            _githubService = new GithubRepoServices(_repository.Object, handler.CreateClientFactory(), _mapper.Object, _logger.Object);
        }

        [Test]
        public void TestCalculateItemsToAdd()
        {
            var actual =_githubService.CalculateItemsToAdd(_apiRepos, _dbAddRepos);
            Assert.AreEqual(1,actual.Count());
        }
        [Test]
        public void TestCalculateItemsToRemove()
        {
            var actual =_githubService.CalculateItemsToRemove(_apiRemoveRepos, _dbRepos);
            Assert.AreEqual(1,actual.Count());
        }

        [Test]
        public void TestCalculateItemsToUpdate()
        {
            var actual = _githubService.CalculateItemsToUpdate(_apiRepos, _dbRepos);
            Assert.AreEqual(2, actual.Count());
        }

        [Test]
        public void TestAddRepos()
        {
            _githubService.AddNewRepos(_apiRepos,1);
            _repository.Verify(x => x.Add(It.IsAny<GithubRepo>()),Times.Exactly(_apiRepos.Count));
        }
        [Test]
        public async Task TestRemoveRepos()
        {
            var reposToDelete = new List<long> {1, 2, 3};
            await _githubService.RemoveDeletedRepos(reposToDelete);
            _repository.Verify(x => x.GetSingle(It.IsAny<Expression<Func<GithubRepo,bool>>>()),Times.Exactly(reposToDelete.Count));
            _repository.Verify(x => x.Delete(It.IsAny<GithubRepo>()),Times.Exactly(reposToDelete.Count));
        }


    }
}
