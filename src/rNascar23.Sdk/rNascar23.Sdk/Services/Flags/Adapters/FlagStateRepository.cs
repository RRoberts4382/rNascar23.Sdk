using AutoMapper;
using Microsoft.Extensions.Logging;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.Data;
using rNascar23.Sdk.Flags.Models;
using rNascar23.Sdk.Flags.Ports;
using rNascar23.Sdk.Service.Flags.Data.Models;
using rNascar23.Sdk.Sources.Models;
using rNascar23.Sdk.Sources.Ports;
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
        protected readonly IApiSourcesRepository _apiSourcesRepository;

        #endregion

        #region ctor

        public FlagStateRepository(
            IMapper mapper,
            ILogger<FlagStateRepository> logger,
            IApiSourcesRepository apiSourcesRepository)
            : base(logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _apiSourcesRepository = apiSourcesRepository ?? throw new ArgumentNullException(nameof(apiSourcesRepository));
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
                var url = _apiSourcesRepository.GetApiUrl(ApiSourceType.FlagState);

                var json = await GetAsync(url, cancellationToken).ConfigureAwait(false);

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
