using Discord.Interactions;

namespace Adribot.constants.enums;

public enum RemoteAccessActionType
{
    [ChoiceDisplay("connect")]
    Connect,
    [ChoiceDisplay("disconnect")]
    Disconnect,
    [ChoiceDisplay("channels")]
    Channels,
    [ChoiceDisplay("members")]
    Members,
    [ChoiceDisplay("message")]
    Message,
    [ChoiceDisplay("command")]
    Command
}
