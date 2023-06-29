using System.Threading.Tasks;
using DSharpPlus;

namespace Adribot.services;

public sealed class InfractionService : BaseTimerService
{
    public InfractionService(DiscordClient client, int timerInterval = 10) : base(client, timerInterval)
    {

    }

    public override async Task Start(int timerInterval) =>
        await base.Start(timerInterval);

    public override Task Work() => base.Work();

    //private async Task<List<DGuild>> InitiateInfractionsAsync() => await Task.CompletedTask;
}
