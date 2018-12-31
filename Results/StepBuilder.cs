using System;
using System.Collections.Generic;

namespace CsBenchmark.Results
{
    public class StepBuilder
    {
        private readonly ResultSetsBuilder Parent;
        internal readonly StepDescriptor Descriptor;
        internal readonly StepResultSet Steps = new StepResultSet();


        public StepBuilder(ResultSetsBuilder parent, StepDescriptor descriptor)
        {
            this.Parent = parent;
            this.Descriptor = descriptor;
        }


        public StepBuilder AddStepResult(StepDescriptor stepDescriptor, long totalNanos)
        {
            return AddStepResult(stepDescriptor, totalNanos, (object)null);
        }


        public StepBuilder AddStepResult<T>(StepDescriptor stepDescriptor, long totalNanos, T res)
        {
            this.Steps.Add(Tuple.Create(stepDescriptor, new List<long> { totalNanos }));
            return this;
        }


        public StepBuilder AddStepResult(StepDescriptor stepDescriptor, List<long> runtimesNanos)
        {
            return AddStepResult(stepDescriptor, runtimesNanos, (object)null);
        }


        public StepBuilder AddStepResult<T>(StepDescriptor stepDescriptor, List<long> runtimesNanos, T res)
        {
            this.Steps.Add(Tuple.Create(stepDescriptor, runtimesNanos));
            return this;
        }


        public ResultSetsBuilder EndGroup()
        {
            return Parent.EndGroup(this);
        }

    }
}
