using Microsoft.Extensions.Caching.Memory;

namespace CacheAsidePatternExampleWithIMemoryCache;

public static class CacheExtensions
{
    public static (string response, bool wasCached, double ageInSeconds) GetOrCreateWithCacheInfo(this IMemoryCache cache, int id, Func<string> stringFactory)
    {
        if (cache.TryGetValue(id, out ResponseWithTimestamp? cachedResponseWithTimestamp))
        {
            var cachedSinceInSeconds = (DateTimeOffset.UtcNow - cachedResponseWithTimestamp!.CachedAt).TotalSeconds;

            return (cachedResponseWithTimestamp.Response, true, cachedSinceInSeconds);
        }

        var response = stringFactory();
        var responseWithTimestamp = new ResponseWithTimestamp(response, DateTimeOffset.UtcNow);

        cache.Set(id, responseWithTimestamp, CacheOptions.AbsoluteExpirationInFiveSeconds);

        return (response, false, 0);
    }
}
