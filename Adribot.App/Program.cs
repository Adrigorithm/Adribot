using System.Threading.Tasks;
using Adribot.src.entities;

namespace Adribot;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Bot bot = new();
        await bot.StartAsync();

        await Task.Delay(-1);
    }
}
