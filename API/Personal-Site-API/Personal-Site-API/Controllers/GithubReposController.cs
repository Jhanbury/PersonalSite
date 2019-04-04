using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Site.Application.GithubRepos.Models;
using Site.Application.GithubRepos.Queries.GetAllGithubRepos;

namespace Personal_Site_API.Controllers
{
    public class GithubReposController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<GithubRepoDto>>> GetAll()
        {
            var list = await Mediator.Send<List<GithubRepoDto>>(new GetAllGithubReposQuery());
            return Ok(list);
        }
    }
}