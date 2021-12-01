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
            Console.WriteLine($"\t## [ L'Utilisateur {Context.User.Username}#{Context.User.Discriminator} a utilisé la commande '!server' ]");
            Console.ForegroundColor = default;
        }

        
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
        

        [Command("help")]
        public async Task Help()
        {
            var builder = new EmbedBuilder()
                .WithColor(22, 133, 0)
                .WithTitle("Aide aux commandes")
                .WithDescription(new Emoji("\u2139") + " Liste des commandes disponibles " + new Emoji("\u2139"))
                .AddField("!server", "Montre les informations du serveur")
                .AddField("!clear (Admin Only)", "Nettoie un channel d'un nombre de messages donnés")
                .AddField("!help", " Montre les commandes disponibles")
                .WithCurrentTimestamp();
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
            
            Console.WriteLine($"\t## [ L'utilisateur {Context.User.Username}#{Context.User.Discriminator} a utlisé la commande '!help' ]");
        }
    }
}