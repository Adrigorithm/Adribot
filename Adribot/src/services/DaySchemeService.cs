using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Adribot.src.data;
using Adribot.src.entities.utilities;
using Adribot.src.extensions;
using DSharpPlus;
using Ical.Net;

namespace Adribot.src.services
{
    public class DaySchemeService : BaseTimerService
    {
        private readonly List<IcsCalendar> _calendars;

        public DaySchemeService(DiscordClient client, int timerInterval = 60) : base(client, timerInterval)
        {
            DateTimeOffset today = DateTimeOffset.UtcNow;

            using var database = new DataManager();
            _calendars = database.GetIcsCalendarsNotExpired(today);
        }

        public override async Task WorkAsync()
        {
            if (_calendars.Count > 0)
            {
                DateTimeOffset today = DateTimeOffset.UtcNow;

                foreach (IcsCalendar calendar in _calendars)
                {
                    List<Event> eventsToPost = new();
                    var counter = calendar.Events.Count - 1;

                    while (counter >= 0 && calendar.Events[counter].End > today)
                    {
                        TimeSpan eventStartsInTimeSpan = calendar.Events[counter].Start - today;
                        if (!calendar.Events[counter].IsPosted && eventStartsInTimeSpan > TimeSpan.Zero && eventStartsInTimeSpan < TimeSpan.FromMinutes(10))
                            eventsToPost.Add(calendar.Events[counter]);

                        counter--;
                    }

                    await PostEventsAsync(eventsToPost, calendar.DGuildId, calendar.ChannelId, calendar.IcsCalendarId);
                }
            }
        }

        /// <summary>
        /// Should be be called separately when events concern multiple guilds.
        /// </summary>
        /// <param name="cEvents">A collection of events</param>
        /// <param name="calendarId">The Id of the calendar to change its state to 'posted'. Set to null if the state shouldn't change.</param>
        /// <param name="guildId">The guild the events are to be posted to</param>
        /// <param name="channelId">The channel in the event should be posted in</param>
        /// <returns></returns>
        public async Task PostEventsAsync(IEnumerable<Event> cEvents, ulong guildId, ulong channelId, int? calendarId = null)
        {
            foreach (Event cEvent in cEvents)
            {
                await (await Client.GetGuildAsync(guildId)).GetChannel(channelId).SendMessageAsync(cEvent.GenerateEmbedBuilder().Build());

                if (calendarId is not null)
                    _calendars.First(c => c.IcsCalendarId == calendarId).Events.First(e => e.EventId == cEvent.EventId).IsPosted = true;
            }
        }

        public string[] GetCalendarNames(ulong guildId) =>
            _calendars.Where(c => c.DGuildId == guildId).Select(c => c.Name).ToArray();

        public IcsCalendar? GetCalendarByName(ulong guildId, string name) =>
            _calendars.FirstOrDefault(c => c.DGuildId == guildId && c.Name == name);

        public Event? GetNextEvent(ulong guildId) =>
            _calendars.First(c => c.DGuildId == guildId).Events.FirstOrDefault(e => e.Start - DateTime.UtcNow > TimeSpan.Zero);

        public async Task AddCalendarAsync(ulong guildId, ulong channelId, Uri? icsFileUri = null)
        {
            var icsCalendar = new IcsCalendar
            {
                ChannelId = channelId,
                DGuildId = guildId
            };

            var calendar = Calendar.Load(await GetStreamFromUri(icsFileUri));
            icsCalendar.Name = calendar.Name;
            calendar.Events.ToList().ForEach(e => icsCalendar.Events.Add(e.ToEvent()));
            _calendars.Add(icsCalendar);

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

            using var database = new DataManager();
            await database.AddInstanceAsync(icsCalendar);
            await database.AddAllInstancesAsync(icsCalendar.Events);
        }

        public void DeleteCalendarAsync(IcsCalendar calendar)
        {
            _calendars.Remove(calendar);

            using var database = new DataManager();
            database.RemoveInstance(calendar);
        }

    }
}
