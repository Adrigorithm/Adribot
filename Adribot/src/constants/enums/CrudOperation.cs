using DSharpPlus.SlashCommands;

namespace Adribot.src.constants.enums
{
    public enum CrudOperation
    {
        [ChoiceName("Get")]
        GET,
        [ChoiceName("Set")]
        SET,
        [ChoiceName("New")]
        UPDATE,
        [ChoiceName("Delete")]
        REMOVE,
        [ChoiceName("Info")]
        INFO,
        [ChoiceName("List")]
        LIST
    }
}
