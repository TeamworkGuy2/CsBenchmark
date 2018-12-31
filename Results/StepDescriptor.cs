using System;
using System.Text;

namespace CsBenchmark.Results
{
    public class StepDescriptor
    {
        private readonly Func<string> toStringSupplier;
        private readonly Func<object, string> toStringFunc;
        private readonly object args;


        public StepDescriptor(Func<string> toStringSupplier, Func<object, string> toStringFunc, object args)
        {
            this.toStringSupplier = toStringSupplier;
            this.toStringFunc = toStringFunc;
            this.args = args;
        }


        public string Get()
        {
            return toStringSupplier();
        }


        public bool IsFunc()
        {
            return toStringFunc != null;
        }


        public bool HasParams()
        {
            return args != null;
        }


        public object GetParams()
        {
            return args;
        }


        public static StepDescriptor Of(String description)
        {
            return new StepDescriptor(() => description, null, null);
        }


        public static StepDescriptor Of<P>(P args, Func<P, String> toStringFunc) where P : class
        {
            return new StepDescriptor(() => toStringFunc(args), (obj) => toStringFunc(obj as P), args);
        }


        public static StepDescriptor Of<P>(Func<string> toStringSupplier) where P : class
        {
            return new StepDescriptor(toStringSupplier, null, null);
        }


        public static StepDescriptor Of(int loops, string description)
        {
            return new StepDescriptor(() => "(" + string.Format("{0:0,0}", loops) + " loops)" + (description != null ? (" - " + description) : ""), null, null);
        }


        public static StepDescriptor Of(string description, params int[] actionCounts)
        {
            var sb = new StringBuilder(actionCounts.Length * 5);
            for (int i = 0, size = actionCounts.Length; i < size; i++)
            {
                sb.Append(string.Format("{0:0,0}", actionCounts[i]) + (i < size - 1 ? "*" : ""));
            }

            return new StepDescriptor(() => "(" + sb.ToString() + " actions)" + (description != null ? (" - " + description) : ""), null, null);
        }

    }
}
