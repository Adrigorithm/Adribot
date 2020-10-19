using System.Collections.Generic;
using System.Timers;
using Adribot.src.entities;
using Adribot.src.services.dbGateway;
using Adribot.timers;

namespace Adribot.services
{
    public static class BanService
    {
        private static readonly BaseTimer _timer;
        private static List<Ban> _bans;

        static BanService()
        {
            _timer = new BaseTimer();
            _timer.SetupTimer(10000, true, true);
            _timer.GlobalTimer.Elapsed += GlobalTimerOnElapsed;
            GetBans(10);
        }

        private static void GlobalTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private static void GetBans(int amount) {
            Database database = new Database();
            var bans = database.GetObjectList(typeof(Ban));
        }
    }
}