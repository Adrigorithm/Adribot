using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

class InfractionService : BackgroundService
{
    private readonly ILogger<InfractionService> _logger;
    private List<Infraction> _infractions;

    public InfractionService(ILogger<InfractionService> logger) => _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken){
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("TimeoutService running.");
            await Task.Delay(1000, stoppingToken);
        }
    }
}