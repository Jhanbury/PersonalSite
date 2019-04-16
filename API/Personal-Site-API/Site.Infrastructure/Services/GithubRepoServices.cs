using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Site.Application.Entities;
using Site.Application.GithubRepos.Models;
using Site.Application.Interfaces;

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

        public async Task<List<GithubRepoDto>> GetAllGithubRepos(string username)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent","Personal-Site");
                var response = await client.GetAsync($"https://api.github.com/users/{username}/repos");
                var responseString = await response.Content.ReadAsStringAsync();
                var repos = JsonConvert.DeserializeObject<List<GithubRepoDto>>(responseString);
                return repos;
            }

            
        }

        private void AddNewRepos(IEnumerable<GithubRepoDto> newRepos, int userId)
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
                var dbRepos = await _repository.Get(x => x.UserId.Equals(userId));
                var enumerable = dbRepos.ToList();
                var dbIds = dbRepos.Select(x => x.GithubId);
                var githubIds = githubRepos.Select(x => x.GithubId);
                var itemsToAdd = githubRepos.Where(x => !enumerable.Any(y => y.GithubId.Equals(x.GithubId)));
                var itemsToRemove = dbIds.Except(githubIds);
                var itemsToCheckForUpdates = githubRepos.Where(x => enumerable.Any(y => y.GithubId.Equals(x.GithubId)));
                //_logger.LogInformation($"Items To Add:{itemsToAdd?.Count() ?? 0}");
                //_logger.LogInformation($"Items To Remove:{itemsToRemove?.Count() ?? 0}");
                //_logger.LogInformation($"Items To Update:{itemsToCheckForUpdates?.Count() ?? 0}");
                await RemoveDeletedRepos(itemsToRemove);
                AddNewRepos(itemsToAdd, userId);
                await UpdateExisting(itemsToCheckForUpdates);
                //await RemoveDeletedRepos(itemsToRemove);

            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }
            
        }

        
        private async Task RemoveDeletedRepos(IEnumerable<long> itemsToRemove)
        {
            foreach (var repo in itemsToRemove)
            {
                var model = await _repository.GetSingle(x => x.GithubId.Equals(repo));
                if (model != null)
                {
                    _repository.Delete(model);
                    _logger.LogInformation($"Github Repo deleted to DB: {model.Description}");
                }
            }
        }


        private async Task UpdateExisting(IEnumerable<GithubRepoDto> itemsToCheckForUpdates)
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


    }
}