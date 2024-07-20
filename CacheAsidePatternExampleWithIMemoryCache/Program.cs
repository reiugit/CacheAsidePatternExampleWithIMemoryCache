using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

var app = builder.Build();

app.MapGet("/test-cacheaside/{id}", (int id, [FromServices] IMemoryCache cache) =>
{
    bool isCached = true;

    var response = cache.GetOrCreate(id, entry =>
    {
        isCached = false;
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5);
        return $"Response for Id {id}";
    });

    return new { id, response, isCached };
});

app.Run();
