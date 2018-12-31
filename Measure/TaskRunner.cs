using System;
using System.Diagnostics;
using CsBenchmark.Time;

namespace CsBenchmark.Measure
{
    public class TaskRunner
    {

        public static R RunTimedLoop<R>(int loops, Func<int, R> action, PerformanceLoopLog perfLog)
        {
            var s = Stopwatch.StartNew();
            for (int j = 0; j < loops; j++)
            {
                var res = action(j);
                // to prevent look from removed and 'action' run once and result returned
                if (j == loops - 1)
                {
                    s.Stop();
                    perfLog.AddLoopTime(TimeUnit.TickToNs(s.Elapsed.Ticks));
                    return res;
                }
            }
            return default(R);
        }

    }
}
