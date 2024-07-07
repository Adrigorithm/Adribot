using System;
using System.IO;
using System.Text.Json;
using Adribot.src.config;
using Microsoft.Extensions.Configuration;

namespace Adribot.src.services.providers;

public class SecretsProvider(ConfigValueType config)
{
    public ConfigValueType Config { get; init; } = config;

    public static SecretsProvider LoadFromFile(string filePath = "secret/config.json")
    {
        try
        {
            return new SecretsProvider(JsonSerializer.Deserialize<ConfigValueType>(File.OpenRead(filePath)));
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Configuration file not found!");
            throw;
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Adribot has insufficient permissions to access the configuration file.\n" +
                              "Consider starting as an authorized user or grant the application access.");
            throw;
        }
        catch (JsonException e)
        {
            Console.WriteLine($"Configuration file is corrupt: {e.Message}");
            throw;
        }
    }

    /// <summary>
    /// Loads config values from system level environment variables
    /// </summary>
    /// <returns></returns>
    public static SecretsProvider LoadFromEnv()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        return new SecretsProvider(new ConfigValueType
        {
            BotToken = config["Adribot_botToken"] ?? throw new ArgumentNullException("Enviroment variable not found: Adribot_botToken"),
            CatToken = config["Adribot_catToken"] ?? throw new ArgumentNullException("Enviroment variable not found: Adribot_catToken"),
            DevUserId = Convert.ToUInt64(config["Adribot_devUserId"] ?? throw new ArgumentNullException("Enviroment variable not found: Adribot_devUserId")),
            EmbedColour = config["Adribot_embedColour"] ?? throw new ArgumentNullException("Enviroment variable not found: Adribot_embedColour"),
            SqlConnectionString = config["Adribot_sqlConnectionString"] ?? throw new ArgumentNullException("Enviroment variable not found: Adribot_sqlConnectionString")
        });
    }
}
