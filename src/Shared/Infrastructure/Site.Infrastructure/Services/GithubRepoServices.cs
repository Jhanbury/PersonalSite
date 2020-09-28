using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Site.Application.Infrastructure.Models;
using Site.Application.Interfaces;
using Site.Domain.Entities;

namespace Site.Infrastructure.Services
{
    public class GithubRepoServices : IGithubService
    {
        private readonly IRepository<GithubRepo, int> _repository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GithubRepoServices(IRepository<GithubRepo, int> repository, IHttpClientFactory factory, IMapper mapper, ILogger<GithubRepoServices> logger)
        {
            _logger = logger;
            _repository = repository;
            _httpClientFactory = factory;
            _mapper = mapper;
        }

        public async Task<List<GithubRepoApiResultDto>> GetAllGithubRepos(string username)
        {
            using (var client = _httpClientFactory.CreateClient("github"))
            {
                var response = await client.GetAsync($"/users/{username}/repos");
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var repos = JsonConvert.DeserializeObject<List<GithubRepoApiResultDto>>(responseString);
                return repos;
            }

            
        }

        public void AddNewRepos(IEnumerable<GithubRepoApiResultDto> newRepos, int userId)
        {
            foreach (var repo in newRepos)
            {
                var model = _mapper.Map<GithubRepo>(repo);
                model.UserId = userId;
                var createdModel = _repository.Add(model);
                _logger.LogInformation($"Github Repo added to DB: {createdModel.Name}");
            }
        }

        

        public async Task UpdateGithubReposForUser(int userId, string username)
        {
            try
            {
                var githubRepos = await GetAllGithubRepos(username);
                foreach (var repo in githubRepos.Where(x => x.Fork == false))
                {
                    if (_repository.Any(x => x.GithubId.Equals(repo.GithubId)))
                    {
                        //update
                        var existingModel = await _repository.GetSingle(x => x.GithubId.Equals(repo.GithubId));
                        var existingId = existingModel.RepoId;
                        var existingUserId = existingModel.UserId;
                        _mapper.Map(repo, existingModel);
                        existingModel.RepoId = existingId;
                        existingModel.UserId = existingUserId;
                        _logger.LogInformation(existingModel.Description);
                        _repository.Update(existingModel);
                    }
                    else
                    {
                        //add
                        var model = _mapper.Map<GithubRepo>(repo);
                        model.UserId = userId;
                        var createdModel = _repository.Add(model);
                        _logger.LogInformation($"Github Repo added to DB: {createdModel.Name}");
                    }
                }
                var dbRepos = await _repository.Get(x => x.UserId.Equals(userId));
                var itemsToRemove = CalculateItemsToRemove(githubRepos, dbRepos);
                await RemoveDeletedRepos(itemsToRemove);
                await RemoveForkedRepos(githubRepos.Where(x => x.Fork), userId);
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
        }

        private async Task RemoveForkedRepos(IEnumerable<GithubRepoApiResultDto> forkedRepos, int userId)
        {
          foreach (var forked in forkedRepos)
          {
            var model = await _repository.GetSingle(x => x.GithubId.Equals(forked.GithubId) && x.UserId.Equals(userId));
            if(model != null)
              _repository.Delete(model);
          }
        }


        public async Task RemoveDeletedRepos(IEnumerable<long> itemsToRemove)
        {
            foreach (var repo in itemsToRemove)
            {
                var model = await _repository.GetSingle(x => x.GithubId.Equals(repo));
                if (model != null)
                {
                    _repository.Delete(model);
                    //_logger.LogInformation($"Github Repo deleted to DB: {model.Description}");
                }
            }
        }


        public async Task UpdateExisting(IEnumerable<GithubRepoApiResultDto> itemsToCheckForUpdates)
        {
            foreach (var repo in itemsToCheckForUpdates)
            {
                var existingModel = await _repository.GetSingle(x => x.GithubId.Equals(repo.GithubId));
                var existingId = existingModel.RepoId;
                var existingUserId = existingModel.UserId;
                _mapper.Map(repo, existingModel);
                existingModel.RepoId = existingId;
                existingModel.UserId = existingUserId;
                _logger.LogInformation(existingModel.Description);
                _repository.Update(existingModel);
            }
        }

        public IEnumerable<long> CalculateItemsToRemove(IEnumerable<GithubRepoApiResultDto> apiResults, IEnumerable<GithubRepo> dbRecords)
        {
            var dbIds = dbRecords.Select(x => x.GithubId);
            var githubIds = apiResults.Select(x => x.GithubId);
            return dbIds.Except(githubIds);
        }

        public IEnumerable<GithubRepoApiResultDto> CalculateItemsToAdd(IEnumerable<GithubRepoApiResultDto> apiResults, IEnumerable<GithubRepo> dbRepos)
        {
            return apiResults.Where(x => !dbRepos.Any(y => y.GithubId.Equals(x.GithubId)));
        }

        public IEnumerable<GithubRepoApiResultDto> CalculateItemsToUpdate(IEnumerable<GithubRepoApiResultDto> apiResults, IEnumerable<GithubRepo> dbRecords)
        {
            return apiResults.Where(x => dbRecords.Any(y => y.GithubId.Equals(x.GithubId)));
        }
    }
}
