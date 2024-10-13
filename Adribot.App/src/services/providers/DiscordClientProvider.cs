using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Adribot.services.providers;

public class DiscordClientProvider
{
    public DiscordSocketClient Client { get; init; }

    public DiscordClientProvider()
    {
        Client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        });

        Client.Log += Log;
    }

    private static Task Log(LogMessage message)
    {
        Console.WriteLine(message.ToString());
        return Task.CompletedTask;
    }
}
