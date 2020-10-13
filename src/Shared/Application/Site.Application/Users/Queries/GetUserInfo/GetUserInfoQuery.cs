using MediatR;
using Site.Application.Users.Models;

namespace Site.Application.Users.Queries.GetUserInfo
{
    public class GetUserInfoQuery : IRequest<UserDto>
    {
        public int UserId { get; set; }

        public GetUserInfoQuery(int userId)
        {
            UserId = userId;
        }
    }
}