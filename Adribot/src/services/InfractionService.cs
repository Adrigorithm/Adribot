using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.EntityFrameworkCore;

static class InfractionService
{
    private static DiscordClient _client;
    private static AdribotContext _adribotContext;
    private static Timer _timer;

    public static async void Init(DiscordClient client, string connectionString)
    {
        _client = client;
        _adribotContext = new(connectionString);
        await PrepareDatabaseAsync();

        TimerCallback timerCallback = Tick;
        _timer = new(timerCallback, null, 0, 1000);

        _client.GuildMemberUpdated += GuildMemberUpdatedAsync;
        _client.UserUpdated += UserUpdatedAsync;
    }

    private static async Task PrepareDatabaseAsync()
    {
        await _adribotContext.Database.OpenConnectionAsync();

        // Remove Guilds bot is no longer in.
        // Add Guilds bot was invited to.
        if (_adribotContext.Guilds.Count() > 0)
        {
            _adribotContext.Guilds.RemoveRange(_adribotContext.Guilds.Where(dg => !_client.Guilds.Keys.Contains(dg.GuildId)));
            await _adribotContext.Guilds.AddRangeAsync(_client.Guilds.Where(g => !_adribotContext.Guilds.Any(dg => dg.GuildId == g.Key)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ToDGuild());
        }
        else
        {
            await _adribotContext.Guilds.AddRangeAsync(_client.Guilds.ToDictionary(kvp => kvp.Key, kvp => kvp.Value).ToDGuild());
        }

        await _adribotContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Guilds ON");
        await _adribotContext.SaveChangesAsync();
        await _adribotContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Guilds OFF");

        await _adribotContext.Database.CloseConnectionAsync();
    }

    private static async Task UserUpdatedAsync(DiscordClient sender, UserUpdateEventArgs e) => await HoistCheckAsync((DiscordMember)e.UserAfter);

    private static async Task GuildMemberUpdatedAsync(DiscordClient sender, GuildMemberUpdateEventArgs e) => await HoistCheckAsync(e.MemberAfter);

    private static async Task HoistCheckAsync(DiscordMember memberAfter)
    {
        if (memberAfter.DisplayName != "💩")
        {
            if (_adribotContext.Infractions.Any(i => i.MemberId == memberAfter.Id && i.Type == InfractionType.HOIST && !i.isExpired))
            {
                await memberAfter.ModifyAsync(m => m.Nickname = "💩");
            }
            else if ((byte)memberAfter.DisplayName[0] < 48)
            {
                await memberAfter.ModifyAsync(m => m.Nickname = "💩");
                await _adribotContext.Infractions.AddAsync(new()
                {
                    Date = DateTime.UtcNow,
                    Duration = TimeSpan.FromDays(1),
                    GuildId = memberAfter.Guild.Id,
                    isExpired = false,
                    Type = InfractionType.HOIST,
                    MemberId = memberAfter.Id
                });

                await _adribotContext.SaveChangesAsync();
            }
        }
    }

    private static void Tick(object stateInfo)
    {
        for (int i = 0; i < _adribotContext.Infractions.Count(); i++)
        {
            var infraction = _adribotContext.Infractions.ElementAt(i);
            if (!infraction.isExpired && infraction.Date + infraction.Duration < DateTime.UtcNow)
                Task.Run(async () => await LiftInfractionAsync(infraction));
        }
    }

    private static async Task LiftInfractionAsync(Infraction infraction)
    {
        try
        {
            switch (infraction.Type)
            {
                case InfractionType.HOIST:
                    await (await _client.GetGuildAsync(infraction.GuildId)).Members[infraction.MemberId].ModifyAsync(m => m.Nickname = "");
                    break;
                case InfractionType.MUTE:
                    await (await _client.GetGuildAsync(infraction.GuildId)).Members[infraction.MemberId].ModifyAsync(m => m.Muted = false);
                    break;
                case InfractionType.BAN:
                    await (await _client.GetGuildAsync(infraction.GuildId)).Members[infraction.MemberId].UnbanAsync();
                    break;
            }

            _adribotContext.Infractions.First(i => i.InfractionId == infraction.InfractionId).isExpired = true;
            await _adribotContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Infraction of type {infraction.Type.ToString()} on <@{infraction.MemberId}> could not be reclused.{Environment.NewLine}The following Exception was thrown: {e.Message}");
        }
    }
}