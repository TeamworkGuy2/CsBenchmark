
namespace CsBenchmark.Time
{
    public class TimeUnitFormatter
    {
        // TODO we do not yet use constructor locale information for abbreviations
        private readonly string format2DecimalPlaces;
        private readonly string format3DecimalPlaces;


        public TimeUnitFormatter()
        {
            this.format2DecimalPlaces = NewFormat(2);
            this.format3DecimalPlaces = NewFormat(3);
        }


        /// Get a time unit's English name abbreviation<br>
        /// Example: {@link TimeUnit#DAYS} is 'days'<br>
        /// or: {@link TimeUnit#NANOSECONDS} is 'nano', 'nanos', or 'ns'<br>
        /// The version of abbreviation returned depends on the arguments
        /// @param unit the time unit to abbreviate
        /// @param shortest if true 'millis' is shortened to 'ms', 'nanos' to 'ns', etc. If false, the longer abbreviation is returned
        /// @param plural if true 'secs' instead of 'sec' or 'millis' instead of 'millis'.  If false, the singular version is returned
        /// @return the abbreviation
        public string Abbreviation(TimeUnit unit, bool shortest, bool plural)
        {
            if (unit == TimeUnit.NANO)          return (shortest ? "ns" : (plural ? "nanos" : "nano"));
            else if (unit == TimeUnit.MICRO)    return (plural ? "microseconds" : "microsecond");
            else if (unit == TimeUnit.MILLI)    return (shortest ? "ms" : (plural ? "millis" : "milli"));
            else if (unit == TimeUnit.SEC)      return (plural ? "secs" : "sec");
            else                                throw TimeUnitUtil.ThrowUnknownUnit(unit, null);
            //case DAYS: return (plural ? "days" : "day");
            //case HOURS: return (plural ? "hrs" : "hr");
            //case MICROSECONDS: return (plural ? "micros" : "micro");
            //case MINUTES: return (plural ? "mins" : "min");
        }


        /// Convert a time measurement from one unit to another and then to a decimal number with a certain number of decimal places
        /// @param srcUnit the units of the {@code srcDuration} (i.e. milliseconds or minutes)
        /// @param srcDuration the time period to convert
        /// @param dstUnit the units to convert {@code srcDuration} to (i.e. hours or nanoseconds)
        /// @param decimalPlaces the number of decimal places to display the converted value as (note: currently optimized for 2 and 3 decimal places)
        /// @return a string representing the source time period in {@code dstUnit} units to n-{@code decimalPlaces} of accuracy
        public string Format(TimeUnit srcUnit, double srcDuration, TimeUnit dstUnit, int decimalPlaces)
        {
            double res = TimeUnitUtil.GetConversionMultiplier(srcUnit, dstUnit) * srcDuration;
            return decimalPlaces == 3 ? string.Format(this.format3DecimalPlaces, res) : (decimalPlaces == 2 ? string.Format(this.format2DecimalPlaces, res) : string.Format(NewFormat(decimalPlaces), res));
        }


        /// Convert a time unit and output it as a decimal to n-{@code decimalPlaces} with a units abbreviation (i.e. 'ms' or 'hours').<br>
        /// Equivalent to {@link #format(TimeUnit, double, TimeUnit, int) toString()} + " " + {@link #abbreviation(TimeUnit, boolean, boolean) abbreviation()}
        /// @see #format(TimeUnit, double, TimeUnit, int)
        /// @see #abbreviation(TimeUnit, boolean, boolean)
        public string Format(TimeUnit srcUnit, double srcDuration, TimeUnit dstUnit, int decimalPlaces, bool shortest)
        {
            return Format(srcUnit, srcDuration, dstUnit, decimalPlaces, shortest, false);
        }


        /// Convert a time unit and output it as a decimal to n-{@code decimalPlaces} with a units abbreviation (i.e. 'ms' or 'hours').<br>
        /// Equivalent to {@link #format(TimeUnit, double, TimeUnit, int) toString()} + " " + {@link #abbreviation(TimeUnit, boolean, boolean) abbreviation()}
        /// @see #format(TimeUnit, double, TimeUnit, int)
        /// @see #abbreviation(TimeUnit, boolean, boolean)
        public string Format(TimeUnit srcUnit, double srcDuration, TimeUnit dstUnit, int decimalPlaces, bool shortest, bool plural)
        {
            return Format(srcUnit, srcDuration, dstUnit, decimalPlaces) + " " + Abbreviation(dstUnit, shortest, plural);
        }


        private static string NewFormat(int decimalPlaces)
        {
            return "{0:0,0." + new string('0', decimalPlaces) + "}";
            //format.setRoundingMode(RoundingMode.HALF_UP);
            //format.setMinimumFractionDigits(0);
            //format.setMaximumFractionDigits(decimalPlaces);
        }

    }
}
