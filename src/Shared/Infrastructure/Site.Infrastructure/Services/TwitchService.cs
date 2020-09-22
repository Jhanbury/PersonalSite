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
using Site.Infrastructure.Models;
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
        private readonly string _twitchAppId;
        private readonly string _twitchAppSecret;
        private readonly string _baseCallbackUrl;
        private readonly string _twitchAuthUrl;
        private readonly string _twitchWebhookUrl;

        public TwitchService(IConfiguration config, IRepository<PlatformAccount, int> accountRepo, IRepository<Domain.Entities.Video, int> videoRepo, IMapper mapper, IHttpClientFactory factory)
        {
            _twitchAPI = new TwitchAPI();
            _twitchAppId = config.GetValue<string>("Twitch:App:Id");
            _twitchAppSecret = config.GetValue<string>("Twitch:App:Secret");
            _twitchWebhookUrl = config.GetValue<string>("Twitch:Endpoints:APIUrl");
            _twitchAuthUrl = config.GetValue<string>("Twitch:Endpoints:AuthUrl");
            _baseCallbackUrl = config.GetValue<string>("Twitch:Endpoints:CallbackUrl");
            _twitchAPI.Settings.ClientId = _twitchAppId;
            //_baseCallbackUrl = config.GetValue<string>("TwitchBaseCallback")?? "https://site-endpoints.azurewebsites.net";
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
            if (response != null && response.Data != null && response.Data.Any())
            {
                var streamData = response.Data.FirstOrDefault();
                var commandData = _mapper.Map<UpdateAccountStreamStateCommand>(streamData);
                commandData.UserId = userId;
                commandData.AccountId = platformId;
                return commandData;
            }
            else
            {
                return new UpdateAccountStreamStateCommand()
                {
                    UserId = userId,
                    IsStreaming = false,
                    AccountId = platformId
                };
            }


        }


        public async Task<bool> SubscribeToTwitchStreamWebHook(TwitchSubscriptionData subscription)
        {
            var token = await GetAppToken();
            using (var client = _clientFactory.CreateClient("twitch"))
            {
                var callback = $"{_baseCallbackUrl}/api/twitch/streamupdate/{subscription.UserId}/{subscription.TwitchAccountId}";
                var mode = $"subscribe";
                var topic = $"{_twitchWebhookUrl}/helix/streams?user_id={subscription.TwitchAccountId}";
                var leaseSeconds = 864000;

                var parameters = new TwitchWebHookParameters()
                {
                    Callback = callback,
                    Mode = mode,
                    Topic = topic,
                    Lease = leaseSeconds
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var contentString = JsonConvert.SerializeObject(parameters);
                var content = new StringContent(contentString, Encoding.UTF8, "application/json");
                var url = "helix/webhooks/hub";
                var response = await client.PostAsync(url, content);
                return response.IsSuccessStatusCode;
            }
        }

        private async Task<string> GetAppToken()
        {

            var grantType = "client_credentials";
            using (var client = _clientFactory.CreateClient())
            {
                var url =
                    $"{_twitchAuthUrl}/oauth2/token?client_id={_twitchAppId}&client_secret={_twitchAppSecret}&grant_type={grantType}";
                var response = await client.PostAsync(url, new MultipartFormDataContent());
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var reponseData = JsonConvert.DeserializeObject<TwitchAuthResponse>(data);
                return reponseData.Access_Token;

            }
        }

        private async Task<IEnumerable<PlatformAccount>> GetUserAccounts(int userId)
        {
            return await _accountRepo.Get(x => x.UserId.Equals(userId) && x.Platform.Equals(Site.Domain.Enums.Platform.Twitch));
        }
    }


}
