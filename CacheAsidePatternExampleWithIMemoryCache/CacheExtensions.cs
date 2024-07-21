using Microsoft.Extensions.Caching.Memory;

namespace CacheAsidePatternExampleWithIMemoryCache;

public static class CacheExtensions
{
    public static ResponseWithCacheInfo GetOrCreateWithCacheInfo(this IMemoryCache cache, int id, Func<string> factory)
    {
        if (cache.TryGetValue(id, out ResponseWithTimestamp? cachedResponseWithTimestamp))
        {
            var cachedSinceInSeconds = (DateTimeOffset.UtcNow - cachedResponseWithTimestamp!.CachedAt).TotalSeconds;

            return new(id, cachedResponseWithTimestamp.Response, true, cachedSinceInSeconds);
        }

        var response = factory();
        var responseWithTimestamp = new ResponseWithTimestamp(response, DateTimeOffset.UtcNow);

        cache.Set(id, responseWithTimestamp, CacheOptions.AbsoluteExpirationInFiveSeconds);

        return new(id, response, false, 0);
    }
}
