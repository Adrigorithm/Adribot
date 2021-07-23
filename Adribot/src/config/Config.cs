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
        private const string ConfigPath = "config.json";

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("prefixes")]
        public string[] Prefixes { get; set; }

        public Config() {

        }

        public async Task LoadConfigAsync() {
            try {
                Config cfgTemp = await JsonSerializer.DeserializeAsync<Config>(File.OpenRead(ConfigPath));
                Token = cfgTemp.Token;
                Prefixes = cfgTemp.Prefixes;
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