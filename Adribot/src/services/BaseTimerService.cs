using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;

namespace Adribot.services;

public abstract class BaseTimerService : ITimerService
{
    protected DiscordClient Client;
    private  Timer _timer;

    /// <summary>
    /// Initiates service dependencies.
    /// </summary>
    /// <param name="client">The client on which the service should operate</param>
    /// <param name="timerInterval">Time in seconds between each timer tick.</param>
    protected BaseTimerService(DiscordClient client, int timerInterval = 10)
    {
        Client = client;
        Task.Run(async () => await Start(timerInterval));
    }
    
    private async void CallbackAsync(object? state) =>
        await Work();

    public virtual async Task Start(int timerInterval)
    {
        _timer = new Timer(CallbackAsync, null, 0, timerInterval * 1000);
        await Task.CompletedTask;
    }

    public async Task Stop() =>
        await _timer.DisposeAsync();

    public virtual async Task Work() => await Task.CompletedTask;
}