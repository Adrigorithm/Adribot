using System.Threading.Tasks;

namespace Adribot.services;

public interface ITimerService
{
    Task Work();
    Task Start(int timerInterval);
    Task Stop();
}