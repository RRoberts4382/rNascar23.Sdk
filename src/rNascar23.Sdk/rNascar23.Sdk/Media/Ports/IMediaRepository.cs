using rNascar23.Sdk.Common;
using rNascar23.Sdk.Media.Models;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Media.Ports
{
    public interface IMediaRepository
    {
        MediaImage GetCarNumberImage(SeriesTypes seriesId, int carNumber);
        Task<byte[]> GetCarNumberImageAsync(SeriesTypes seriesId, int carNumber, CancellationToken cancellationToken = default);
        Task<AudioConfiguration> GetAudioConfigurationAsync(SeriesTypes seriesId, CancellationToken cancellationToken = default);
        Task<VideoConfiguration> GetVideoConfigurationAsync(SeriesTypes seriesId, CancellationToken cancellationToken = default);
    }
}