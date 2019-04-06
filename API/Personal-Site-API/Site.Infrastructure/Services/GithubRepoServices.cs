using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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

        public GithubRepoServices(IRepository<GithubRepo, int> repository, IHttpClientFactory factory)
        {
            _repository = repository;
            _httpClientFactory = factory;
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

        
    }
}