using System.Threading;
using System.Threading.Tasks;
using Adribot.src.config;
using Adribot.src.services.providers;
using DSharpPlus;

namespace Adribot.src.services;

public abstract class BaseTimerService : ITimerService
{
    protected DiscordClient Client { get; init; }
    protected ConfigValueType? Config { get; init; }
    protected bool IsDatabaseDataLoaded;

    private Timer _timer;
    private bool _isDownloadCompleted;

    /// <summary>
    /// Initiates service dependencies.
    /// </summary>
    /// <param name="client">The client on which the service should operate</param>
    /// <param name="timerInterval">Time in seconds between each timer tick.</param>
    protected BaseTimerService(DiscordClientProvider clientProvider, SecretsProvider? secretsProvider = null, int timerInterval = 10)
    {
        Client = clientProvider.Client;
        Client.GuildDownloadCompleted += GuildsReady;
        Config = secretsProvider?.Config;

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
            await Work();
    }

    public virtual async Task Start(int timerInterval)
    {
        _timer = new Timer(CallbackAsync, null, 0, timerInterval * 1000);
        await Task.CompletedTask;
    }

    public async Task Stop() =>
        await _timer.DisposeAsync();

    public virtual async Task Work() => await Task.CompletedTask;
}
