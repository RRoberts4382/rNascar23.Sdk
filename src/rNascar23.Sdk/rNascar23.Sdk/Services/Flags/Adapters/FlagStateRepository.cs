using AutoMapper;
using Microsoft.Extensions.Logging;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.Flags.Models;
using rNascar23.Sdk.Flags.Ports;
using rNascar23.Sdk.Service.Flags.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace rNascar23.Sdk.Service.Flags.Adapters
{
    internal class FlagStateRepository : JsonDataRepository, IFlagStateRepository
    {
        #region fields

        protected readonly IMapper _mapper;

        #endregion

        #region properties

        protected virtual string Url { get => @"https://cf.nascar.com/live/feeds/live-flag-data.json"; }

        #endregion

        #region ctor

        public FlagStateRepository(IMapper mapper, ILogger<FlagStateRepository> logger)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region public

        public virtual async Task<IEnumerable<FlagState>> GetFlagStatesAsync(
            int? skip = null,
            int? take = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var json = await GetAsync(Url, cancellationToken).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(json))
                {
                    var model = Newtonsoft.Json.JsonConvert.DeserializeObject<FlagStateModel[]>(json);

                    if (model != null)
                    {
                        var flags = _mapper.Map<List<FlagState>>(model);

                        var enumerable = flags as IEnumerable<FlagState>;

                        if (skip.HasValue)
                            enumerable = enumerable.Skip(skip.Value);

                        if (take.HasValue)
                            enumerable = enumerable.Take(take.Value);

                        return enumerable;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
            }

            return new List<FlagState>();
        }

        #endregion
    }
}
