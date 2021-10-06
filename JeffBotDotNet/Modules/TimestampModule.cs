using Discord.Commands;
using JeffBotDotNet.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeffBotDotNet.Modules
{
    [Group("timestamp")]
    [Alias("ts")]
    public class TimestampModule : ModuleBase<SocketCommandContext>
    {
        [Command("create")]
        [Alias("new")]
        [Summary("Replies with a timestamp based on the given parameters")]
        public async Task CreateTimestamp(
            [Summary("Date to create (optional)")] DateTime? date = null,
            [Summary("Format of timestamp [t, T, d, D, f*, F, R]")] string format = "f"
            )
        {
            if(!IsFormat(format))
            {
                return;
            }
            await ReplyAsync(GetTimestamp(date, format));
        }

        [Command("parse")]
        [Alias("p")]
        [Summary("Parses a string and replaces date strings with timestamps")]
        public async Task ParseMessage(
            [Summary("Should Jeff replace your message or give you a message you can copy paste?")] bool escape,
            [Remainder]
            [Summary("The message to parse (dates should be encapsulated with <{date}:{format}>. Can be \"now\" for current time)")] string message
            )
        {
            var retStr = ""; // Contains the message with parsed timestamps.
            var lastIndex = 0; // Gives us a reference for where we accessed the string last.
            var opens = message.AllIndexesOf("<"); // Find all occurrences of < in the message.
            foreach(var open in opens)
            {
                var close = message.IndexOf('>', open); // Find  next >.
                // If no > was found we treat it as is.
                if(close == -1)
                {
                    // Add contents from last index to open to the new message and move last index up
                    retStr += message[lastIndex..open];
                    lastIndex = open;
                    continue;
                }

                // Find index of format colon within <>
                var formatColon = message.IndexOf(':', open, close-open);

                var format = "f"; // Timestamp format.
                // if colon was found and identifier is one character and is a valid format then set format as that.
                if(formatColon != -1 && message[(formatColon+1)..close].Length == 1 && IsFormat(message[formatColon..close]))
                {
                    format = message[formatColon..close];
                }

                // Try parsing substring between < and > (or : if supplied)
                if(DateTime.TryParse(message[(open+1)..(formatColon == -1 ? close : formatColon)], out DateTime parsed))
                {
                    // Create timestamp from parsed value and format
                    var stamp = GetTimestamp(parsed, format, escape);
                    // Add contents from last index to open + the timestamp to the new message and move last index up
                    retStr += message[lastIndex..open] + stamp;
                    lastIndex = close+1;
                    continue;
                }

                // If string between <> is "now"
                if(message[(open+1)..(formatColon == -1 ? close : formatColon)].ToLower() == "now")
                {
                    // Create timestamp using current date
                    var stamp = GetTimestamp(DateTime.Now, format, escape);
                    // Add contents from last index to open + the timestamp to the new message and move last index up
                    retStr += message[lastIndex..open] + stamp;
                    lastIndex = close+1;
                    continue;
                }

                // If parsing date string failed, then treat it as normal.
                retStr += message[lastIndex..open];
                lastIndex = open;
            }
            // After handling last <, add the remainder of the string.
            retStr += message[lastIndex..];

            // Reply with parsed string and delete command message.
            await ReplyAsync(retStr);
            await Context.Message.DeleteAsync();
        }

        private static bool IsFormat(string format)
        {
            return new List<string> { "t", "T", "d", "D", "f", "F", "R" }.Contains(format);
        }

        private static string GetTimestamp(DateTime? date, string format, bool escape=false)
        {
            return $"{(escape?"\\":"")}<t:{(long)(date ?? DateTime.Now).Subtract(DateTime.UnixEpoch).TotalSeconds}:{format}>";
        }
    }
}
