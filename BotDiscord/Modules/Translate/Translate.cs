using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BotDiscord.Modules.Translate
{
    public class Translate
    {

        public static string TranslateError(string reason)
        {
            /*var startup = new Startup();
            foreach (string translate in startup.Configuration)
            {
                /*Console.WriteLine(translate);#1#
                
                if (translate == reason)
                {
                    return translate[1];
                }
            }*/

            return reason;
        }
    }
}