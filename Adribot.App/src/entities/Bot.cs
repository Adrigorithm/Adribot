using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Adribot.Data.Repositories;
using Adribot.Extensions;
using Adribot.Services.Providers;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Adribot.Entities;

public class Bot
{
    private readonly DGuildRepository _dGuildRepository;
    private readonly DiscordClientProvider _clientProvider;
    private readonly InteractionService _interactionService;
    private IServiceProvider _serviceProvider;

    public Bot(DiscordClientProvider clientProvider, DGuildRepository dGuildRepository)
    {
        _clientProvider = clientProvider;
        _dGuildRepository = dGuildRepository;
        _interactionService = new(_clientProvider.Client);

        clientProvider.Client.Ready += ReadyAsync;
        clientProvider.Client.MessageReceived += MessageReceivedAsync;
        clientProvider.Client.InteractionCreated += InteractionCreatedAsync;
    }

    private async Task InteractionCreatedAsync(SocketInteraction interaction)
    {
        var ctx = new SocketInteractionContext(_clientProvider.Client, interaction);
        await _interactionService.ExecuteCommandAsync(ctx, _serviceProvider);
    }

    public async Task StartAsync(string token, IServiceProvider services, TokenType tokenType = TokenType.Bot)
    {
        _serviceProvider = services;
        await _clientProvider.Client.LoginAsync(tokenType, token);
        await _clientProvider.Client.StartAsync();
    }

    public async Task StopAsync() =>
        await _clientProvider.Client.LogoutAsync();

    private async Task MessageReceivedAsync(SocketMessage message)
    {
        if (message.Channel is not ITextChannel)
            await Task.CompletedTask;

        IGuildUser? user = GetGuildUserOrDefault(message.Author);

        if (user is null || user.GuildPermissions.Administrator)
            await Task.CompletedTask;

        if (message.MentionedUsers.Any(u =>
        {
            IGuildUser? user = GetGuildUserOrDefault(u);
            return user is null
                ? false
                : user.GuildPermissions.Administrator;
        }))
        {
            await message.AddReactionAsync(Emoji.Parse("💢"));
        }

        IGuildUser? GetGuildUserOrDefault(SocketUser socketUser) =>
            socketUser switch
            {
                IThreadUser threadUser => threadUser.GuildUser,
                IGuildUser guildUser => guildUser,
                _ => null
            };
    }

    private async Task ReadyAsync()
    {
        _serviceProvider.GetServices<object>().ToList().ForEach(s => Console.WriteLine(s.GetType().Name));

        await _interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), _serviceProvider);
        await _interactionService.RegisterCommandsGloballyAsync();

        IEnumerable<SocketGuild> guilds = _clientProvider.Client.Guilds;
        FrozenDictionary<ulong, ulong[]> guildMembers = _dGuildRepository.GetGuildsWithMembers();

        for (var i = 0; i < guilds.Count(); i++)
        {
            SocketGuild guildCurrent = guilds.ElementAt(i);
            var isCachedGuild = guildMembers.ContainsKey(guildCurrent.Id);

            if (!isCachedGuild)
            {
                _dGuildRepository.AddDGuild(guildCurrent.ToDGuild());
                List<(ulong, string)> membersToAdd = [];

                foreach (SocketGuildUser member in guildCurrent.Users)
                    membersToAdd.Add((member.Id, member.Mention));

                _dGuildRepository.AddMembersToGuild(guildCurrent.Id, membersToAdd);
            }
            else
            {
                var cachedMembers = guildMembers[guildCurrent.Id].ToHashSet();
                List<(ulong, string)> membersToAdd = [];

                foreach (SocketGuildUser newMember in guildCurrent.Users)
                {
                    if (cachedMembers.Add(newMember.Id))
                        membersToAdd.Add((newMember.Id, newMember.Mention));
                }

                _dGuildRepository.AddMembersToGuild(guildCurrent.Id, membersToAdd);
            }
        }

        await Task.CompletedTask;
    }
}
