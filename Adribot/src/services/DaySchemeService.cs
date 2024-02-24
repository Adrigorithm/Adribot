using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Adribot.src.data.repositories;
using Adribot.src.entities.utilities;
using Adribot.src.extensions;
using Adribot.src.services.providers;
using DSharpPlus.Entities;
using Ical.Net;

namespace Adribot.src.services;

public sealed class DaySchemeService(IcsCalendarRepository _calendarRepository, DiscordClientProvider clientProvider, int timerInterval = 60) : BaseTimerService(clientProvider, timerInterval)
{
    private List<IcsCalendar> _calendars;

    public override Task Work()
    {
        if (!IsDatabaseDataLoaded)
            _calendars = _calendarRepository.GetIcsCalendarsNotExpired().ToList();

        if (_calendars.Count > 0)
        {
            var eventsToPost = new List<(int calendarId, ulong guildId, ulong channelId, Event[] events)>();
            _calendars.ForEach(c => eventsToPost.Add((c.IcsCalendarId, c.DGuild.GuildId, c.ChannelId, c.Events.Where(e => !e.IsPosted && e.End > DateTimeOffset.Now && e.End - DateTimeOffset.Now <= TimeSpan.FromMinutes(10)).ToArray())));

            PostEvents(eventsToPost);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Should be be called separately when events concern multiple guilds.
    /// </summary>
    /// <param name="cEvents">A collection of events</param>
    /// <param name="calendarId">The Id of the calendar to change its state to 'posted'. Set to null if the state shouldn't change.</param>
    /// <param name="guildId">The guild the events are to be posted to</param>
    /// <param name="channelId">The channel in the event should be posted in</param>
    /// <returns></returns>
    public void PostEvents(List<(int calendarId, ulong guildId, ulong channelId, Event[] events)> eventsContainer)
    {
        Dictionary<int, List<(int eventId, bool IsPosted)>> eventsPosted = [];
        DiscordChannel currentChannel;

        eventsContainer.ForEach(async eC =>
        {
            currentChannel = (await Client.GetGuildAsync(eC.guildId)).GetChannel(eC.channelId);
            eC.events.ToList().ForEach(async e =>
            {
                try
                {
                    await currentChannel.SendMessageAsync(e.GeneratePXLEmbedBuilder().Build());
                    eventsPosted[eC.calendarId] ??= [];
                    eventsPosted[eC.calendarId].Add((e.EventId, true));
                }
                catch
                {
                    // Failed to send message - do nothing
                }
            });

            _calendarRepository.ChangeEventsPostedStatus(eventsPosted);
        });
    }

    public string[] GetCalendarNames(ulong guildId) =>
        _calendars.Where(c => c.DGuild.GuildId == guildId).Select(c => c.Name).ToArray();

    public IcsCalendar? GetCalendarByName(ulong guildId, string name) =>
        _calendars.FirstOrDefault(c => c.DGuild.GuildId == guildId && c.Name == name);

    public Event? GetNextEvent(ulong guildId) =>
        _calendars.First(c => c.DGuild.GuildId == guildId).Events.FirstOrDefault(e => e.Start - DateTime.UtcNow > TimeSpan.Zero);

    public async Task AddCalendarAsync(ulong guildId, ulong channelId, Uri? icsFileUri = null)
    {
        var calendar = Calendar.Load(await GetStreamFromUri(icsFileUri));
        IEnumerable<Event> calendarEvents = calendar.Events.ToList().Select(e => e.ToEvent());
        
        _calendars.Add(_calendarRepository.AddCalendar(calendar.Name, guildId, channelId, calendarEvents));

        static async Task<Stream> GetStreamFromUri(Uri uri)
        {
            if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
            {
                using var httpClient = new HttpClient();
                return await httpClient.GetStreamAsync(uri);
            }
            else
            {
                return uri.Scheme == Uri.UriSchemeFile
                    ? (Stream)new FileStream(uri.LocalPath, FileMode.Open, FileAccess.Read)
                    : throw new NotSupportedException("Unsupported Uri scheme");
            }
        }
    }

    public void DeleteCalendarAsync(IcsCalendar calendar)
    {
        _calendars.Remove(calendar);
        _calendarRepository.RemoveCalendar(calendar);
    }

}