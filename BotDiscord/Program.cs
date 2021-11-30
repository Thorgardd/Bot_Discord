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
        /// This method translate every error
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        public static string Translate(string reason)
        {
            switch (reason)
            {
                case "User requires guild permission ManageMessages.":
                    return "Permission invalide";
                case "Unknown command.":
                    return "Commande inconnue.";
                case "The input text has too few parameters.":
                    return "La commande a pas assez de paramètres.";

            }

            return reason;
        }
    }
}
