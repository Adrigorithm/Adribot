using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.Data.Repositories;
using Adribot.Entities.Utilities;
using Adribot.Services.Providers;
using Discord;
using Discord.WebSocket;

namespace Adribot.Services;

public sealed class RemindMeService(
    RemindMeRepository remindMeRepository,
    DiscordClientProvider client,
    SecretsProvider secretsProvider,
    int timerInterval = 10)
    : BaseTimerService(client, secretsProvider, timerInterval)
{
    private IEnumerable<Reminder>? _reminders;

    public override async Task Work()
    {
        _reminders ??= remindMeRepository.GetRemindersToOld();

        if (!_reminders.Any())
            return;
        
        Reminder? reminder = _reminders.First().EndDate.CompareTo(DateTimeOffset.UtcNow) <= 0 
            ? _reminders.First() 
            : null;
        
        if (reminder == null)
            return;
        
        var embed = new EmbedBuilder
        {
            Color = Config.EmbedColour,
            Description = reminder.Content,
            Timestamp = reminder.Date,
            Title = "You wanted to be reminded of the following:"
        };

        SocketGuild guild = Client.Guilds.First(g => g.Id == reminder.DMember.DGuild.GuildId);
        _ = reminder.Channel is null
            ? await guild.GetUser(reminder.DMember.MemberId).SendMessageAsync(embed: embed.Build())
            : await ((ITextChannel)guild.Channels.First(c => c.Id == (ulong)reminder.Channel)).SendMessageAsync(reminder.DMember.Mention, embed: embed.Build(), allowedMentions: AllowedMentions.All);

        _reminders.ToList().Remove(reminder);
        remindMeRepository.RemoveReminder(reminder);
    }

    public void AddRemindMe(ulong guildId, ulong memberId, ulong? channelId, string content, DateTimeOffset endDate)
    {
        Reminder reminder = remindMeRepository.AddRemindMe(guildId, memberId, channelId, content, endDate);

        var indexOlderReminder = _reminders.Count() > 0
            ? _reminders.ToList().FindIndex(r => r.EndDate.CompareTo(reminder.EndDate) > 0)
            : -1;

        if (indexOlderReminder == -1)
            _reminders.Append(reminder);
        else
            _reminders.ToList().Insert(indexOlderReminder, reminder);
    }
}
