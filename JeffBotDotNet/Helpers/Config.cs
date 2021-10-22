using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JeffBotDotNet.Helpers
{
    public static class Config
    {
        public static ConfigModel GetConfig()
        {
            //var path = $"{Path.Combine(Environment.CurrentDirectory.Split(Path.DirectorySeparatorChar).SkipLast(3).ToArray())}{Path.DirectorySeparatorChar}Config.json";
            var path = Environment.CurrentDirectory.Split(Path.DirectorySeparatorChar).Where(x => true);
            
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return JsonConvert.DeserializeObject<ConfigModel>(
                    File.ReadAllText($"{Path.Combine(Environment.CurrentDirectory.Split(Path.DirectorySeparatorChar).SkipLast(3).ToArray())}{Path.DirectorySeparatorChar}Config.json"));
            }
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText("Config.json"));
            }

            throw new NotImplementedException($"What OS are you using??  Never heard of {RuntimeInformation.OSDescription}");
        }
    }

    public class ConfigModel
    {
        [JsonProperty("Token")]
        public string Token { get; set; }
    }
}
