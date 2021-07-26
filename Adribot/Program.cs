using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Adribot.src.commands;

namespace Adribot
{
    class Program
    {
        static async Task Main(string[] args) {
            var bot = new Bot();
            bot.AttachCommands(new[] { typeof(AdminCommands), typeof(TagCommands), typeof(StealCommands) });

            await bot.Start();
            await Task.Delay(-1);
        }
    }
}
