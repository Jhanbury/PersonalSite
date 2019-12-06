using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Site.Application.Interfaces;
using Site.Domain.Entities;
using TwitchLib.Api;

namespace Site.Infrastructure.Services
{
    public class TwitchService : ITwitchService
    {
        private readonly IRepository<PlatformAccount, int> _accountRepo;
        private readonly IRepository<Video, int> _videoRepository;
        private readonly TwitchAPI _twitchAPI;
        private readonly IMapper _mapper;

        public TwitchService(IConfiguration config, IRepository<PlatformAccount, int> accountRepo, IRepository<Video, int> videoRepo, IMapper mapper)
        {
            _twitchAPI = new TwitchAPI();
            _twitchAPI.Settings.ClientId = config.GetValue<string>("TwitchAPIKey");
            _mapper = mapper;
            _videoRepository = videoRepo;
            _accountRepo = accountRepo;
        }

        public async Task UpdateTwitchVideos(int userId)
        {
            var accounts = await GetUserAccounts(userId);
            foreach (var account in accounts)
            {
                var channelVideosResult = await _twitchAPI.V5.Channels.GetChannelVideosAsync(account.PlatformId);
                var channelVideos = channelVideosResult.Videos;
                foreach (var video in channelVideos)
                {
                    if (_videoRepository.Any(x => x.SourceId.Equals(video.Id)))
                    {
                        var model = await _videoRepository.GetSingle(x => x.SourceId.Equals(video.Id));
                        _mapper.Map(video, model);
                        _videoRepository.Update(model);
                    }
                    else
                    {
                        var model = _mapper.Map<Video>(video);
                        model.PlatformAccountId = account.Id;
                        _videoRepository.Add(model);
                    }
                }
            }
        }

        public async Task UpdateTwitchAccounts(int userId)
        {
            var accounts = await GetUserAccounts(userId);
            foreach (var account in accounts)
            {
                var channel = await _twitchAPI.V5.Channels.GetChannelByIDAsync(account.PlatformId);
                _mapper.Map(channel, account);
                _accountRepo.Update(account);
            }
            

        }

        private async Task<IEnumerable<PlatformAccount>> GetUserAccounts(int userId)
        {
            return await _accountRepo.Get(x => x.UserId.Equals(userId) && x.Platform.Equals(Site.Domain.Enums.Platform.Twitch));
        }
    }
}
