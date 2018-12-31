using System;

namespace CsBenchmark.Time
{
    public class TimeUnit
    {
        public static readonly TimeUnit NANO = new TimeUnit(0, 1.0 / (1000 * 1000 * 1000), "nanosecond", "ns");
        public static readonly TimeUnit MICRO = new TimeUnit(1, 1.0 / (1000 * 1000), "microsecond", "mc");
        public static readonly TimeUnit MILLI = new TimeUnit(2, 1.0 / 1000, "millisecond", "ms");
        public static readonly TimeUnit SEC = new TimeUnit(3, 1.0, "second", "sec");

        public int Ordinal { get; private set; }
        private readonly double oneSecondScale;
        public readonly string FullName;
        public readonly string ShortName;


        public TimeUnit(int ordinal, double oneSecondScale, string fullName, string shortName)
        {
            this.Ordinal = ordinal;
            this.oneSecondScale = oneSecondScale;
            this.FullName = fullName;
            this.ShortName = shortName;
        }


        public double From(double src, TimeUnit srcType, int decimalPlaces = 2)
        {
            var scaled = 1 / (this.oneSecondScale / srcType.oneSecondScale);
            return Math.Round(src * scaled, decimalPlaces);
        }


        public static long MilliToNs(long millis) => millis * 1000 * 1000;


        public static double MilliToNs(double millis) =>millis * 1000 * 1000;


        public static long TickToNs(long ticks) => ticks * 100;


        public static double TickToNs(double ticks) => ticks * 100;

    }
}
