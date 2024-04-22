using DSharpPlus.SlashCommands;

namespace Adribot.src.constants.enums;

public enum ActionType
{
    [ChoiceName("connect")]
    Connect,
    [ChoiceName("disconnect")]
    Disconnect,
    [ChoiceName("channels")]
    Channels,
    [ChoiceName("message")]
    Message,
    [ChoiceName("command")]
    Command
}