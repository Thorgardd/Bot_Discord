using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace BotDiscord.Modules.Commands
{
    public partial class Commands
    {
        [Command("clear")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task Clear(int amount)
        {
            if (!(Context.User as SocketGuildUser).GuildPermissions.ManageMessages)
            {
                var builder = new EmbedBuilder()
                    .WithColor(new Color(22, 133, 0))
                    .AddField(new Emoji("\u26A0") + "Erreur", "Demande trop grande")
                    .WithCurrentTimestamp();
                var embed = builder.Build();
                await Context.Channel.SendMessageAsync(null, false, embed);
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\t## [ L'utilisateur {Context.User.Username}#{Context.User.Discriminator} a utilisé la commande '!clear' sans en avoir la permission ]");
                Console.ForegroundColor = default;
                // TODO - NE FONCTIONNE PAS
            }
            else if ((Context.User as SocketGuildUser).GuildPermissions.ManageMessages && amount < 200)
            {
                var messages = await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync();
                await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\t## [ L'utilisateur {Context.User.Username}#{Context.User.Discriminator} a utilisé la commande '!clear' ]");
                Console.ForegroundColor = default;
            }
        }
    }
}