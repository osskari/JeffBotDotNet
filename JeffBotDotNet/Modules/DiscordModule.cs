using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeffBotDotNet.Modules
{
    [Group("")]
    public class DiscordModule : ModuleBase<SocketCommandContext>, IJeffBotModule
    {
        [Command("help")]
        [Alias("-h", "h")]
        [Summary("Prints out instructions on how to use the module")]
        public async Task HelpAsync()
        {
            throw new NotImplementedException();
        }
    }
}
