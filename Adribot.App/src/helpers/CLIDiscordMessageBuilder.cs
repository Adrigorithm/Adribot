namespace Adribot.src.helpers;

public static class CLIDiscordMessageBuilder
{
    public static string Build(string channelName, string userName, string message) =>
        $"In #{channelName}, {userName} wrote:\n{message}";
}