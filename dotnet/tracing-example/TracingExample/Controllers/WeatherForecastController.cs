using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;
using TracingExample.Config;

namespace TracingExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public string Get()
    {
        using var activity = DiagnosticsConfig.ActivitySource.StartActivity("SayHello");
        var act = Activity.Current;
        using var activity2 = DiagnosticsConfig.ActivitySource.StartActivity("SayHalloAgain");
        return "hello";
    }
    [HttpGet("exception")]
    public string Exception()
    {
        try
        {
            DoWork();
        }
        catch (Exception ex)
        {
            Activity.Current?.RecordException(ex);
            Activity.Current?.SetStatus(ActivityStatusCode.Error);
        }
        return "ASDasd";
    }
    [HttpGet("http")]
    public string HttpCall()
    {
        using var activity = DiagnosticsConfig.ActivitySource.StartActivity("Start of http method");
        using HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        using var activity2 = DiagnosticsConfig.BrokerServiceActivitySource.StartActivity("broker_service.call");
        ProcessRepositoriesAsync(client).GetAwaiter().GetResult();
        return "Ok";
    }
    static async Task ProcessRepositoriesAsync(HttpClient client)
    {
        var json = await client.GetStringAsync(
            "https://api.github.com/orgs/dotnet/repos");

        Console.Write(json);
    }
    public void DoWork()
    {
        throw new Exception("some exception");
    }
}
