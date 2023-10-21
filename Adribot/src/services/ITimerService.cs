using System.Threading.Tasks;

namespace Adribot.src.services;

public interface ITimerService
{
    Task WorkAsync();
    Task Start(int timerInterval);
    Task Stop();
}
