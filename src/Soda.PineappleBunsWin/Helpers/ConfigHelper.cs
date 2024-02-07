using System.IO;
using System.Text.Json;

namespace Soda.PineappleBunsWin.Helpers;

public class ConfigHelper
{
    public static string GetConfigPath()
    {
        return FileHelper.GetBaseDirectory();
    }

    public static T? ReadConfig<T>() where T : class
    {
        var configPath = GetConfigPath();
        if (!File.Exists(configPath))
        {
            return null;
        }

        var json = File.ReadAllText(configPath);
        var config = JsonSerializer.Deserialize<T>(json);
        return config;
    }

    public static void WriteConfig<T>(T config) where T : class
    {
        var configPath = GetConfigPath();
        var json = JsonSerializer.Serialize(config);
        File.WriteAllText(configPath, json);
    }
}