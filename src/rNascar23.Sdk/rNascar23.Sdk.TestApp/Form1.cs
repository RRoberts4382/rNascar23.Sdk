using rNascar23.Sdk.LoopData.Ports;
using rNascar23.Sdk.Media.Ports;
using rNascar23.Sdk.Sources.Models;
using rNascar23.Sdk.Sources.Ports;
using System;
using System.Linq;
using System.Windows.Forms;

namespace rNascar23.Sdk.TestApp
{
    public partial class Form1 : Form
    {
        private readonly IApiSourcesRepository _apiSourcesRepository;
        private readonly IMediaRepository _mediaRepository;
        private readonly IDriverInfoRepository _driverInfoRepository;

        public Form1(
            IApiSourcesRepository apiSourcesRepository,
            IMediaRepository mediaRepository,
            IDriverInfoRepository driverInfoRepository)
        {
            InitializeComponent();

            _apiSourcesRepository = apiSourcesRepository ?? throw new ArgumentNullException(nameof(apiSourcesRepository));
            _mediaRepository = mediaRepository ?? throw new ArgumentNullException(nameof(mediaRepository));
            _driverInfoRepository = driverInfoRepository ?? throw new ArgumentNullException(nameof(driverInfoRepository));
        }

        private void btnApiSources_Click(object sender, EventArgs e)
        {
            try
            {
                var apiSources = _apiSourcesRepository.GetApiSources();

                MessageBox.Show($"{apiSources.Count()} api sources found");

                var sourceType = ApiSourceType.KeyMoments;

                var apiSource = _apiSourcesRepository.GetApiSource(sourceType);

                MessageBox.Show($"Got the source for {sourceType}: {apiSource.UrlTemplate}");

                int? year = 2023;
                int? seriesId = 1;
                int? raceId = 5274;

                var url = _apiSourcesRepository.GetApiUrl(sourceType, year, seriesId, raceId);

                MessageBox.Show($"Got url {url} for type:{sourceType}; year:{year}; seriesId:{seriesId}; raceId:{raceId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnAudio_Click(object sender, EventArgs e)
        {
            try
            {
                var audioConfiguration = await _mediaRepository.GetAudioConfigurationAsync(Common.SeriesTypes.Xfinity);

                MessageBox.Show($"{audioConfiguration.AudioChannels.Count()} audio channels found");

                var videoConfiguration = await _mediaRepository.GetVideoConfigurationAsync(Common.SeriesTypes.Xfinity);

                if (videoConfiguration.VideoComponents.Count > 0)
                {
                    MessageBox.Show($"{videoConfiguration.VideoComponents[0].VideosChannels.Count} video channels found");
                }
                else
                {
                    MessageBox.Show("No video channels found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnDrivers_Click(object sender, EventArgs e)
        {
            try
            {
                var drivers = await _driverInfoRepository.GetDriversAsync();

                MessageBox.Show($"{drivers.Count()} drivers found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }
    }
}
