using System;
using System.Diagnostics;

namespace CsBenchmark.Extensions
{
    public static class StopwatchExtensions
    {

        public static long StopNanos(this Stopwatch timer)
        {
            var res = timer.ElapsedTicks * (1000000 / TimeSpan.TicksPerMillisecond);
            timer.Stop();
            return res;
        }

    }
}
