using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace BotDiscord.Modules.Commands
{
    public partial class Commands
    {
        [Command("infos")]
        public async Task Infos(SocketGuildUser user = null)
        {
            var builder = new EmbedBuilder()
                .WithColor(new Color(22, 133, 0))
                .WithThumbnailUrl(user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
                .WithTitle("Informations de l'utilisateur connecté")
                .AddField("Pseudo : ", $"{user.Mention}")
                .AddField("Compte créé le : ", $"{(user.CreatedAt.ToString("dd/MM/yyyy"))}")
                .WithCurrentTimestamp();
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\t## [ L'utlisateur {Context.User.Username}#{Context.User.Discriminator} a utilisé la commande '!infos' sur {user.Username}#{user.Discriminator}");
            Console.ForegroundColor = default;
        }
    }
}