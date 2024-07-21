using Microsoft.Extensions.Caching.Memory;

namespace CacheAsidePatternExampleWithIMemoryCache;

public static class CacheExtensions
{
    public static (string response, bool wasCached, int ageInSeconds) GetOrCreateWithCacheInfo(this IMemoryCache cache, int id, Func<string> stringFactory)
    {
        bool wasCached = true;

        var (response, cachedAt) = cache.GetOrCreate(id, entry =>
        {
            wasCached = false;

            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5);
            
            return (stringFactory(), DateTimeOffset.UtcNow);
        });

        var cachedSinceInSeconds = (DateTimeOffset.UtcNow - cachedAt).Seconds;

        return (response!, wasCached, cachedSinceInSeconds);
    }
}
