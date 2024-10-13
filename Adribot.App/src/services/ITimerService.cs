using System.Threading.Tasks;

namespace Adribot.services;

public interface ITimerService
{
    Task Work();
    void Start(int timerInterval);
    Task Stop();
}
