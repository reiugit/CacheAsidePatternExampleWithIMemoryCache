using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using CacheAsidePatternExampleWithIMemoryCache;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

var app = builder.Build();

app.MapGet("/test-cacheaside/{id:int}", (int id, [FromServices] IMemoryCache cache) =>
{
    var (wasCached, response) = cache.GetOrCreate(id, () => $"Response for Id {id}");

    return new { id, response, wasCached };
});

app.Run();
