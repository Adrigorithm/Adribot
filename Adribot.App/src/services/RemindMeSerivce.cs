using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.data.repositories;
using Adribot.src.entities.utilities;
using Adribot.src.services.providers;
using DSharpPlus.Entities;

namespace Adribot.src.services;

public sealed class RemindMeSerivce : BaseTimerService
{
    private readonly RemindMeRepository _remindMeRespository;
    private readonly List<Reminder> _reminders = [];

    public RemindMeSerivce(RemindMeRepository remindMeRepository, DiscordClientProvider client, SecretsProvider secretsProvider, int timerInterval = 10) : base(client, secretsProvider, timerInterval)
    {
        _remindMeRespository = remindMeRepository;
        _reminders = _remindMeRespository.GetRemindersToOld().ToList();
    }

    public override async Task Work()
    {
        if (_reminders.Count > 0)
        {
            Reminder? reminder = _reminders.Count > 0 && _reminders[0].EndDate.CompareTo(DateTimeOffset.UtcNow) <= 0 ? _reminders[0] : null;

            if (reminder is not null)
            {
                DiscordMessageBuilder remindMessage = new DiscordMessageBuilder()
                    .WithContent($"{reminder.DMember.Mention}")
                    .AddEmbed(new DiscordEmbedBuilder()
                    {
                        Color = new DiscordColor(Config.EmbedColour),
                        Description = reminder.Content,
                        Timestamp = reminder.Date,
                        Title = "You wanted to be reminded of the following:"
                    });
                remindMessage.WithAllowedMention(new UserMention(reminder.DMember.MemberId));

                DiscordGuild guild = await Client.GetGuildAsync(reminder.DMember.DGuild.GuildId);
                _ = reminder.Channel is null
                    ? await (await guild.GetMemberAsync(reminder.DMember.MemberId)).SendMessageAsync(remindMessage)
                    : await guild.Channels[(ulong)reminder.Channel].SendMessageAsync(remindMessage);

                _reminders.Remove(reminder);
                _remindMeRespository.RemoveReminder(reminder);
            }
        }
    }

    public void AddRemindMe(ulong guildId, ulong memberId, ulong? channelId, string content, DateTimeOffset endDate)
    {
        Reminder reminder = _remindMeRespository.AddRemindMe(guildId, memberId, channelId, content, endDate);

        var indexOlderReminder = _reminders.Count > 0 
            ? _reminders.FindIndex(r => r.EndDate.CompareTo(reminder.EndDate) > 0)
            : -1;

        if (indexOlderReminder == -1)
            _reminders.Add(reminder);
        else
            _reminders.Insert(indexOlderReminder, reminder);
    }
}
