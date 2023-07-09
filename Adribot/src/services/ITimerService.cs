using System.Threading.Tasks;

namespace Adribot.services;

public interface ITimerService
{
    Task WorkAsync();
    Task Start(int timerInterval);
    Task Stop();
}
