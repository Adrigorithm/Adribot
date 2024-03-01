using System;
using System.Text;
using System.Threading.Tasks;
using Adribot.src.commands.fun;
using Adribot.src.commands.moderation;
using Adribot.src.commands.utilities;
using Adribot.src.data;
using Adribot.src.data.repositories;
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

    public Bot()
    {
        var secrets = new SecretsProvider();

        _client = new(new DiscordConfiguration
        {
            Token = secrets.Config.BotToken,
            Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
        });

        ServiceProvider services = new ServiceCollection()
            .AddSingleton(secrets)
            .AddDbContext<AdribotContext>()
            .AddSingleton(new DiscordClientProvider(_client))
            .AddSingleton<DGuildRepository>()
            .AddSingleton<InfractionRepository>()
            .AddSingleton<RemindMeRepository>()
            .AddSingleton<IcsCalendarRepository>()
            .AddSingleton<DGuildRepository>()
            .AddSingleton<EventsDataService>()
            .AddSingleton<TagRepository>()
            .AddSingleton<BaseTimerService, InfractionService>()
            .AddSingleton<BaseTimerService, RemindMeSerivce>()
            .AddSingleton<BaseTimerService, IcsCalendarService>()
            .AddSingleton<BaseTimerService, StarboardService>()
            .AddSingleton<TagService>()
            .BuildServiceProvider();

        SlashCommandsExtension slashies = _client.UseSlashCommands(new SlashCommandsConfiguration()
        {
            Services = services
        });

        _client.MessageCreated += MessageCreatedAsync;
        slashies.SlashCommandErrored += SlashCommandErroredAsync;

        slashies.RegisterCommands<AdminCommands>(1153306877288001629);
        slashies.RegisterCommands<MinecraftCommands>();
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
            pingedAdmin = (await args.Guild.GetMemberAsync(args.MentionedUsers[counter].Id))?.Permissions.HasPermission(Permissions.Administrator) ?? false;
            counter++;
        }

        if (member is not null &&
            !args.Channel.IsPrivate &&
            !member.IsBot &&
            !member.Permissions.HasPermission(Permissions.Administrator) &&
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

    public async Task StartAsync() =>
        await _client.ConnectAsync();
    public async Task StopAsync() =>
        await _client.DisconnectAsync();
}
