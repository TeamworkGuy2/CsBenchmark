using CsBenchmark.Time;

namespace CsBenchmark.Results
{
    public class ResultOutputOptions
    {
        public readonly TimeUnit DisplayTimeUnit;
        public readonly bool ShowWarmupInfo;


        public ResultOutputOptions(TimeUnit displayTimeUnit, bool showWarmupInfo)
        {
            this.DisplayTimeUnit = displayTimeUnit;
            this.ShowWarmupInfo = showWarmupInfo;
        }

    }
}
