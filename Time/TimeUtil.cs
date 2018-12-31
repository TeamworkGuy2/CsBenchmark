
namespace CsBenchmark.Time
{
    public static class TimeUtil
    {
        //public static readonly DecimalFormat intFormat = new DecimalFormat("#,###");

        public static string ConvertNsWithAbbreviation(double durationNanos, TimeUnit dstUnit)
        {
            return ConvertWithAbbreviation(TimeUnit.NANO, durationNanos, dstUnit != null ? dstUnit : TimeUnit.NANO);
        }


        public static string ConvertWithAbbreviation(TimeUnit srcUnit, double srcDuration, TimeUnit dstUnit)
        {
            return TimeUnitUtil.Format(srcUnit, srcDuration, dstUnit != null ? dstUnit : TimeUnit.NANO, 3, true, true);
        }

    }
}
