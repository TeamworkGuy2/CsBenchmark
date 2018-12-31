using System.Collections.Generic;
using System.Linq;
using CsBenchmark.Time;

namespace CsBenchmark.Measure
{
    public class PerformanceLoopLog : PerformanceLog
    {
        public int LoopCount;
        public List<long> ElapsedNanoSteps;
        public long? ElapsedNanoAltTotal;


        public PerformanceLoopLog(string name, string description = null) : base(name, description)
        {
            this.ElapsedNanoSteps = null;
        }


        public PerformanceLoopLog(string name, string description, long elapsedNanos, int loopCount) : base(name, description)
        {
            this.LoopCount = loopCount;
            this.ElapsedNanoAltTotal = elapsedNanos;
            this.ElapsedNanoSteps = null;
        }


        public void AddLoopTime(long elapsedNanos)
        {
            this.LoopCount++;
            if (this.ElapsedNanoSteps == null)
            {
                this.ElapsedNanoSteps = new List<long>();
            }
            this.ElapsedNanoSteps.Add(elapsedNanos);
        }


        public override long GetElapsedNanos()
        {
            return (this.ElapsedNanoSteps != null && this.ElapsedNanoSteps.Count > 0 ? this.ElapsedNanoSteps.Sum() : (this.ElapsedNanoAltTotal ?? 0));
        }


        public override string ToString(TimeUnit timeUnit, int decimalPlaces)
        {
            return base.Name + "\t" + timeUnit.From(this.GetElapsedNanos(), TimeUnit.NANO, decimalPlaces) + " " + timeUnit.ShortName +
                " " + this.LoopCount + " loops" +
                (base.Description != null ? " - " + base.Description : "");
        }

    }
}
