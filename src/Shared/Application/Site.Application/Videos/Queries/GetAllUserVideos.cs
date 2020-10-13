using System.Collections.Generic;
using MediatR;
using Site.Application.PlatformAccounts.Model;
using Site.Application.Videos.Models;

namespace Site.Application.Videos.Queries
{
    public class GetAllUserVideos : IRequest<List<AccountDto>>
    {
        public GetAllUserVideos(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }
    }
    
}
