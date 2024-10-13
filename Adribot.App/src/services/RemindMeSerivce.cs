using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.data.repositories;
using Adribot.entities.utilities;
using Adribot.services.providers;
using Discord;
using Discord.WebSocket;

namespace Adribot.services;

public sealed class RemindMeSerivce : BaseTimerService
{
    private readonly RemindMeRepository _remindMeRespository;
    private readonly IEnumerable<Reminder> _reminders = [];

    public RemindMeSerivce(RemindMeRepository remindMeRepository, DiscordClientProvider client, SecretsProvider secretsProvider, int timerInterval = 10) : base(client, secretsProvider, timerInterval)
    {
        _remindMeRespository = remindMeRepository;
        _reminders = _remindMeRespository.GetRemindersToOld();
    }

    public override async Task Work()
    {
        if (_reminders.Count() > 0)
        {
            Reminder? reminder = _reminders.Count() > 0 && _reminders.First().EndDate.CompareTo(DateTimeOffset.UtcNow) <= 0 ? _reminders.First() : null;

            if (reminder is not null)
            {
                var embed = new EmbedBuilder
                {
                    Color = new Color(Convert.ToUInt16(Config.EmbedColour)),
                    Description = reminder.Content,
                    Timestamp = reminder.Date,
                    Title = "You wanted to be reminded of the following:"
                };

                SocketGuild guild = Client.Guilds.First(g => g.Id == reminder.DMember.DGuild.GuildId);
                _ = reminder.Channel is null
                    ? await guild.GetUser(reminder.DMember.MemberId).SendMessageAsync(embed: embed.Build())
                    : await ((ITextChannel)guild.Channels.First(c => c.Id == (ulong)reminder.Channel)).SendMessageAsync(reminder.DMember.Mention, embed: embed.Build(), allowedMentions: AllowedMentions.All);

                _reminders.ToList().Remove(reminder);
                _remindMeRespository.RemoveReminder(reminder);
            }
        }
    }

    public void AddRemindMe(ulong guildId, ulong memberId, ulong? channelId, string content, DateTimeOffset endDate)
    {
        Reminder reminder = _remindMeRespository.AddRemindMe(guildId, memberId, channelId, content, endDate);

        var indexOlderReminder = _reminders.Count() > 0
            ? _reminders.ToList().FindIndex(r => r.EndDate.CompareTo(reminder.EndDate) > 0)
            : -1;

        if (indexOlderReminder == -1)
            _reminders.Append(reminder);
        else
            _reminders.ToList().Insert(indexOlderReminder, reminder);
    }
}
