using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Adribot.config;

public static class Config
{
    // Debug
    private const string ConfigPath = "../../../secret/config.json";
    
    public static ConfigValueType Configuration { get; private set; }

    public static async Task LoadConfigAsync()
    {
        try
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            Configuration = await JsonSerializer.DeserializeAsync<ConfigValueType>(File.OpenRead(ConfigPath));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Configuration file not found!");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Adribot has insufficient permissions to access the configuration file.\n" +
                              "Consider starting as an authorized user or grant the application access.");
        }
        catch (JsonException e)
        {
            Console.WriteLine($"Configuration file is corrupt: {e.Message}");
        }
    }
}
