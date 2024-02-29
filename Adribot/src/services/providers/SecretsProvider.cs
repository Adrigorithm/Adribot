using System;
using System.IO;
using System.Text.Json;
using Adribot.src.config;

namespace Adribot.src.services.providers;

public class SecretsProvider
{
    private const string ConfigPath = "secret/config.json";

    public ConfigValueType Config { get; init; }

    public SecretsProvider()
    {
        try
        {
            Config = JsonSerializer.Deserialize<ConfigValueType>(File.OpenRead(ConfigPath));
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
