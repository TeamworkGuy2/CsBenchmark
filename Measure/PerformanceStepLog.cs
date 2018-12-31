using System;
using System.Collections.Generic;
using System.Linq;
using CsBenchmark.Time;

namespace CsBenchmark.Measure
{
    public class PerformanceStepLog : PerformanceLog
    {
        public IList<PerformanceLog> Steps;
        public long? ElapsedNanos;
        /// <summary>Used to store benchmark results so actions can't be removed by the compiler</summary>
        public IList<object> TempResults = new List<object>();


        public PerformanceStepLog(string name, string description = null, long? elapsedNanos = null) : base(name, description)
        {
            this.Steps = new List<PerformanceLog>();
            this.ElapsedNanos = elapsedNanos;
        }


        public override long GetElapsedNanos()
        {
            return (this.ElapsedNanos.HasValue ? this.ElapsedNanos.Value : this.Steps.Sum(s => s.GetElapsedNanos()));
        }


        public override string ToString(TimeUnit timeUnit, int decimalPlaces)
        {
            return base.Name + "\t" + timeUnit.From(this.GetElapsedNanos(), TimeUnit.NANO, decimalPlaces) + " " + timeUnit.ShortName +
                (base.Description != null ? " - " + base.Description : "") +
                (this.Steps.Count > 0 ? "\n\t" + String.Join("\n\t", this.Steps.Select(s => s.ToString(timeUnit, decimalPlaces))) + "\n" : "");
        }

    }
}
