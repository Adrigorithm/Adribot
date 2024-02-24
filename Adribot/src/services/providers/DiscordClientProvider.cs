using DSharpPlus;

namespace Adribot.src.services.providers;

public class DiscordClientProvider(DiscordClient _client)
{
    public DiscordClient Client =>
        _client;
}