using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace BotDiscord.Modules
{
    public class Commands : ModuleBase
    {
        [Command("server")]
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
            Console.WriteLine($"[ L'Utilisateur {Context.User.Username}#{Context.User.Discriminator} a utilisé la commande '!server' ]");
            Console.ForegroundColor = default;
        }

        [Command("clear")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task Clear(int amount)
        {
            if (!(Context.User as SocketGuildUser).GuildPermissions.ManageMessages)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\t[ L'utilisateur {Context.User.Username}#{Context.User.Discriminator} a utilisé la commande '!clear'\n sans en avoir la permission ]");
                Console.ForegroundColor = default;
            }
            else if ((Context.User as SocketGuildUser).GuildPermissions.ManageMessages && amount < 200)
            {
                var messages = await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync();
                await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\t[ L'utilisateur {Context.User.Username}#{Context.User.Discriminator} a utilisé la commande '!clear' ]");
                Console.ForegroundColor = default;
            }
        }
    }
}