using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();

var app = builder.Build();

// Map the GET request to the "/current-time" endpoint.
app.MapGet("/current-time", (IMemoryCache cache) =>
{
    // Declare a variable to hold the current time.
    DateTime currentTime;

    // Define a unique key for caching the current time.
    const string cacheKey = "CurrentTimeCacheKey";

    // Try to retrieve the current time from the cache.
    if (!cache.TryGetValue(cacheKey, out currentTime))
    {
        // If the current time is not in the cache, fetch the current time.
        currentTime = DateTime.Now;

        // Define caching options. Here, we set the cache to expire after 10 seconds.
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
        };

        // Store the current time in the cache with the defined options.
        cache.Set(cacheKey, currentTime, cacheEntryOptions);
    }

    // Return the current time, either from the cache or newly fetched.
    return currentTime;
});

app.Run();