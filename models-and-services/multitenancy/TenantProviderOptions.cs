using System;

namespace KpokPatagon.Multitenancy
{
    /// <summary>
    /// Options for configuring the <b>TenantProvider</b>.
    /// </summary>
    public class TenantProviderOptions
    {
        /// <summary>
        /// The prefix to use for cache keys. Defaults to "tp-".
        /// </summary>
        public string CacheKeyPrefix { get; set; } = "tp-";

        /// <summary>
        /// The sliding expiration time for cached tenants. Defaults to 1 day.
        /// </summary>
        public TimeSpan SlidingExpireTime { get; set; } = TimeSpan.FromDays(1);
    }
}
