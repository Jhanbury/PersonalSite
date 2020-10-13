using System.Collections.Generic;
using MediatR;
using Site.Application.CareerExperience.Models;

namespace Site.Application.CareerExperience.Queries.GetUserExperience
{
    public class GetUserExperienceQuery : IRequest<List<UserExperienceDto>>
    {
        public GetUserExperienceQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }
    }
}