using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.Media.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using rNascar23.Sdk.Media.Models;
using AutoMapper;
using rNascar23.Sdk.Services.Media.Data.Models;
using rNascar23.Sdk.Service.Media.Models;

namespace rNascar23.Sdk.Service.Media
{
    internal class MediaRepository : JsonDataRepository, IMediaRepository
    {
        #region fields

        protected readonly IMapper _mapper;
        protected readonly IList<MediaImage> _mediaCache = new List<MediaImage>();

        #endregion

        #region properties

        // https://cf.nascar.com/config/audio/audio_mapping_1_3.json
        protected virtual string AudioUrl { get => @"https://cf.nascar.com/config/audio/audio_mapping_{0}_3.json"; }
        // https://cf.nascar.com/drive/1/configs-ng.json
        protected virtual string VideoUrl { get => @"https://cf.nascar.com/drive/{0}/configs-ng.json"; }
        // https://cf.nascar.com/data/images/carbadges/1/16.png
        protected virtual string CarNumberUrl { get => @"https://cf.nascar.com/data/images/carbadges/{0}/{1}.png"; }

        #endregion

        #region ctor

        public MediaRepository(
            ILogger<MediaRepository> logger,
            IMapper mapper)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region public

        public virtual async Task<byte[]> GetCarNumberImageAsync(SeriesTypes seriesId, int carNumber, CancellationToken cancellationToken = default)
        {
            try
            {
                var cached = _mediaCache.FirstOrDefault(
                    c => c.MediaType == MediaTypes.CarNumber &&
                    c.SeriesId == seriesId &&
                    c.CarNumber == carNumber); ;

                if (cached != null)
                {
                    return cached.Image;
                }

                var image = await DownloadCarNumberAsync(seriesId, carNumber);

                if (image != null)
                {
                    _mediaCache.Add(new MediaImage()
                    {
                        MediaType = MediaTypes.CarNumber,
                        SeriesId = seriesId,
                        CarNumber = carNumber,
                        Image = image
                    });

                    return image;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error loading car number media for series id {seriesId}, car number {carNumber}");
            }

            return Array.Empty<byte>();
        }

        public virtual MediaImage GetCarNumberImage(SeriesTypes seriesId, int carNumber)
        {
            try
            {
                var cached = _mediaCache.FirstOrDefault(
                    c => c.MediaType == MediaTypes.CarNumber &&
                    c.SeriesId == seriesId &&
                    c.CarNumber == carNumber); ;

                if (cached != null)
                {
                    return cached;
                }

                var image = DownloadCarNumber(seriesId, carNumber);

                if (image != null)
                {
                    var mediaImage = new MediaImage()
                    {
                        MediaType = MediaTypes.CarNumber,
                        SeriesId = seriesId,
                        CarNumber = carNumber,
                        Image = image
                    };

                    _mediaCache.Add(mediaImage);

                    return mediaImage;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error loading car number media for series id {seriesId}, car number {carNumber}");
            }

            return null;
        }

        public virtual async Task<AudioConfiguration> GetAudioConfigurationAsync(SeriesTypes seriesId, CancellationToken cancellationToken = default)
        {
            string json = String.Empty;

            try
            {
                var absoluteUrl = BuildAudioUrl(seriesId);

                json = await GetAsync(absoluteUrl).ConfigureAwait(false);

                if (string.IsNullOrEmpty(json))
                    return new AudioConfiguration();

                var model = JsonConvert.DeserializeObject<AudioConfigurationModel>(json);

                if (model != null)
                {
                    var configuration = _mapper.Map<AudioConfiguration>(model);

                    return configuration;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error reading audio configuration: {ex.Message}\r\n\r\njson: {json}\r\n");
            }

            return new AudioConfiguration();
        }

        public virtual async Task<VideoConfiguration> GetVideoConfigurationAsync(SeriesTypes seriesId, CancellationToken cancellationToken = default)
        {
            string json = String.Empty;

            try
            {
                var absoluteUrl = BuildVideoUrl(seriesId);

                json = await GetAsync(absoluteUrl).ConfigureAwait(false);

                if (string.IsNullOrEmpty(json))
                    return new VideoConfiguration();

                var model = JsonConvert.DeserializeObject<VideoConfigurationModel>(json);

                if (model != null)
                {
                    var configuration = _mapper.Map<VideoConfiguration>(model);

                    return configuration;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error reading video configuration: {ex.Message}\r\n\r\njson: {json}\r\n");
            }

            return new VideoConfiguration();
        }

        #endregion

        #region protected

        protected virtual byte[] DownloadCarNumber(SeriesTypes seriesId, int carNumber)
        {
            try
            {
                string url = BuildCarNumberImageUrl(seriesId, carNumber);

                var client = new RestClient(url);

                var request = new RestRequest(String.Empty, Method.Get);

                return client.DownloadData(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error downloading car number image: seriesId:{seriesId}, carNumber:{carNumber}");
            }

            return null;
        }

        protected virtual async Task<byte[]> DownloadCarNumberAsync(SeriesTypes seriesId, int carNumber)
        {
            try
            {
                string url = BuildCarNumberImageUrl(seriesId, carNumber);

                var client = new RestClient(url);

                var request = new RestRequest(String.Empty, Method.Get);

                return await client.DownloadDataAsync(request);
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, $"Error downloading car number image: seriesId:{seriesId}, carNumber:{carNumber}");
            }

            return Array.Empty<byte>();
        }

        protected virtual string BuildCarNumberImageUrl(SeriesTypes seriesId, int carNumber)
        {
            return String.Format(CarNumberUrl, (int)seriesId, carNumber);
        }

        protected virtual string BuildAudioUrl(SeriesTypes seriesId)
        {
            return String.Format(AudioUrl, (int)seriesId);
        }

        protected virtual string BuildVideoUrl(SeriesTypes seriesId)
        {
            return String.Format(VideoUrl, (int)seriesId);
        }

        #endregion
    }
}
