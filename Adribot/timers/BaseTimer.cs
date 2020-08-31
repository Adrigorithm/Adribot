using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Adribot.timers
{
    public abstract class BaseTimer
    {
        protected Timer GlobalTimer { get; private set; }

        protected void SetupTimer(double interval, bool enable, bool autoReset)
        {
            GlobalTimer = new Timer(interval) {Enabled = enable, AutoReset = autoReset};
        }
    }
}
