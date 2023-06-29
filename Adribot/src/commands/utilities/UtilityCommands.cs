using DSharpPlus.Entities;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;
using System.Linq;
using Adribot.data;

namespace Adribot.commands.utilities
{
    public class UtilityCommands : ApplicationCommandModule
    {
        [RequireOwner]
        [SlashCommand("info", "Retrieves database information")]
        public async Task GetDataInfoAsync(InteractionContext ctx)
        {
            AdribotDb dbConnection = new();
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"Guilds in database {dbConnection.DGuilds.Count()}"));
        }
    }
}
