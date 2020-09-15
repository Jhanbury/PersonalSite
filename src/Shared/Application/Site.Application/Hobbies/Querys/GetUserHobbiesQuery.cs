using System.Collections.Generic;
using MediatR;
using Site.Application.Hobbies.Model;

namespace Site.Application.Hobbies.Querys
{
    public class GetUserHobbiesQuery : IRequest<List<HobbyDto>>
    {
        public int Id { get; private set; }

        public GetUserHobbiesQuery(int id)
        {
            Id = id;
        }
    }
}
