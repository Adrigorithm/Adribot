using System.Threading.Tasks;

namespace Adribot.src.services;

public interface ITimerService
{
    Task Work();
    void Start(int timerInterval);
    Task Stop();
}
