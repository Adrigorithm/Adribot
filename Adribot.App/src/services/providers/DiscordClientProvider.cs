using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Adribot.services.providers;

public class DiscordClientProvider
{

    public DiscordClientProvider()
    {
        Client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        });

        Client.Log += Log;
    }
    public DiscordSocketClient Client { get; init; }

    private static Task Log(LogMessage message)
    {
        Console.WriteLine(message.ToString());
        return Task.CompletedTask;
    }
}
