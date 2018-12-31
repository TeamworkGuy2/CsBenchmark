using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsBenchmark.Extensions;
using CsBenchmark.StringEscape;
using CsBenchmark.Time;

namespace CsBenchmark.Results
{
    public class ResultSets
    {
        private string indent = "   ";

        public readonly string Description;
        public readonly IList<Tuple<StepDescriptor, StepResultSet>> Results;


        public ResultSets(string description, IList<Tuple<StepDescriptor, StepResultSet>> results)
        {
            this.Description = description;
            this.Results = results.ToArray().ToList();
        }


        public override string ToString()
        {
            return ToString(null);
        }


        public void ToJson(StringBuilder dst, ResultOutputOptions st)
        {
            bool inline = false;

            dst.Append("{\n");
            dst.Append("\"description\": \"").Append(StringEscapeJson.ToJsonString(Description)).Append("\"");
            dst.Append(inline ? ", " : ",\n");
            dst.Append(indent);
            dst.Append("\"steps\": [{\n");
            for (int i = 0, size = Results.Count; i < size; i++)
            {
                ToJson(Results[i].Item1, Results[i].Item2, indent, inline, st, dst);
                dst.Append(indent + "}, {\n");
            }
            dst.Append("\n" + indent + "}]");
            dst.Append("\n}");
        }


        public string ToString(TimeUnit displayTimeUnit)
        {
            int groupCount = Results.Count;
            var sb = new StringBuilder();
            var groupIndent = "";
            var stepsIndent = indent;
            var stepIndent = indent + indent;

            sb.Append("benchmark: ");
            sb.Append(Description);

            if (groupCount > 1)
            {
                groupIndent = indent;
                stepsIndent = indent + indent;
                stepIndent = indent + indent + indent;
            }

            for (int i = 0; i < groupCount; i++)
            {
                var groupDesc = Results[i].Item1.Get();
                if (groupCount > 1)
                {
                    sb.Append("\n" + groupIndent + "group " + i + "/" + groupCount + (!"null".Equals(groupDesc) ? (" - " + groupDesc) : ""));
                }

                var steps = Results[i].Item2;
                int stepCount = steps.Count;

                if (stepCount > 1)
                {
                    sb.Append("\n" + stepsIndent + "steps:\n");
                }

                for (int j = 0; j < stepCount; j++)
                {
                    sb.Append(stepCount > 1 ? stepIndent : indent);

                    ToString(steps[j].Item1, steps[j].Item2, displayTimeUnit, sb);
                    sb.Append("\n");
                }
            }
            return sb.ToString();
        }


        public void ToJson(StepDescriptor description, StepResultSet stepResults, string indentation, bool inline, ResultOutputOptions st, StringBuilder dst)
        {
            dst.Append(indentation).Append("\"description\": \"").Append(StringEscapeJson.ToJsonString(description.Get())).Append("\"")
                .Append(inline ? ", " : ",\n")
                .Append(indentation).Append("\"runTimesNano\": [")
                .Append(stepResults.Select(i => i.Item2.Sum().ToString() + ", ").ToStringList())
                .Append(']')
                .Append(inline ? "]" : "]\n");
        }



        public void ToString(StepDescriptor description, List<long> timeNanos, TimeUnit displayTimeUnit, StringBuilder dst)
        {
            long time = timeNanos.Sum();
            dst.Append(TimeUtil.ConvertNsWithAbbreviation(time, displayTimeUnit));
            dst.Append(indent);
            dst.Append(description.Get());
        }


        public static ResultSetsBuilder builder(string description)
        {
            return new ResultSetsBuilder(description);
        }

    }
}
