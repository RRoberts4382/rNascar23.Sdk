using Microsoft.Extensions.Logging;
using rNascar23.Sdk.Sources.Models;
using rNascar23.Sdk.Sources.Ports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rNascar23.Sdk.TestApp
{
    public partial class Form1 : Form
    {
        private readonly IApiSourcesRepository _apiSourcesRepository;

        public Form1(IApiSourcesRepository apiSourcesRepository)
        {
            InitializeComponent();

            _apiSourcesRepository = apiSourcesRepository ?? throw new ArgumentNullException(nameof(apiSourcesRepository));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var apiSources = _apiSourcesRepository.GetApiSources();

                MessageBox.Show($"{apiSources.Count()} Api Sources Found");

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
    }
}
