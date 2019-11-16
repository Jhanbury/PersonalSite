using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Services;
using Site.Application.Interfaces;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Linq;
using AutoMapper;
using Site.Application.PlatformAccounts.Model;
using Site.Application.Videos.Models;
using Microsoft.Extensions.Configuration;

namespace Site.Infrastructure.Services
{
    public class YouTubeService : IYouTubeService
    {
        private readonly IRepository<PlatformAccount, int> repository;
        private readonly IRepository<Application.Videos.Models.Video, int> _videoRepository;
        private readonly IMapper _mapper;
        private readonly string _apiKey;
        private readonly string _appName = "Personal Site";

        public YouTubeService(IRepository<PlatformAccount, int> repository, IRepository<Application.Videos.Models.Video, int> videoRepository, IMapper mapper, IConfiguration config)
        {
            this._apiKey = config.GetValue<string>("YouTubeAPIKey");
            this._videoRepository = videoRepository;
            this.repository = repository;
            this._mapper = mapper;
        }

        public async Task UpdateYouTubeVideos(int userId)
        {
            //query for uploads playlist id
            var accounts = await GetUserAccounts(userId);
            var sourceIds = accounts.Select(x => x.PlatformId);
            var ids = string.Join(",", sourceIds);
            var channels = await GetChannelInformation("contentDetails", ids);
            foreach (var channel in channels)
            {
                var uploadPlaylistId = channel.ContentDetails.RelatedPlaylists.Uploads;
                var playListVideos = await GetPlaylistVideos(uploadPlaylistId);
                var videoIds = playListVideos.Select(x => x.Id);
                var idString = StringifyIds(videoIds);
                if (string.IsNullOrEmpty(idString))
                    continue;
                var videos = await GetVideoInformation("snippet,contentDetails,statistics", idString);
                await UpdateVideos(videos);
            }
        }

        public async Task UpdateYouTubeAccounts(int userId)
        {
            try
            {
                var accounts = await GetUserAccounts(userId);
                var sourceIds = accounts.Select(x => x.PlatformId);
                var ids = string.Join(",", sourceIds);
                var channels = await GetChannelInformation("snippet,contentDetails,statistics", ids);
                foreach (var channel in channels)
                {
                    var existingAccount = accounts.FirstOrDefault(x => x.PlatformId.Equals(channel.Id));
                    _mapper.Map(channel, existingAccount);
                    repository.Update(existingAccount);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

        }

        private async Task UpdateVideos(IEnumerable<Google.Apis.YouTube.v3.Data.Video> videos)
        {
            foreach (var video in videos)
            {
                if (_videoRepository.Any(x => x.SourceId.Equals(video.Id)))
                {
                    //update
                    var model = await _videoRepository.GetSingle(x => x.SourceId.Equals(video.Id));
                    _mapper.Map(video, model);
                    _videoRepository.Update(model);
                }
                else
                {
                    //add
                    var model = _mapper.Map<Application.Videos.Models.Video>(video);
                    _videoRepository.Add(model);
                }
            }
        }

        private async Task<IEnumerable<Channel>> GetChannelInformation(string parts, string ids)
        {
            var service = new Google.Apis.YouTube.v3.YouTubeService(new BaseClientService.Initializer
            {
                ApplicationName = "Discovery Sample",
                ApiKey = _apiKey,
            });
            var channelsRequest = service.Channels.List(parts);
            channelsRequest.Id = ids;
            var channels = await channelsRequest.ExecuteAsync();
            return channels.Items.AsEnumerable();
        }

        private async Task<IEnumerable<PlatformAccount>> GetUserAccounts(int userId)
        {
            return await repository.Get(x => x.Id.Equals(userId));
        }

        private async Task<IEnumerable<Google.Apis.YouTube.v3.Data.Video>> GetPlaylistVideos(string playlistId)
        {
            var service = new Google.Apis.YouTube.v3.YouTubeService(new BaseClientService.Initializer
            {
                ApplicationName = "Discovery Sample",
                ApiKey = _apiKey,
            });
            var playlistVideos = service.PlaylistItems.List("contentDetails");
            playlistVideos.Id = playlistId;
            var results = await playlistVideos.ExecuteAsync();
            var videoIds = results.Items.Select(x => x.ContentDetails.VideoId);
            var idString = StringifyIds(videoIds);
            if (string.IsNullOrEmpty(idString))
            {
                return Enumerable.Empty<Google.Apis.YouTube.v3.Data.Video>();
            }
            var videos = await GetVideoInformation("", idString);
            return videos;
        }

        private async Task<IEnumerable<Google.Apis.YouTube.v3.Data.Video>> GetVideoInformation(string parts, string ids)
        {
            var service = new Google.Apis.YouTube.v3.YouTubeService(new BaseClientService.Initializer
            {
                ApplicationName = "Discovery Sample",
                ApiKey = _apiKey,
            });
            var videosRequest = service.Videos.List(parts);
            videosRequest.Id = ids;
            var videos = await videosRequest.ExecuteAsync();
            return videos.Items.AsEnumerable();
        }

        private string StringifyIds(IEnumerable<string> ids)
        {
            return string.Join(",", ids);
        }
    }
}