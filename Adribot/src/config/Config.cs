using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Adribot.config
{
    public class Config
    {
        [JsonIgnore]
        private const string ConfigPath = "secret/config.json";

        [JsonPropertyName("botToken")]
        public string BotToken { get; set; }

        [JsonPropertyName("sqlConnectionString")]
        public string SQLConnectionString {get; set;}

        public async Task LoadConfigAsync() {
            try {
                Console.WriteLine(Directory.GetCurrentDirectory());
                Config cfgTemp = await JsonSerializer.DeserializeAsync<Config>(File.OpenRead(ConfigPath));
                BotToken = cfgTemp.BotToken;
                SQLConnectionString = cfgTemp.SQLConnectionString;
            } catch(FileNotFoundException) {
                Console.WriteLine("Configuration file not found!");
            } catch(UnauthorizedAccessException) {
                Console.WriteLine("Adribot has insufficient permissions to access the configuration file.\n" +
                    "Consider starting as an authorized user or grant the application access.");
            } catch(JsonException e) {
                Console.WriteLine($"Configuration file is corrupt: {e.Message}");
            }
        }
    }
}