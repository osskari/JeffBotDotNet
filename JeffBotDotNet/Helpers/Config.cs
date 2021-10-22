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
                path = path.SkipLast(3);
            }

            Console.WriteLine($"FULL: {Path.Combine(Environment.CurrentDirectory.Split(Path.DirectorySeparatorChar))}");
            Console.WriteLine($"PARTIAL: {Path.Combine(path.ToArray())}");
            Console.WriteLine($"{File.ReadAllText("Config.json")}");
            return JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText($"{Path.Combine(path.ToArray())}{Path.DirectorySeparatorChar}Config.json"));
        }
    }

    public class ConfigModel
    {
        [JsonProperty("Token")]
        public string Token { get; set; }
    }
}
