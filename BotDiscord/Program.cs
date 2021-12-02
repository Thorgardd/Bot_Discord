using System;
using System.Threading.Tasks;

namespace BotDiscord
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await Startup.RunAsync(args);
        }

        /// <summary>
        /// This method will translate every error which will be showed in the Channel
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        public static string Translate(string reason)
        {
            switch (reason)
            {
                case "User requires guild permission ManageMessages.":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\t## [ Un utilisateur a tenté une commande inconnue sans les permissions requises ]");
                    return "Permission invalide";
                case "Unknown command.":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\t## [ Un utilisateur a tenté une commande inconnue ]");
                    return "Commande inconnue.";
                case "The input text has too few parameters.":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\t## [ Un utilisateur a tenté une commande incomplète ]");
                    return "La commande n'a pas assez de paramètres.";
                case "User not found.":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\t## [ Un utilisateur a tenté une recherche d'utlisateurs infructueuse ]");
                    return "Utilisateur non trouvé";

            }

            return reason;
        }
    }
}
