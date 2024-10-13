using System;
using System.IO;
using System.Text.Json;
using Adribot.config;
using Adribot.extensions;
using Microsoft.Extensions.Configuration;

namespace Adribot.services.providers;

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
    /// <param name="config">optionally load from a specific configuration provider</param>
    /// <param name="envVarsAreFiles">whether the environment values will have file paths instead of the raw values</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static SecretsProvider LoadFromEnv(IConfiguration? config = null, bool envVarsAreFiles = false)
    {
        config ??= new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        return new SecretsProvider(new ConfigValueType
        {
            BotToken = config["BOT_TOKEN"] is null 
                ? throw new ArgumentNullException(null, "Environment variable not found: Adribot_botToken")
                : envVarsAreFiles
                    ? File.ReadAllText(config["BOT_TOKEN"])
                    : config["BOT_TOKEN"],
            CatToken = config["CAT_TOKEN"] is null 
                ? throw new ArgumentNullException(null, "Environment variable not found: Adribot_catToken")
                : envVarsAreFiles
                    ? File.ReadAllText(config["CAT_TOKEN"])
                    : config["CAT_TOKEN"],
            DevUserId = Convert.ToUInt64(config["DEV_ID"] ?? throw new ArgumentNullException(null, "Environment variable not found: Adribot_devUserId")),
            EmbedColour = config["DISCORD_EMBED_COLOUR"]?.ToDiscordColour() ?? throw new ArgumentNullException(null, "Environment variable not found: Adribot_embedColour"),
            SqlConnectionString = config["DB_CONNECTION"] is null 
                ? throw new ArgumentNullException(null, "Environment variable not found: Adribot_sqlConnectionString")
                : envVarsAreFiles
                    ? File.ReadAllText(config["DB_CONNECTION"])
                    : config["DB_CONNECTION"]
        });
    }

    public static SecretsProvider LoadFromValues(string? botToken, string? catToken, ulong? devUserId, string? embedColour, string? sqlConnectionString) =>
        new(new()
        {
            BotToken = string.IsNullOrWhiteSpace(botToken)
                ? throw new ArgumentNullException(nameof(botToken), "Environment variable not found: Adribot_botToken")
                : botToken,
            CatToken = string.IsNullOrWhiteSpace(catToken)
                ? throw new ArgumentNullException(nameof(catToken), "Environment variable not found: Adribot_catToken")
                : catToken,
            DevUserId = devUserId is null || devUserId == 0
                ? throw new ArgumentNullException(nameof(devUserId), "Environment variable not found: Adribot_devUserId")
                : (ulong)devUserId,
            EmbedColour = embedColour?.ToDiscordColour() ?? throw new ArgumentNullException(nameof(embedColour), "Enviroment variable not found: Adribot_embedColour"),
            SqlConnectionString = string.IsNullOrWhiteSpace(sqlConnectionString)
                ? throw new ArgumentNullException(null, "Enviroment variable not found: Adribot_sqlConnectionString")
                : sqlConnectionString
        });
}
