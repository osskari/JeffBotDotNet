using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeffBotDotNet.Modules
{
    [Group("convert")]
    [Summary("Unit conversion commands for conversations between people from different places")]
    public class UnitConverterModule : ModuleBase<SocketCommandContext>, IJeffBotModule
    {
        [Command("ctof")]
        [Alias("celsiustofahrenheit", "celsius2fahrenheit", "c2f")]
        [Summary("converts celsius to fahrenheit")]
        public async Task CToFAsync([Summary("celsius value")] double c)
        {
            await ReplyAsync($"{c}°C => {(c * 9 / 5) + 32:F2}°F");
        }

        [Command("ftoc")]
        [Alias("fahrenheittocelsius", "fahrenheit2celsius", "f2c")]
        [Summary("converts fahrenheit to celsius")]
        public async Task FToCAsync([Summary("fahrenheit value")] double f)
        {
            await ReplyAsync($"{f}°F => {((f - 32) * 5 / 9):F2}°C");
        }

        [Command("help")]
        [Alias("-h", "h")]
        [Summary("Prints out instructions on how to use the module")]
        public async Task HelpAsync()
        {
            var a = new Discord.EmbedBuilder();
            a.AddField("name", "value69");
            a.AddField("name2", "value420", true);
            await ReplyAsync(embed: a.Build());
            //throw new NotImplementedException();
        }
    }
}
