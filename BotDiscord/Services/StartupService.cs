using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace BotDiscord.Services
{
    public class StartupService
    {
        public static IServiceProvider _Provider;
        private readonly DiscordSocketClient _Discord;
        private readonly CommandService _Commands;
        private readonly IConfigurationRoot _Config;

        public StartupService(IServiceProvider provider, DiscordSocketClient discord, CommandService commands, IConfigurationRoot config)
        {
            _Provider = provider;
            _Discord = discord;
            _Commands = commands;
            _Config = config;
        }

        public async Task StartAsync()
        {
            string? Token = Environment.GetEnvironmentVariable("DiscordToken", EnvironmentVariableTarget.User);

            await _Discord.LoginAsync(TokenType.Bot, Token);
            await _Discord.StartAsync();
            
            await _Commands.AddModulesAsync(Assembly.GetEntryAssembly(), _Provider);

            if (string.IsNullOrEmpty(Token))
            {
                Console.WriteLine("Veuillez inserer votre Token dans le fichier _config.yml");
                return;
            }
        }
    }
}