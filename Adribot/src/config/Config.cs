using System;
using System.IO;
using Newtonsoft.Json;

namespace Adribot.config
{
    public class Config
    {
        private const string Path = "config.json";

        [JsonProperty("token")]
        public static string Token { get; private set; }
        
        [JsonProperty("prefix")]
        public static string Prefix { get; private set; }

        static Config()
        {
            LoadConfig(Path);
        }

        private static void LoadConfig(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    JsonConvert.DeserializeObject<Config>(File.ReadAllText(Path));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                Console.WriteLine("Config file is missing.");
            }
        }
    }
}