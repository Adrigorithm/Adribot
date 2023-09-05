using Adribot.config;
using Adribot.data;
using Adribot.entities.discord;
using Adribot.services;
using Adribot.src.entities.utilities;
using Adribot.src.extensions;
using DSharpPlus;
using DSharpPlus.Entities;
using Ical.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Adribot.src.services
{
    public class DaySchemeService : BaseTimerService
    {
        private List<IcsCalendar> _calendars;

        public DaySchemeService(DiscordClient client, int timerInterval = 60) : base(client, timerInterval)
        {
        }

        public override async Task WorkAsync()
        {
            DateTimeOffset today = DateTimeOffset.UtcNow;

            if (!IsDatabaseDataLoaded)
            {
                using var database = new DataManager(Client);
                _calendars = database.GetIcsCalendarsNotExpired(today);

                IsDatabaseDataLoaded = true;
            }

            foreach (IcsCalendar calendar in _calendars)
            {
                List<Event> eventsToPost = new();
                int counter = calendar.Events.Count - 1;

                while (counter >= 0 && calendar.Events[counter].End > today)
                {
                    TimeSpan eventStartsInTimeSpan = calendar.Events[counter].Start - today;
                    if (!calendar.Events[counter].isPosted && eventStartsInTimeSpan > TimeSpan.Zero && eventStartsInTimeSpan < TimeSpan.FromMinutes(10))
                        eventsToPost.Add(calendar.Events[counter]);

                    counter--;
                }

                await PostEventsAsync(eventsToPost, calendar.DGuildId, calendar.ChannelId, calendar.IcsCalendarId);
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
                await (await Client.GetGuildAsync(guildId)).GetChannel(channelId).SendMessageAsync(new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = $"{cEvent.Organiser}" },
                    Color = new DiscordColor(Config.Configuration.EmbedColour),
                    Description = $"{cEvent.Summary}\n\n{cEvent.Description}\n\n[{cEvent.Start:HH}] - [{cEvent.End:HH}] @ {cEvent.Location}",
                    Title = cEvent.Name
                });

                if (calendarId is not null)
                    _calendars.First(c => c.IcsCalendarId == calendarId).Events.First(e => e.EventId == cEvent.EventId).isPosted = true;
            }
        }

        public Event? GetNextEventAsync(ulong guildId) =>
            _calendars.First(c => c.DGuildId == guildId).Events.FirstOrDefault(e => e.Start - DateTime.UtcNow > TimeSpan.Zero);

        public async Task AddCalendarAsync(ulong guildId, ulong channelId, Uri? icsFileUri = null, string icsFileText = null)
        {
            var icsCalendar = new IcsCalendar
            {
                ChannelId = channelId,
                DGuildId = guildId,
            };

            var calendar = Calendar.Load(string.IsNullOrEmpty(icsFileText) ? await GetRemoteFileContent(icsFileUri) : icsFileText);
            calendar.Events.ToList().ForEach(e => icsCalendar.Events.Add(e.ToEvent()));
            _calendars.Add(icsCalendar);

            using var database = new DataManager(Client);
            await database.AddInstanceAsync(icsCalendar);

            static async Task<string> GetRemoteFileContent(Uri uri)
            {
                using var httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(uri);

                return await response.Content.ReadAsStringAsync();
            }
        }

    }
}
