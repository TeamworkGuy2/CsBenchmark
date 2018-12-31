using System;
using CsBenchmark.Inputs;
using CsBenchmark.Measure;
using CsBenchmark.Results;
using CsBenchmark.Time;

namespace CsBenchmark.Examples
{
    class CsBenchmarkMain
    {

        private static void formatDecimalTest()
        {
            var az = new double[] {
                1,
                22,
                333,
                4444,
                55555,
                666666,
                7777777,
                88888888,
                999999999,
                9999999990,
                1.1,
                22.22,
                333.333,
                4444.4444,
                55555.55555,
                666666.666666,
                7777777.7777777,
                88888888.88888888,
                999999999.999999999,
                9999999990.0999999999,
            };
            var i = 0;
            foreach (var a in az)
            {
                Console.WriteLine(i + ". " + string.Format("{0:0,0.00}", a));
                i++;
            }
        }


        private static void Print(ResultSets res)
        {
            Console.WriteLine(res.ToString(TimeUnit.MILLI));
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Performance Test 1...");
            Inlining.Measure(NestedLoop.Of(500, 500), new ProgressTracker(0.05, (p) => Console.WriteLine(p * 100 + "%")));
            var res = Inlining.Measure(NestedLoop.Of(2000, 2000), new ProgressTracker(0.05, (p) => Console.WriteLine(p * 100 + "%")));
            Console.WriteLine(res.ToString(TimeUnit.MILLI, 3));
            Console.WriteLine(string.Join(", ", res.TempResults));

            Console.WriteLine("Performance Test 2...");
            new ArrayAccess().ReadArrayElements(NestedLoop.Of(500, 500));
            Print(new ArrayAccess().ReadArrayElements(NestedLoop.Of(2000, 2000)));

            Console.Read();
        }

    }
}
