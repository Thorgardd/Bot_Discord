using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace BotDiscord.Services
{
    public class CommandHandler
    {
        public static IServiceProvider _Provider;
        private readonly DiscordSocketClient _Discord;
        private readonly CommandService _Commands;
        private readonly IConfigurationRoot _Config;

        public CommandHandler(DiscordSocketClient discord, IServiceProvider provider, IConfigurationRoot config, CommandService commands)
        {
            _Discord = discord;
            _Provider = provider;
            _Commands = commands;
            _Config = config;

            _Discord.Ready += OnReady;
            _Discord.MessageReceived += OnMessageReceived;
            // _Discord.UserJoined += OnJoined;
        }

        /// <summary>
        /// Method used when Discord Bot is ready to be used on your Discord Server
        /// </summary>
        public async Task OnReady()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(Environment.NewLine + $"\t[ Connecté en tant que {_Discord.CurrentUser.Username}#{_Discord.CurrentUser.Discriminator} ]");
            Console.WriteLine($"\t[ Latence : {_Discord.Latency.ToString()}ms ]");
            Console.WriteLine($"\t[ Statut : {_Discord.Status} ]");
            Console.WriteLine($"\t[ Type : {_Discord.Rest.TokenType} ]" + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\t[ Bienvenue dans la Matrice ]" + Environment.NewLine);
        }

        /// <summary>
        /// Method used when the bot deals with a message
        /// </summary>
        public async Task OnMessageReceived(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;
            if (msg.Author.IsBot) return;
            var context = new SocketCommandContext(_Discord, msg);
            int pos = 0;
            if (msg.HasStringPrefix(_Config["prefix"], ref pos) || msg.HasMentionPrefix(_Discord.CurrentUser, ref pos))
            {
                var result = await _Commands.ExecuteAsync(context, pos, _Provider);
                if (!result.IsSuccess)
                {
                    var reason = result.ErrorReason;
                    var builder = new EmbedBuilder()
                        .WithColor(new Color(22, 133, 0))
                        .AddField($"{new Emoji("\u26A0")} ERREUR", $"{Program.Translate(reason)}");
                    var embed = builder.Build();
                    await context.Channel.SendMessageAsync(null, false, embed);
                }
            }
        }
        
        /// <summary>
        /// Method used when a user join the Discord Server
        /// </summary>
        public async Task OnJoined()
        {
            var builder = new EmbedBuilder()
                .WithColor(new Color(22, 133, 0));
        }
    }
}