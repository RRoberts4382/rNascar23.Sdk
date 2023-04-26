# rNascar23.Sdk

This project is for developers who want to read data from NASCAR endpoints, and use this data in their own project.

This sdk targets netstandard2.0 ,net6.0, and net6.0-maccatalyst. 

### Required packages:
- Newtonsoft.Json Version 13.0.3
- AutoMapper.Extensions.Microsoft.DependencyInjection Version 8.0.1
- Microsoft.Extensions.DependencyInjection.Abstractions Version 7.0.0
- RestSharp Version 109.0.1


After referencing this project (or the NuGet package it builds) from your project, you can add it to your ServiceCollection by adding the `AddrNascar23Sdk()` call when you are configuring services.

### Adding to service collection:

```
static IHostBuilder CreateHostBuilder()
{
    return Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                        {
                            services.AddrNascar23Sdk();
                        });
}
```

### Reading data:

```
public class MyClass
{
    private readonly ILiveFeedRepository _liveFeedRepository = null;

    /// <summary>
    /// Get instance of the repository from dependency injection in the constructor of your class.
    /// </summary>
    public MyClass(ILiveFeedRepository liveFeedRepository)
    {
         _liveFeedRepository = liveFeedRepository ?? throw new ArgumentNullException(nameof(liveFeedRepository));
    }

    /// <summary>
    /// Read data from the repository
    /// </summary>
    private async Task<bool> ReadDataAsync()
    {
        var liveFeedData = await _liveFeedRepository.GetLiveFeedAsync();
    }
}
```

### Data Sources

The following sources of data are available during live events:
- FlagState - Information about the flags that have been displayed during the event.
- LapTimes - Summary and detailed information about lap times for the drivers in the event.
- LiveFeed - Summary information about the event in progress.
- LoopData - Driver loop data for the current event.
- PitStops - Detailed information about each pit stop by each driver during the current event.
- Points - Driver points and stage points.
- Schedules - Season schedule information for the top 3 touring series. Includes results for past events.
