using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

var app = builder.Build();

app.MapGet("/test-cacheaside/{id:int}", (int id, [FromServices] IMemoryCache cache) =>
{
    bool wasCached = true;

    var response = cache.GetOrCreate(id, entry =>
    {
        wasCached = false;
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5);
        return $"Response for Id {id}";
    });

    return new { id, response, wasCached };
});

app.Run();
