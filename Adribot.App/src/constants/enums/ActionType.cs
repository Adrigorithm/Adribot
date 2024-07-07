using DSharpPlus.SlashCommands;

namespace Adribot.src.constants.enums;

public enum RAActionType
{
    [ChoiceName("connect")]
    Connect,
    [ChoiceName("disconnect")]
    Disconnect,
    [ChoiceName("channels")]
    Channels,
    [ChoiceName("members")]
    Members,
    [ChoiceName("message")]
    Message,
    [ChoiceName("command")]
    Command
}
