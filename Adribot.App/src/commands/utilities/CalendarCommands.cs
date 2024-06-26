using System;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.constants.enums;
using Adribot.src.entities.utilities;
using Adribot.src.helpers;
using Adribot.src.services;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Adribot.src.commands.utilities;

public class CalendarCommands(IcsCalendarService _icsCalendarService) : ApplicationCommandModule
{
    [SlashCommand("calendar", "Perform various calendar tasks")]
    [SlashCommandPermissions(DiscordPermissions.SendMessages)]
    public async Task GetNextCalendarEventAsync(InteractionContext ctx, [Option("mode", "the task to perform on the calendars")] CalendarCrudOperation option = CalendarCrudOperation.List, [Option("calendar", "the calendar name to perform the action on")] string? calendarName = null, [Option("calendarUri", "link to an external ical/ics file")] string? uri = null, [Option("channel", "channel this calendar will post events to")] long channelId = -1)
    {
        IcsCalendar? calendar = option == CalendarCrudOperation.List || string.IsNullOrEmpty(calendarName) ? null : _icsCalendarService.GetCalendarByName(ctx.Guild.Id, calendarName);

        switch (option)
        {
            case CalendarCrudOperation.New:
                if (calendar is not null || string.IsNullOrEmpty(calendarName))
                {
                    await ctx.CreateResponseAsync(DiscordInteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().WithContent($"Calendar `{calendarName}` for guild [{ctx.Guild.Id}] already exists or is invalid. Try another name or remove it first.")).AsEphemeral());
                }
                else
                {
                    await _icsCalendarService.AddCalendarAsync(ctx.Guild.Id, ctx.Member.Id, channelId != -1 ? (ulong)channelId : ctx.Channel.Id, new Uri(uri));
                    await ctx.CreateResponseAsync(DiscordInteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().WithContent($"Calendar `{calendarName}` for guild [{ctx.Guild.Id}] was added successfully.")).AsEphemeral());
                }

                break;
            case CalendarCrudOperation.Delete:
                if (calendar is null)
                {
                    await ctx.CreateResponseAsync(DiscordInteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().WithContent($"Calendar `{calendarName}` for guild [{ctx.Guild.Id}] cannot be deleted because it does not exist.")).AsEphemeral());
                }
                else
                {
                    var isDeleted = _icsCalendarService.TryDeleteCalendar(ctx.Member, calendar);
                    await ctx.CreateResponseAsync(DiscordInteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().WithContent($"Calendar `{calendarName}` for guild [{ctx.Guild.Id}] {(isDeleted ? "was added successfully." : "is not to be deleted by you.")}")).AsEphemeral());
                }

                break;
            case CalendarCrudOperation.Info:
                if (calendar is null)
                {
                    await ctx.CreateResponseAsync(DiscordInteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().WithContent($"Calendar `{calendarName}` for guild [{ctx.Guild.Id}] does not exist.")).AsEphemeral());
                }
                else
                {
                    await ctx.CreateResponseAsync(DiscordInteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().AddEmbed(calendar.GenerateEmbedBuilder())));
                }

                break;
            case CalendarCrudOperation.Next:
                if (calendar is null)
                {
                    await ctx.CreateResponseAsync(DiscordInteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().WithContent($"Calendar `{calendarName}` for guild [{ctx.Guild.Id}] does not exist.")).AsEphemeral());
                }
                else
                {
                    Event? cEvent = calendar.Events.FirstOrDefault(e => e.Start > DateTimeOffset.UtcNow);
                    if (cEvent is null)
                        await ctx.CreateResponseAsync(DiscordInteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().WithContent($"Calendar `{calendarName}` for guild [{ctx.Guild.Id}] does not exist.")).AsEphemeral());
                    else
                        await ctx.CreateResponseAsync(DiscordInteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().AddEmbed(cEvent.GeneratePXLEmbedBuilder())).AsEphemeral());
                }

                break;
            case CalendarCrudOperation.List:
                var calendarNames = _icsCalendarService.GetCalendarNames(ctx.Guild.Id);
                await ctx.CreateResponseAsync(DiscordInteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().WithContent(FakeExtensions.GetMarkdownCSV(calendarNames))).AsEphemeral());

                break;
        }
    }
}
