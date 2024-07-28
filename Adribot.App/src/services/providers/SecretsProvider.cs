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
    public static SecretsProvider LoadFromEnv(IConfiguration? config = null)
    {
        config ??= new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        return new SecretsProvider(new ConfigValueType
        {
            BotToken = config["BOT_TOKEN"] ?? throw new ArgumentNullException("Enviroment variable not found: Adribot_botToken"),
            CatToken = config["CAT_TOKEN"] ?? throw new ArgumentNullException("Enviroment variable not found: Adribot_catToken"),
            DevUserId = Convert.ToUInt64(config["DEV_ID"] ?? throw new ArgumentNullException("Enviroment variable not found: Adribot_devUserId")),
            EmbedColour = config["DISCORD_EMBED_COLOUR"] ?? throw new ArgumentNullException("Enviroment variable not found: Adribot_embedColour"),
            SqlConnectionString = config["DB_CONNECTION"] ?? throw new ArgumentNullException("Enviroment variable not found: Adribot_sqlConnectionString")
        });
    }

    public static SecretsProvider LoadFromValues(string? botToken, string? catToken, ulong? devUserId, string? embedColour, string? sqlConnectionString) =>
        new(new()
        {
            BotToken = string.IsNullOrWhiteSpace(botToken)
                ? throw new ArgumentNullException(nameof(botToken), "Enviroment variable not found: Adribot_botToken")
                : botToken,
            CatToken = string.IsNullOrWhiteSpace(catToken)
                ? throw new ArgumentNullException(nameof(catToken), "Enviroment variable not found: Adribot_catToken")
                : catToken,
            DevUserId = devUserId is null || devUserId == 0
                ? throw new ArgumentNullException(nameof(devUserId), "Enviroment variable not found: Adribot_devUserId")
                : (ulong)devUserId,
            EmbedColour = embedColour ?? throw new ArgumentNullException(nameof(embedColour), "Enviroment variable not found: Adribot_embedColour"),
            SqlConnectionString = string.IsNullOrWhiteSpace(sqlConnectionString)
                ? throw new ArgumentNullException("Enviroment variable not found: Adribot_sqlConnectionString")
                : sqlConnectionString
        });
}
