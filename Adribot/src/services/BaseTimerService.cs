using DSharpPlus;
using System.Threading;
using System.Threading.Tasks;

namespace Adribot.src.services;

public abstract class BaseTimerService : ITimerService
{
    protected DiscordClient Client;
    protected bool IsDatabaseDataLoaded;

    private Timer _timer;
    private bool _isDownloadCompleted;

    /// <summary>
    /// Initiates service dependencies.
    /// </summary>
    /// <param name="client">The client on which the service should operate</param>
    /// <param name="timerInterval">Time in seconds between each timer tick.</param>
    protected BaseTimerService(DiscordClient client, int timerInterval = 10)
    {
        Client = client;
        Client.GuildDownloadCompleted += GuildsReady;
        Task.Run(async () => await Start(timerInterval));
    }

    private Task GuildsReady(DiscordClient sender, DSharpPlus.EventArgs.GuildDownloadCompletedEventArgs args)
    {
        _isDownloadCompleted = true;
        return Task.CompletedTask;
    }

    private async void CallbackAsync(object? state)
    {
        if (_isDownloadCompleted)
            await WorkAsync();
    }

    public virtual async Task Start(int timerInterval)
    {
        _timer = new Timer(CallbackAsync, null, 0, timerInterval * 1000);
        await Task.CompletedTask;
    }

    public async Task Stop() =>
        await _timer.DisposeAsync();

    public virtual async Task WorkAsync() => await Task.CompletedTask;
}
