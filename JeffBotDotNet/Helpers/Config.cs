using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeffBotDotNet.Helpers
{
    public static class Config
    {
        public static ConfigModel GetConfig()
        {
            return JsonConvert.DeserializeObject<ConfigModel>(
                File.ReadAllText(
                    $"{string.Join("\\", Environment.CurrentDirectory.Split("\\").SkipLast(3))}\\Config.json"
                    )
                );
        }
    }

    public class ConfigModel
    {
        [JsonProperty("Token")]
        public string Token { get; set; }
    }
}
