using Microsoft.Extensions.Caching.Memory;

namespace CacheAsidePatternExampleWithIMemoryCache;

public static class CacheOptions //for brevity without options pattern and without DI
{
    private static readonly MemoryCacheEntryOptions absoluteExpirationInFiveSeconds = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
    };

    public static MemoryCacheEntryOptions AbsoluteExpirationInFiveSeconds
        => absoluteExpirationInFiveSeconds;
}
