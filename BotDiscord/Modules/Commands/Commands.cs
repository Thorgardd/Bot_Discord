using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace BotDiscord.Modules.Commands
{
    public partial class Commands : ModuleBase
    {
        #region General Commands

        [Command("help")]
        public async Task Help()
        {
            var builder = new EmbedBuilder()
                .WithColor(22, 133, 0)
                .WithDescription(new Emoji("\u2139") + " Liste des commandes disponibles " + new Emoji("\u2139"))
                .AddField("!server", "Montre les informations du serveur")
                .AddField("!clear (Admin Only)", "Nettoie un channel d'un nombre de messages donnés")
                .AddField("!help", " Montre les commandes disponibles")
                .AddField("!meme", "Chargement d'un meme sur le Discord")
                .WithCurrentTimestamp();
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync("⚙️AIDE AU COMMANDES ⚙️", false, null);
            await Context.Channel.SendMessageAsync(null, false, embed);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\t## [ L'utilisateur {Context.User.Username}#{Context.User.Discriminator} a utlisé la commande '!help' ]");
        }

        #endregion
        
    }
}