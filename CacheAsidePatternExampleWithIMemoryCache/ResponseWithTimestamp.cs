namespace CacheAsidePatternExampleWithIMemoryCache;

public record ResponseWithTimestamp(string Response, DateTimeOffset CachedAt);
