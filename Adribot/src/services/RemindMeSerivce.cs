using Adribot.config;
using Adribot.data;
using Adribot.entities.utilities;
using Adribot.services;
using DSharpPlus;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adribot.src.services
{
    public class RemindMeSerivce : BaseTimerService
    {
        private List<Reminder> _reminders = new();

        public RemindMeSerivce(DiscordClient client, int timerInterval = 10) : base(client, timerInterval) { }

        public override async Task Start(int timerInterval) =>
            await base.Start(timerInterval);

        public override async Task WorkAsync()
        {
            using var database = new DataManager(Client);

            if (_reminders.Count == 0)
                _reminders = database.GetRemindersToOld();

            Reminder? reminder = _reminders.Count > 0 && _reminders[0].EndDate.CompareTo(DateTimeOffset.UtcNow) <= 0 ? _reminders[0] : null;

            if (reminder is not null)
            {
                DiscordMember member = await (await Client.GetGuildAsync(reminder.DGuildId)).GetMemberAsync(reminder.DMemberId);

                DiscordMessageBuilder remindMessage = new DiscordMessageBuilder().WithEmbed(new DiscordEmbedBuilder()
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = Convert.ToString(member is null ? "404" : $"<@{member.Id}") },
                    Color = new DiscordColor(Config.Configuration.EmbedColour),
                    Description = reminder.Content,
                    Timestamp = reminder.Date,
                    Title = "You wanted to be reminded of the following:"
                });

                DiscordGuild guild = await Client.GetGuildAsync(reminder.DGuildId);
                _ = reminder.Channel is null
                    ? await (await guild.GetMemberAsync(reminder.DMemberId)).SendMessageAsync(remindMessage)
                    : await guild.Channels[(ulong)reminder.Channel].SendMessageAsync(remindMessage);

                _reminders.Remove(reminder);
                database.RemoveInstance(reminder);
            }
        }

        public async Task AddRemindMeAsync(Reminder reminder)
        {
            int indexOlderReminder = _reminders.FindIndex(r => r.EndDate.CompareTo(reminder.EndDate) > 0);
            if (indexOlderReminder == -1)
                _reminders.Add(reminder);
            else
                _reminders.Insert(indexOlderReminder, reminder);

            using var database = new DataManager(Client);
            await database.AddInstanceAsync(reminder);
        }
    }
}
