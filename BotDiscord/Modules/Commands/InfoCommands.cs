using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace BotDiscord.Modules.Commands
{
    public partial class Commands
    {
        
        [Command("serverinfos")]
        public async Task ServerInfos()
        {
            var builder = new EmbedBuilder()
                .WithThumbnailUrl(Context.Guild.IconUrl)
                .WithTitle($"{Context.Guild.Name} - INFORMATIONS")
                .WithColor(new Color(22, 133, 0))
                .WithDescription($"Informations autorisées")
                .AddField("Date de Création de la Matrice : ", $"{(Context.Guild.CreatedAt.ToString("dd/MM/yyyy"))}", false)
                .AddField("Membres : ", $"{(Context.Guild as SocketGuild).MemberCount}", false)
                .AddField("Propriétaire : ", $"@{(Context.Guild as SocketGuild).Owner}", false)
                .AddField("Latence : ", $"{(Context.Client as DiscordSocketClient).Latency}", false)
                .WithCurrentTimestamp();
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\t## [ L'Utilisateur {Context.User.Username}#{Context.User.Discriminator} a utilisé la commande '!server' ]");
            Console.ForegroundColor = default;
        }
        
        
        
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