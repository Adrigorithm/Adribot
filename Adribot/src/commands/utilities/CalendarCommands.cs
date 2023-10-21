using Adribot.src.entities.utilities;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System.Text;
using System.Threading.Tasks;

namespace Adribot.src.commands.utilities
{
    public partial class UtilityCommands
    {
        [SlashCommand("nextEvent", "Grabs the first next event of a given calendar")]
        [SlashCommandPermissions(Permissions.SendMessages)]
        public async Task GetNextCalendarEventAsync(InteractionContext ctx, [Option("calendar", "the calendar the get the first event for")] string calendar = "list")
        {
            string[] names = DaySchemeService.GetCalendarNames(ctx.Guild.Id);

            if (calendar == "list")
            {
                StringBuilder sb = new($"Calendars found for guild [{ctx.Guild.Id}]\n\n");


                for (int i = 0; i < names.Length; i++)
                {
                    if (i < names.Length - 1)
                        sb.Append($"`{names[i]}, `");
                    else
                        sb.Append($"`{names[i]}`");
                }

                if (names.Length > 0)
                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().WithContent(sb.ToString())).AsEphemeral());
                else
                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().WithContent($"No calendars where found for guild [{ctx.Guild.Id}]")).AsEphemeral());
            }
            else
            {
                IcsCalendar? requestedCalendar = DaySchemeService.GetCalendarByName(ctx.Guild.Id, calendar);

                if (requestedCalendar is null)
                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().WithContent($"Calendar `{calendar}` not found for guild [{ctx.Guild.Id}]")).AsEphemeral());
                else
                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().WithEmbed(requestedCalendar.GenerateEmbedBuilder().Build())).AsEphemeral());
            }

        }
    }
}
