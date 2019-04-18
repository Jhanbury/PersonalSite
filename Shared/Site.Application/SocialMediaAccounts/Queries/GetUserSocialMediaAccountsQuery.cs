using System.Collections.Generic;
using MediatR;
using Site.Application.SocialMediaAccounts.Models;

namespace Site.Application.SocialMediaAccounts.Queries
{
    public class GetUserSocialMediaAccountsQuery : IRequest<IEnumerable<SocialMediaAccountDto>>
    {
        public int UserId { get; set; }

        public GetUserSocialMediaAccountsQuery(int userId)
        {
            UserId = userId;
        }
    }
}