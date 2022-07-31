using System;
using System.Threading.Tasks;

class Program{
    static async Task Main(string[] args){
        Bot bot = new();
        await bot.StartAsync();
        
        await Task.Delay(-1);
    }
}
