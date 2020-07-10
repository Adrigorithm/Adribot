using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace Adribot
{
    public class AuthCommand
    {
        protected ApiJson apiJson;

        public AuthCommand() {
            LoadAPI();
        }

        private void LoadAPI() {
            string json = GetApiJson();
            apiJson = JsonConvert.DeserializeObject<ApiJson>(json);
        }

        private string GetApiJson() {
            using(var fs = File.OpenRead("api.json")) {
                using(var sr = new StreamReader(fs, new UTF8Encoding(false))) {
                    return sr.ReadToEnd();
                }
            }
        }
    }

    public struct ApiJson
    {
        [JsonProperty("cat")]
        public string CatToken { get; private set; }
    }
}
