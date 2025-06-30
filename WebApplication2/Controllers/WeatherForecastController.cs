using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers;

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

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    // mixing data layer in controller (normally not)
    private static readonly List<EclipseInfo> _eclipseInfos = new List<EclipseInfo>
    {
        new EclipseInfo { Name = "Solar Eclipse", X = 34.0522, Y = -118.2437, Date = new DateTime(2024, 4, 8) },
        new EclipseInfo { Name = "Lunar Eclipse", X = 51.5074, Y = -0.1278, Date = new DateTime(2025, 3, 14) },
        new EclipseInfo { Name = "Total Solar Eclipse", X = 40.7128, Y = -74.0060, Date = new DateTime(2026, 8, 12) }
    };

    [HttpGet("GetEclipses")]
    public IEnumerable<EclipseInfo> GetEclipseInfos()
    {
        return _eclipseInfos;
        ////return Ok(decimal)
        //return new List<EclipseInfo>
        //{
        //    new EclipseInfo { Name = "Solar Eclipse", X = 34.0522, Y = -118.2437, Date = new DateTime(2024, 4, 8) },
        //    new EclipseInfo { Name = "Lunar Eclipse", X = 51.5074, Y = -0.1278, Date = new DateTime(2025, 3, 14) },
        //    new EclipseInfo { Name = "Total Solar Eclipse", X = 40.7128, Y = -74.0060, Date = new DateTime(2026, 8, 12) }
        //};
    }

    [HttpPost("AddEclipse")]
    public IActionResult AddEclipse([FromBody] EclipseInfo eclipseInfo)
    {
        if (eclipseInfo == null || string.IsNullOrEmpty(eclipseInfo.Name) || eclipseInfo.Date == default)
        {
            return BadRequest("Invalid eclipse information.");
        }

        _eclipseInfos.Add(eclipseInfo);
        // Here you would typically save the eclipseInfo to a database or a collection.
        // For this example, we will just return it back as a confirmation.

        // call to data layer to write it to the DB

        return Ok(eclipseInfo);
    }
}
