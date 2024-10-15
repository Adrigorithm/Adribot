using Discord.Interactions;

namespace Adribot.Constants.Enums;

public enum MonitoringOptions
{
    [ChoiceDisplay("commands_local")]
    GuildCommands,
    [ChoiceDisplay("commands_global")]
    GlobalCommands,
    [ChoiceDisplay("commands_all")]
    AllCommands
}
