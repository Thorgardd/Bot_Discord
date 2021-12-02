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
        // Attributs
        //
        public static IServiceProvider _Provider;
        private readonly DiscordSocketClient _Discord;
        private readonly CommandService _Commands;
        private readonly IConfigurationRoot _Config;
        
        // Constructor
        //
        public StartupService(IServiceProvider provider, DiscordSocketClient discord, CommandService commands, IConfigurationRoot config)
        {
            _Provider = provider;
            _Discord = discord;
            _Commands = commands;
            _Config = config;
        }

        // Starting Function
        //
        public async Task StartAsync()
        {
            string token = Environment.GetEnvironmentVariable("DiscordToken", EnvironmentVariableTarget.User);

            await _Discord.LoginAsync(TokenType.Bot, token);
            await _Discord.StartAsync();
            
            await _Commands.AddModulesAsync(Assembly.GetEntryAssembly(), _Provider);

            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Veuillez inserer votre Token dans le fichier _config.yml");
            }
        }
    }
}