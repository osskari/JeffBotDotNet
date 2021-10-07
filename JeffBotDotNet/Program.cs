using Discord;
using Discord.Commands;
using Discord.WebSocket;
using JeffBotDotNet.Helpers;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace JeffBotDotNet
{
    class Program
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        public async Task AsyncMain()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _client.Log += Log;

            var serviceProvider = new Initialize(_commands, _client).BuildServiceProvider();
            await new CommandHandler(serviceProvider, _client, _commands).InstallCommandsAsync();

            var token = Config.GetConfig().Token;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        static void Main(string[] args) => new Program().AsyncMain().GetAwaiter().GetResult();
    }
}
