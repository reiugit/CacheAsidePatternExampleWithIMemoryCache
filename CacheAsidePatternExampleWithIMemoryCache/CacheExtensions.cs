using Microsoft.Extensions.Caching.Memory;

namespace CacheAsidePatternExampleWithIMemoryCache;

public static class CacheExtensions
{
    public static (bool, string) GetOrCreate(this IMemoryCache cache, int id, Func<string> stringFactory)
    {
        bool wasCached = true;

        var response = cache.GetOrCreate(id, entry =>
        {
            wasCached = false;

            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5);
            
            return stringFactory();
        });

        return (wasCached, response!);
    }
}
