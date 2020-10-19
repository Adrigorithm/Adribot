using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Adribot.timers
{
    public class BaseTimer
    {
        public Timer GlobalTimer { get; private set; }

        public void SetupTimer(double interval, bool enable, bool autoReset) {
            GlobalTimer = new Timer(interval) { Enabled = enable, AutoReset = autoReset };
        }
    }
}
