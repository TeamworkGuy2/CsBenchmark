using System;
using System.Collections.Generic;
using System.Linq;

namespace CsBenchmark.Results
{
    public class StepResultSet : List<Tuple<StepDescriptor, List<long>>>
    {

        public StepResultSet() : base()
        {
        }


        public StepResultSet(StepDescriptor descriptor, long resultTimeNanos) : base()
        {
            base.Add(Tuple.Create(descriptor, new List<long> { resultTimeNanos }));
        }


        public StepResultSet(List<Tuple<StepDescriptor, List<long>>> stepResults) : base(stepResults.ToArray().ToList())
        {
        }

    }
}
