﻿using rNasar23Multi.Sdk.Data;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.LoopData.Models;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.LoopData.Ports
{
    public interface ILoopDataRepository : IJsonDataRepository
    {
        Task<EventLoopData> GetLoopDataAsync(SeriesTypes seriesId, int raceId, int? year = null, CancellationToken cancellationToken = default);
    }
}
