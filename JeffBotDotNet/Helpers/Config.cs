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
            var path = $"{Path.Combine(Environment.CurrentDirectory.Split(Path.DirectorySeparatorChar).SkipLast(3).ToArray())}{Path.DirectorySeparatorChar}Config.json";
            Console.WriteLine($"FULL: {Path.Combine(Environment.CurrentDirectory.Split(Path.DirectorySeparatorChar))}");
            Console.WriteLine($"PARTIAL: {Path.Combine(Environment.CurrentDirectory.Split(Path.DirectorySeparatorChar).SkipLast(3).ToArray())}");
            return JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText(path));
        }
    }

    public class ConfigModel
    {
        [JsonProperty("Token")]
        public string Token { get; set; }
    }
}
