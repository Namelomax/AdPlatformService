using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/ad-platforms")]
public class AdPlatformController : ControllerBase
{
    private readonly AdPlatformService _service;

    public AdPlatformController(AdPlatformService service)
    {
        _service = service;
    }

    [HttpPost("load")]
    // Загрузка из файла
    public IActionResult LoadPlatforms([FromBody] string filePath)
    {
        try
        {
            _service.LoadFromFile(filePath);
            return Ok("Данные загружены");
        }
        catch (Exception ex)
        {
            return BadRequest($"Ошибка загрузки: {ex.Message}");
        }
    }

[HttpGet("search")]
// Поиск по полученному запросу
public IActionResult SearchPlatforms([FromQuery] string? location)
{
    if (string.IsNullOrWhiteSpace(location))
    {
        return BadRequest("Неправильный формат локации.");
    }

    var platforms = _service.GetPlatformsForLocation(location);

    if (platforms.Count == 0)
    {
        return Ok("Нет доступных рекламных площадок для данной локации.");
    }

    return Ok(platforms);
}
}
