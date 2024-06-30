using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.data;
using Adribot.src.data.repositories;
using Adribot.src.extensions;
using Adribot.src.services;
using Adribot.src.services.providers;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.DependencyInjection;

namespace Adribot.src.entities;

public class Bot
{
    private readonly DiscordClient _client;
    private readonly DGuildRepository _dGuildRepository;

    public Bot()
    {
        var secrets = new SecretsProvider();

        IServiceCollection services = new ServiceCollection()
            .AddSingleton(secrets)
            .AddDbContext<AdribotContext>()
            .AddSingleton<RemoteAccessService>()
            .AddSingleton<DGuildRepository>()
            .AddSingleton<InfractionRepository>()
            .AddSingleton<RemindMeRepository>()
            .AddSingleton<IcsCalendarRepository>()
            .AddSingleton<TagRepository>()
            .AddSingleton<InfractionService>()
            .AddSingleton<RemindMeSerivce>()
            .AddSingleton<IcsCalendarService>()
            .AddSingleton<StarboardService>()
            .AddSingleton<TagService>();

        var clientBuilder = DiscordClientBuilder.CreateDefault(secrets.Config.BotToken, DiscordIntents.All | DiscordIntents.MessageContents, services);
        clientBuilder.ConfigureEventHandlers(
            ehb => {
                ehb.HandleGuildDownloadCompleted(GuildDownloadCompletedAsync);
                ehb.HandleMessageCreated(MessageCreatedAsync);
            }
        );

        _client = clientBuilder.Build();

        _dGuildRepository = _client.ServiceProvider.GetService<DGuildRepository>();

    }

    private async Task MessageCreatedAsync(DiscordClient sender, MessageCreatedEventArgs args)
    {
        var member = args.Author as DiscordMember;
        var pingedAdmin = false;
        var counter = 0;

        while (counter < args.MentionedUsers.Count && !pingedAdmin)
        {
            pingedAdmin = (await args.Guild.GetMemberAsync(args.MentionedUsers[counter].Id))?.Permissions.HasPermission(DiscordPermissions.Administrator) ?? false;
            counter++;
        }

        if (member is not null &&
            !args.Channel.IsPrivate &&
            !member.IsBot &&
            !member.Permissions.HasPermission(DiscordPermissions.Administrator) &&
            pingedAdmin)
        {
            await args.Message.CreateReactionAsync(DiscordEmoji.FromUnicode("💢"));
        }
    }

    private async Task GuildDownloadCompletedAsync(DiscordClient sender, GuildDownloadCompletedEventArgs e)
    {
        IEnumerable<DiscordGuild> guilds = sender.Guilds.Values;
        FrozenDictionary<ulong, ulong[]> guildMembers = _dGuildRepository.GetGuildsWithMembers();

        for (var i = 0; i < guilds.Count(); i++)
        {
            DiscordGuild guildCurrent = guilds.ElementAt(i);
            var isCachedGuild = guildMembers.ContainsKey(guildCurrent.Id);

            if (!isCachedGuild)
            {
                _dGuildRepository.AddDGuild(guildCurrent.ToDGuild());
                List<(ulong, string)> membersToAdd = [];

                await foreach (DiscordMember member in guildCurrent.GetAllMembersAsync())
                    membersToAdd.Add((member.Id, member.Mention));

                _dGuildRepository.AddMembersToGuild(guildCurrent.Id, membersToAdd);
            }
            else
            {
                var cachedMembers = guildMembers[guildCurrent.Id].ToHashSet();
                List<(ulong, string)> membersToAdd = [];

                foreach (KeyValuePair<ulong, DiscordMember> newMember in guildCurrent.Members)
                {
                    if (cachedMembers.Add(newMember.Key))
                        membersToAdd.Add((newMember.Key, newMember.Value.Mention));
                }

                _dGuildRepository.AddMembersToGuild(guildCurrent.Id, membersToAdd);
            }
        }
    }

    public async Task StartAsync() =>
        await _client.ConnectAsync();
    public async Task StopAsync() =>
        await _client.DisconnectAsync();
}
