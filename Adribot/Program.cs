using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Adribot
{
    class Program
    {
        static async Task Main(string[] args) {
            var bot = new Bot(true, true);
            await bot.Start();
            await Task.Delay(-1);
        }
    }
}
