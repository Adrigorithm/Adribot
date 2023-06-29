using System.Threading.Tasks;
using Adribot.entities;

namespace Adribot
{
    internal static class Program
    {
        // ReSharper disable once InconsistentNaming
        private static async Task Main(string[] args)
        {
            Bot bot = new();
            await bot.StartAsync();
        
            await Task.Delay(-1);
        }
    }
}
