using System;

namespace CsBenchmark.Time
{
    public static class TimeUnitUtil
    {
        static readonly TimeUnitFormatter defaultFormatter = new TimeUnitFormatter();

        static double C1 = 1;
        static double C2 = C1 * 1000;
        static double C3 = C2 * 1000;
        static double C4 = C3 * 1000;
        static double C5 = C4 * 60;
        static double C6 = C5 * 60;
        static double C7 = C6 * 24;

        static double[] nsPer = { C1, C2, C3, C4, C5, C6, C7 };

        static double[] microPer = { C1 / C2, C2 / C2, C3 / C2, C4 / C2, C5 / C2, C6 / C2, C7 / C2 };

        static double[] milliPer = { C1 / C3, C2 / C3, C3 / C3, C4 / C3, C5 / C3, C6 / C3, C7 / C3 };

        static double[] secPer = { C1 / C4, C2 / C4, C3 / C4, C4 / C4, C5 / C4, C6 / C4, C7 / C4 };

        static double[] minPer = { C1 / C5, C2 / C5, C3 / C5, C4 / C5, C5 / C5, C6 / C5, C7 / C5 };

        static double[] hrPer = { C1 / C6, C2 / C6, C3 / C6, C4 / C6, C5 / C6, C6 / C6, C7 / C6 };

        static double[] dayPer = { C1 / C7, C2 / C7, C3 / C7, C4 / C7, C5 / C7, C6 / C7, C7 / C7 };


        public static double[][] APerB = {
            nsPer,
            microPer,
            milliPer,
            secPer,
            minPer,
            hrPer,
            dayPer
        };


        public static double GetConversionMultiplier(TimeUnit srcUnit, TimeUnit dstUnit)
        {
            return APerB[dstUnit.Ordinal][srcUnit.Ordinal];
        }


        /// Convert a time measurement from one unit to another.<br>
        /// Example: {@code convert(TimeUnit.HOURS, 2.33, TimeUnit.MINUTES)}<br>
        /// returns: {@code 153.0}
        /// @param srcUnit the units of the {@code srcDuration} (i.e. milliseconds or minutes)
        /// @param srcDuration the time period to convert
        /// @param dstUnit the units to convert {@code srcDuration} to (i.e. hours or nanoseconds)
        /// @return a decimal number represented the converted units as accurately as possible
        public static double Convert(TimeUnit srcUnit, double srcDuration, TimeUnit dstUnit)
        {
            return GetConversionMultiplier(srcUnit, dstUnit) * srcDuration;
        }


        // ==== mirror TimeUnitFormatter's API ====
        /// @see TimeUnitFormatter#abbreviation(TimeUnit, boolean, boolean)
        public static string Abbreviation(TimeUnit unit, bool shortest, bool plural)
        {
            return defaultFormatter.Abbreviation(unit, shortest, plural);
        }


        /// @see TimeUnitFormatter#format(TimeUnit, double, TimeUnit, int)
        public static string Format(TimeUnit srcUnit, double srcDuration, TimeUnit dstUnit, int decimalPlaces)
        {
            return defaultFormatter.Format(srcUnit, srcDuration, dstUnit, decimalPlaces);
        }


        /// @see TimeUnitFormatter#format(TimeUnit, double, TimeUnit, int, boolean)
        public static string Format(TimeUnit srcUnit, double srcDuration, TimeUnit dstUnit, int decimalPlaces, bool shortest)
        {
            return defaultFormatter.Format(srcUnit, srcDuration, dstUnit, decimalPlaces, shortest);
        }


        /// @see TimeUnitFormatter#format(TimeUnit, double, TimeUnit, int, boolean, boolean)
        public static string Format(TimeUnit srcUnit, double srcDuration, TimeUnit dstUnit, int decimalPlaces, bool shortest, bool plural)
        {
            return defaultFormatter.Format(srcUnit, srcDuration, dstUnit, decimalPlaces, shortest, plural);
        }


        public static Exception ThrowUnknownUnit(TimeUnit unit, string name)
        {
            return new ArgumentException("unknown " + (name != null && name.Length > 0 ? name + " " : "") + "time unit '" + unit + "'");
        }

    }
}
