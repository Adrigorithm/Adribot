using Discord.Interactions;

namespace Adribot.src.constants.enums;

public enum MinecraftVersion
{
    [ChoiceDisplay("1_20-or-older")]
    Legacy,

    [ChoiceDisplay("1_21-and-later")]
    Modern
}