using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Site.Application.Infrastructure.Models.Twitch;
using Site.Application.Interfaces;
using Site.Application.PlatformAccounts.Commands;
using Site.Domain.Entities;
using TwitchLib.Api;
using TwitchLib.Api.V5.Models.Videos;

namespace Site.Infrastructure.Services
{
    public class TwitchService : ITwitchService
    {
        private readonly IRepository<PlatformAccount, int> _accountRepo;
        private readonly IRepository<Domain.Entities.Video, int> _videoRepository;
        private readonly TwitchAPI _twitchAPI;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _twitchApiKey;
        private readonly string _baseCallbackUrl;
        private readonly string _twitchUrl;

        public TwitchService(IConfiguration config, IRepository<PlatformAccount, int> accountRepo, IRepository<Domain.Entities.Video, int> videoRepo, IMapper mapper, IHttpClientFactory factory)
        {
            _twitchAPI = new TwitchAPI();
            _twitchApiKey = config.GetValue<string>("TwitchAPIKey");
            _twitchUrl = config.GetValue<string>("TwitchWebHookUrl");
            _twitchAPI.Settings.ClientId = _twitchApiKey;
            _baseCallbackUrl = config.GetValue<string>("TwitchBaseCallback");
            _mapper = mapper;
            _videoRepository = videoRepo;
            _accountRepo = accountRepo;
            _clientFactory = factory;
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
                        var model = _mapper.Map<Domain.Entities.Video>(video);
                        model.PlatformAccountId = account.Id;
                        _videoRepository.Add(model);
                    }
                }

                await RemoveExpiredVideos(channelVideos, userId);
            }
        }

    private async Task RemoveExpiredVideos(TwitchLib.Api.V5.Models.Videos.Video[] channelVideos, int userId)
    {
      var userChannelVideos = await 
        _videoRepository.Get(x => x.PlatformAccount.UserId.Equals(userId) && x.PlatformAccount.Platform.Equals(Domain.Enums.Platform.Twitch));
      foreach (var video in userChannelVideos)
      {
        if (!channelVideos.Any(x => x.Id.Equals(video.SourceId)))
        {
          _videoRepository.Delete(video);
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

    public UpdateAccountStreamStateCommand HandleTwitchStreamUpdateWebhook(TwitchStreamUpdateResponse response, int userId, string platformId)
    {
      if (response != null && (response?.Data == null || Equals(response.Data, Enumerable.Empty<TwitchStreamUpdateData>())))
        return new UpdateAccountStreamStateCommand()
        {
          UserId = userId,
          IsStreaming = false,
          AccountId = platformId
        };
      var streamData = response.Data.FirstOrDefault();
      var commandData = _mapper.Map<UpdateAccountStreamStateCommand>(streamData);
      commandData.UserId = userId;
      commandData.AccountId = platformId;
      return commandData;
    }


    public async Task<bool> SubscribeToTwitchStreamWebHook(TwitchSubscriptionData subscription)
    {
      using (var client = _clientFactory.CreateClient("TwitchWebhook"))
      {
        var callback = $"{_baseCallbackUrl}/api/TwitchWebhook/StreamUpdate/{subscription.UserId}/{subscription.TwitchAccountId}";
        var mode = $"subscribe";
        var topic = $"{_twitchUrl}/streams?user_id={subscription.TwitchAccountId}";
        var leaseSeconds = 0;
        
        var parameters = new TwitchWebHookParameters()
        {
          Callback = callback,
          Mode = mode,
          Topic = topic,
          Lease = leaseSeconds
        };
        client.DefaultRequestHeaders.Add("Client-ID", _twitchApiKey);
        var contentString = JsonConvert.SerializeObject(parameters);
        var content = new StringContent(contentString, Encoding.UTF8, "application/json");
        var url = $"{_twitchUrl}/webhooks/hub";
        var response = await client.PostAsync(url, content);
        return response.IsSuccessStatusCode;
      }
    }


    private async Task<IEnumerable<PlatformAccount>> GetUserAccounts(int userId)
    {
        return await _accountRepo.Get(x => x.UserId.Equals(userId) && x.Platform.Equals(Site.Domain.Enums.Platform.Twitch));
    }
  }

    
}
