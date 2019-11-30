using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Site.Application.Entities;
using Site.Application.GithubRepos.Models;
using Site.Application.GithubRepos.Queries.GetAllGithubRepos;
using Site.Application.Interfaces;

namespace Site.Application.Tests.GithubRepos
{
    [TestFixture]
    public class GetAllGithubReposQueryHandlerTests
    {
        private GetAllGithubReposQueryHandler _GetAllGithubReposQueryHandler;
        private Mock<IRepository<GithubRepo, int>> _repository;
        private Mock<IMapper> _mapper;
        private List<GithubRepo> getGithubRepos()
        {
            var itemsToReturn = new List<int>(){1,2,3,4,5,6,7,8,9,10};
            return itemsToReturn.Select(x => new GithubRepo
            {
                Name = $"Test Repo {x}",
                Description = $"Test Repo Description {x}",
                GithubId = x,
                Language = "C#",
                RepoId = x,
                UserId = x
            }).ToList();
        }

        [OneTimeSetUp]
        public void Setup()
        {
            _repository = new Mock<IRepository<GithubRepo, int>>();

            _repository.Setup(x => x.GetAll()).ReturnsAsync(getGithubRepos());

            _mapper = new Mock<IMapper>();
            _mapper.Setup(x => x.Map<GithubRepoDto>(It.IsAny<GithubRepo>()))
                .Returns<GithubRepo>((repo) => new GithubRepoDto()
                {
                    Name = repo.Name,
                    Description = repo.Description,
                    GithubId = repo.GithubId,
                    Language = repo.Language,
                    Id = repo.RepoId
                });

            _GetAllGithubReposQueryHandler = new GetAllGithubReposQueryHandler(_repository.Object,_mapper.Object);
        }

        [Test]
        public async Task TestGetAllRepos()
        {
            //var query = new GetAllGithubReposQuery();
            //var actual = await _GetAllGithubReposQueryHandler.Handle(query, CancellationToken.None);
            //var repos = getGithubRepos();
            //Assert.IsNotNull(actual);
            //Assert.IsInstanceOf<List<GithubRepoDto>>(actual);
            //Assert.AreEqual(repos.Count,actual.Count);

        }
    }
}
