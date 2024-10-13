using System.Threading.Tasks;

namespace Adribot.Services;

public interface ITimerService
{
    Task Work();
    void Start(int timerInterval);
    Task Stop();
}
