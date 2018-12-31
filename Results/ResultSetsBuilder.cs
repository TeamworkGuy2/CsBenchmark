using System;
using System.Collections.Generic;

namespace CsBenchmark.Results
{
    public class ResultSetsBuilder
    {
        public string Description { get; set; }
        public IList<Tuple<StepDescriptor, StepResultSet>> Groups = new List<Tuple<StepDescriptor, StepResultSet>>();


        public ResultSetsBuilder(string description)
        {
            this.Description = description;
        }


        public StepBuilder StartGroup(string description)
        {
            return StartGroup(StepDescriptor.Of(description));
        }


        public StepBuilder StartGroup(StepDescriptor descriptor)
        {
            return new StepBuilder(this, descriptor);
        }


        public ResultSetsBuilder EndGroup(StepBuilder stepBldr)
        {
            this.Groups.Add(Tuple.Create(stepBldr.Descriptor, stepBldr.Steps));
            return this;
        }


        public ResultSets Build()
        {
            return new ResultSets(this.Description, this.Groups);
        }

    }
}
