using System.Threading.Tasks;

namespace Adribot.src.services;

public interface ITimerService
{
    Task Work();
    Task Start(int timerInterval);
    Task Stop();
}
