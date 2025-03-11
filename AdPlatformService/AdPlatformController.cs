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
    // Поиск по полученным данным и запросу
    public IActionResult SearchPlatforms([FromQuery] string location)
    {
        var result = _service.GetPlatformsForLocation(location);
        return Ok(result);
    }
}
