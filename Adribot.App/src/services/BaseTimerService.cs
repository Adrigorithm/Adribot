using System.Threading;
using System.Threading.Tasks;
using Adribot.Config;
using Adribot.Services.Providers;
using Discord.WebSocket;

namespace Adribot.Services;

public abstract class BaseTimerService : ITimerService
{

    private Timer _timer;

    /// <summary>
    ///     Initiates service dependencies.
    /// </summary>
    /// <param name="client">The client on which the service should operate</param>
    /// <param name="timerInterval">Time in seconds between each timer tick.</param>
    protected BaseTimerService(DiscordClientProvider clientProvider, SecretsProvider? secretsProvider = null, int timerInterval = 10)
    {
        Client = clientProvider.Client;
        Config = secretsProvider?.Config;

        Start(timerInterval);
    }
    protected DiscordSocketClient Client { get; init; }
    protected ConfigValueType? Config { get; init; }

    public void Start(int timerInterval) =>
        _timer = new Timer(CallbackAsync, null, 0, timerInterval * 1000);

    public async Task Stop() =>
        await _timer.DisposeAsync();

    public virtual async Task Work() =>
        await Task.CompletedTask;

    private async void CallbackAsync(object? state) =>
        await Work();
}
