using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Site.Application.Technologies.Models;
using Site.Application.Technologies.Queries.GetAllTechnologies;

namespace Personal_Site_API.Controllers
{
    public class TechnologyController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<TechnologyDto>>> GetAll()
        {
            var list = await Mediator.Send<List<TechnologyDto>>(new GetAllTechnologiesQuery());
            return Ok(list);
        }
    }
}