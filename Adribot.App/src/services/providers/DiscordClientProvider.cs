using Discord;
using Discord.WebSocket;

namespace Adribot.src.services.providers;

public class DiscordClientProvider
{
    public DiscordSocketClient Client { get; init; }

    public DiscordClientProvider() =>
        Client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        });

}
