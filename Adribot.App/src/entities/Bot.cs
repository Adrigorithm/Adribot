using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adribot.src.commands.fun;
using Adribot.src.commands.moderation;
using Adribot.src.commands.utilities;
using Adribot.src.data;
using Adribot.src.data.repositories;
using Adribot.src.extensions;
using Adribot.src.services;
using Adribot.src.services.providers;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;
using Microsoft.Extensions.DependencyInjection;

namespace Adribot.src.entities;

public class Bot
{
    private readonly DiscordClient _client;
    private readonly DGuildRepository _dGuildRepository;

    public Bot()
    {
        var secrets = new SecretsProvider();

        _client = new(new DiscordConfiguration
        {
            Token = secrets.Config.BotToken,
            Intents = DiscordIntents.All | DiscordIntents.MessageContents
        });

        ServiceProvider services = new ServiceCollection()
            .AddSingleton(secrets)
            .AddDbContext<AdribotContext>()
            .AddSingleton(new DiscordClientProvider(_client))
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
            .AddSingleton<TagService>()
            .BuildServiceProvider();

        SlashCommandsExtension slashies = _client.UseSlashCommands(new SlashCommandsConfiguration()
        {
            Services = services
        });

        _dGuildRepository = services.GetService<DGuildRepository>();

        //_client.MessageCreated += MessageCreatedAsync;
        slashies.SlashCommandErrored += SlashCommandErroredAsync;
        _client.GuildDownloadCompleted += GuildDownloadCompletedAsync;

        slashies.RegisterCommands<AdminCommands>(1153306877288001629);
        slashies.RegisterCommands<MinecraftCommands>();
        slashies.RegisterCommands<RemoteAccessCommands>(574341132826312736);
        slashies.RegisterCommands<FunCommands>(1153306877288001629);
        slashies.RegisterCommands<UtilityCommands>(1153306877288001629);
        slashies.RegisterCommands<TagCommands>(1153306877288001629);
        slashies.RegisterCommands<CalendarCommands>(1153306877288001629);
    }

    private async Task MessageCreatedAsync(DiscordClient sender, MessageCreateEventArgs args)
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

    private Task SlashCommandErroredAsync(SlashCommandsExtension sender, SlashCommandErrorEventArgs args)
    {
        var sb = new StringBuilder("Command `");
        sb.Append(args.Context.CommandName);
        sb.Append("` failed: ");
        sb.Append(args.Exception.Message);

        var devider = new string('=', sb.Length);

        Console.WriteLine($"\n{devider}\n\n{sb}\n\n{devider}\n");

        return Task.CompletedTask;
    }

    private async Task GuildDownloadCompletedAsync(DiscordClient sender, GuildDownloadCompletedEventArgs e)
    {
        //await _client.DeleteGlobalApplicationCommandAsync(1231741399779639327);

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
