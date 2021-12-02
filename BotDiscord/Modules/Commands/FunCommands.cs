using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;

namespace BotDiscord.Modules.Commands
{
    public partial class Commands
    {
        [Command("meme")]
        public async Task Meme()
        {
            var Client = new HttpClient();
            var result = await Client.GetStringAsync("https://reddit.com/r/memes/random.json?limit=1");
            JArray array = JArray.Parse(result);
            JObject post = JObject.Parse(array[0]["data"]["children"][0]["data"].ToString());

            var builder = new EmbedBuilder()
                .WithImageUrl(post["url"].ToString())
                .WithColor(new Color(22, 133, 0))
                .WithTitle(post["title"].ToString())
                .WithUrl("https://reddit.com" + post["permalink"].ToString())
                .WithFooter($"🗨️ {post["num_comments"]} 👍 {post["ups"]}");
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\t## [ L'utilisateur {Context.User.Username}#{Context.User.Discriminator} a utliisé la commande '!meme'");
            Console.ForegroundColor = default;
        }
    }
}