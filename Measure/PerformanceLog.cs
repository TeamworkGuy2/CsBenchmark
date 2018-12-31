using CsBenchmark.Time;

namespace CsBenchmark.Measure
{
    public abstract class PerformanceLog
    {
        public string Name;
        public string Description;


        public PerformanceLog(string name, string description = null)
        {
            this.Name = name;
            this.Description = description;
        }


        public override string ToString()
        {
            return this.ToString(TimeUnit.SEC, 3);
        }


        public abstract long GetElapsedNanos();

        public abstract string ToString(TimeUnit timeUnit, int decimalPlaces);
    }
}
