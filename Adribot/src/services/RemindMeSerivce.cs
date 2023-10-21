using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adribot.src.config;
using Adribot.src.data;
using Adribot.src.entities.utilities;
using DSharpPlus;
using DSharpPlus.Entities;

namespace Adribot.src.services
{
    public class RemindMeSerivce : BaseTimerService
    {
        private readonly List<Reminder> _reminders = new();

        public RemindMeSerivce(DiscordClient client, int timerInterval = 10) : base(client, timerInterval)
        {
            using var database = new DataManager();
            _reminders = database.GetRemindersToOld();
        }
        public override async Task Start(int timerInterval) =>
            await base.Start(timerInterval);

        public override async Task WorkAsync()
        {
            if (_reminders.Count > 0)
            {
                using var database = new DataManager();
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
        }

        public async Task AddRemindMeAsync(Reminder reminder)
        {
            int indexOlderReminder = _reminders.FindIndex(r => r.EndDate.CompareTo(reminder.EndDate) > 0);
            if (indexOlderReminder == -1)
                _reminders.Add(reminder);
            else
                _reminders.Insert(indexOlderReminder, reminder);

            using var database = new DataManager();
            await database.AddInstanceAsync(reminder);
        }
    }
}
