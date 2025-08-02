using KpokPatagon.Cache;
using KpokPatagon.Timing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace KpokPatagon.Multitenancy
{
    /// <summary>
    /// Implements an <see cref="ITenantProvider"/> based on the <see cref="Tenant"/> model.
    /// </summary>
    public sealed class TenantProvider : ITenantProvider
    {
        readonly IServiceProvider _serviceProvider;
        readonly TenantDataService _dataService;
        readonly TenantProviderOptions _options;

        /// <summary>
        /// Initializes a new instance of <see cref="TenantProvider"/>.
        /// </summary>
        public TenantProvider(
            TenantDataService dataService,
            IServiceProvider serviceProvider, 
            IOptions<TenantProviderOptions> options)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _options = options?.Value ?? new TenantProviderOptions();
        }

        string ComputeCacheKey(string name) => $"{_options.CacheKeyPrefix ?? "tp-"}{name.ToLowerInvariant()}";

        /// <summary>
        /// Gets the tenant with the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the tenant to retrieve.</param>
        /// <returns>
        ///     A <see cref="Tenant"/> or <see langworg="null"/> if not found.
        /// </returns>
        public Tenant GetTenant(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return default;
            }

            var cacheKey = ComputeCacheKey(name);

            var cache = _serviceProvider.GetService<ICacheService>();
            if (cache != null && cache.Instance.TryGet(cacheKey, out Tenant t))
            {
                return t;
            }

            bool lc = !_dataService.CommandBuilder.DatabaseApi.IsConnectionOpen;
            try
            {
                if (lc)
                {
                    _dataService.CommandBuilder.DatabaseApi.OpenConnection();
                }

                var tenant = _dataService.GetTenantByNameCaseInsensitive(name);

                if (tenant != null &&
                    tenant.ProvisionState == ProvisionState.Provisioned &&
                    tenant.SubscriptionState == SubscriptionState.Trial)
                {
                    // Sets tenant trial state if required.
                    if (!tenant.TrialStarted.HasValue)
                    {
                        tenant.TrialStarted = Clock.Now.Date;
                    }

                    if (!tenant.TrialEnd.HasValue)
                    {
                        var duration = tenant.TrialDurationInDays ?? 30;
                        tenant.TrialEnd = tenant.TrialStarted.Value.AddDays(duration + 1).AddSeconds(-1);
                    }

                    if (tenant.IsModified())
                    {
                        _dataService.Update(tenant);
                    }
                }

                // Tenant information is cached only if it is provisioned and active,
                // as in any other case it couldn't be used so caching it would be useless.

                if (cache != null && 
                    tenant != null && 
                    tenant.ProvisionState == ProvisionState.Provisioned &&
                    tenant.SubscriptionState != SubscriptionState.Inactive)
                {
                    cache.Instance.Set(cacheKey, tenant, _options.SlidingExpireTime);
                }

                return tenant;
            }
            finally
            {
                if (lc)
                {
                    _dataService.CommandBuilder.DatabaseApi.CloseConnection();
                }
            }
        }
    }
}
