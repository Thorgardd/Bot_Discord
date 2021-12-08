using System;
using System.Threading.Tasks;
using BotDiscord.Modules.Translate;

namespace BotDiscord
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await Startup.RunAsync(args);
        }
    }
}
