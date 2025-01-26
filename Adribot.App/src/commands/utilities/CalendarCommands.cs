using System;
using System.Linq;
using System.Threading.Tasks;
using Adribot.Constants.Enums;
using Adribot.Entities.Utilities;
using Adribot.Helpers;
using Adribot.Services;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace Adribot.Commands.Utilities;

public class CalendarCommands(IcsCalendarService icsCalendarService) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("calendar", "Perform various calendar tasks")]
    [RequireUserPermission(ChannelPermission.SendMessages)]
    [RequireContext(ContextType.Guild)]
    public async Task GetNextCalendarEventAsync([Summary("mode", "the task to perform on the calendars")] CalendarCrudOperation option = CalendarCrudOperation.List, [Summary("calendar", "the calendar name to perform the action on")] string? calendarName = null, [Summary("calendarUri", "link to an external ical/ics file")] string? uri = null, [Summary("channel", "channel this calendar will post events to")] string? channelId = null)
    {
        switch (option)
        {
            case CalendarCrudOperation.New:
                if (string.IsNullOrEmpty(calendarName))
                {
                    await RespondAsync("Your new calendar needs a name. Try again.", ephemeral: true);
                }
                else if (GetIcsCalendar(Context.Guild.Id, calendarName) is not null)
                {
                    await RespondAsync($"A calendar with name `{calendarName}` already exists for this guild. Overwrite the old calendar or chose another name.", ephemeral: true);
                }
                else
                {
                    var conversionSucceeded = ulong.TryParse(channelId, out var channelIdParsed);
                    
                    await icsCalendarService.AddCalendarAsync(Context.Guild.Id, Context.User.Id, conversionSucceeded ? channelIdParsed : Context.Channel.Id, new Uri(uri));
                    await RespondAsync($"Calendar `{calendarName}` for guild [{Context.Guild.Id}] was added successfully.", ephemeral: true);
                }

                break;
            case CalendarCrudOperation.Delete:
                if (string.IsNullOrEmpty(calendarName))
                {
                    await RespondAsync("Cannot find a nameless calendar. Try again.", ephemeral: true);
                    return;
                }
                
                if (GetIcsCalendar(Context.Guild.Id, calendarName) is null)
                {
                    await RespondAsync($"Calendar `{calendarName}` for guild [{Context.Guild.Id}] cannot be deleted because it does not exist.", ephemeral: true);
                }
                else
                {
                    var isDeleted = icsCalendarService.TryDeleteCalendar(Context.User as SocketGuildUser, GetIcsCalendar(Context.Guild.Id, calendarName));
                    await RespondAsync($"Calendar `{calendarName}` for guild [{Context.Guild.Id}] {(isDeleted ? "was added successfully." : "is not to be deleted by you.")}", ephemeral: true);
                }

                break;
            case CalendarCrudOperation.Info:
                if (string.IsNullOrEmpty(calendarName))
                {
                    await RespondAsync("Cannot find a nameless calendar. Try again.", ephemeral: true);
                    return;
                }
                
                IcsCalendar? calendar = GetIcsCalendar(Context.Guild.Id, calendarName);

                if (calendar is null)
                    await RespondAsync($"Calendar `{calendarName}` for guild [{Context.Guild.Id}] does not exist.", ephemeral: true);
                else
                    await RespondAsync(embed: calendar.GenerateEmbedBuilder().Build());

                break;
            case CalendarCrudOperation.Next:
                if (string.IsNullOrEmpty(calendarName))
                {
                    await RespondAsync("Cannot find a nameless calendar. Try again.", ephemeral: true);
                    return;
                }
                
                IcsCalendar? calendar0 = GetIcsCalendar(Context.Guild.Id, calendarName);

                if (calendar0 is null)
                {
                    await RespondAsync($"Calendar `{calendarName}` for guild [{Context.Guild.Id}] does not exist.", ephemeral: true);
                }
                else
                {
                    Event? cEvent = calendar0.Events.FirstOrDefault(e => e.Start > DateTimeOffset.UtcNow);

                    if (cEvent is null)
                        await RespondAsync($"Calendar `{calendarName}` for guild [{Context.Guild.Id}] does not exist.", ephemeral: true);
                    else
                        await RespondAsync(embed: cEvent.GeneratePXLEmbedBuilder().Build());
                }

                break;
            case CalendarCrudOperation.List:
                var calendarNames = icsCalendarService.GetCalendarNames(Context.Guild.Id);
                await RespondAsync(FakeExtensions.GetMarkdownCSV(calendarNames), ephemeral: true);

                break;
        }
    }

    private IcsCalendar? GetIcsCalendar(ulong guildId, string name) =>
        icsCalendarService.GetCalendarByName(guildId, name);
}
