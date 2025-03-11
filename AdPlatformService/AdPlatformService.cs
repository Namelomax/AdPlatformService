using System.Collections.Concurrent;
public class AdPlatformService
{
    private readonly ConcurrentDictionary<string, HashSet<string>> _platforms = new();

    public void LoadFromFile(string filePath)
    {
        var newData = new ConcurrentDictionary<string, HashSet<string>>();
        
        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split(":", StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2) continue;

            var platform = parts[0].Trim();
            var locations = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries)
                                    .Select(loc => loc.Trim())
                                    .ToHashSet();

            newData[platform] = locations;
        }

        _platforms.Clear();
        foreach (var kvp in newData)
            _platforms[kvp.Key] = kvp.Value;
    }

public List<string> GetPlatformsForLocation(string location)
{
    var matchingPlatforms = new HashSet<string>();

    // Проверяем все возможные уровни вложенности (от самого специфичного к глобальному)
    while (!string.IsNullOrEmpty(location))
    {
        foreach (var (platform, locations) in _platforms)
        {
            if (locations.Contains(location))
            {
                matchingPlatforms.Add(platform);
            }
        }

        // Укорачиваем локацию до более общего уровня
        int lastSlashIndex = location.LastIndexOf('/');
        location = lastSlashIndex > 0 ? location.Substring(0, lastSlashIndex) : "";
    }

    return matchingPlatforms.ToList();
}
}
