using System;
using System.Diagnostics;
using CsBenchmark.Extensions;
using CsBenchmark.Inputs;
using CsBenchmark.Results;

namespace CsBenchmark.Examples
{
    class ArrayAccess
    {
        private int[] array;
        private int[] randIndexes;
        private int loops;
        private int subLoops;


        public ResultSets ReadArrayElements(NestedLoop args)
        {
            int tests = 500;
            var timer = new Stopwatch();
            Random rand = new Random();

            // Initialize arrays
            loops = args.Loop1;
            subLoops = args.Loop2;

            array = new int[subLoops];
            for (int i = 0; i < subLoops; i++)
            {
                array[i] = (int)(rand.NextDouble() * 2);
            }

            randIndexes = new int[loops];
            for (int i = 0; i < loops; i++)
            {
                randIndexes[i] = (int)(rand.NextDouble() * loops);
            }

            long baseTotal = 0;
            long baseRandomTotal = 0;
            long randomTotal = 0;
            long orderedTotal = 0;

            for (int a = 0; a < tests; a++)
            {
                timer.Restart();
                BaseIncrement(loops, subLoops);
                baseTotal += timer.StopNanos();

                timer.Restart();
                BaseRandom(loops, subLoops);
                baseRandomTotal += timer.StopNanos();

                timer.Restart();
                ArrayOrdered(loops, subLoops);
                orderedTotal += timer.StopNanos();

                timer.Restart();
                ArrayRandom(loops, subLoops);
                randomTotal += timer.StopNanos();
            }

            return ResultSets.builder("Array lookups by type")
                .StartGroup((string)null)
                    .AddStepResult(StepDescriptor.Of("base (loop>loop>var++)", tests, loops, subLoops), baseTotal)
                    .AddStepResult(StepDescriptor.Of("base random (loop>array[i]>loop>var++)", tests, loops, subLoops), baseRandomTotal)
                    .AddStepResult(StepDescriptor.Of("random (loop>array[i]>loop>array[i])", tests, loops, subLoops), randomTotal)
                    .AddStepResult(StepDescriptor.Of("ordered (loop>loop>array[i])", tests, loops, subLoops), orderedTotal)
                .EndGroup()
                .Build();
        }


        public int BaseIncrement(NestedLoop args)
        {
            return BaseIncrement(args.Loop1, args.Loop2);
        }

        /// Base loop same as other loops without array lookup
        /// @param loops number of main loops
        /// @param subLoops number of sub loops to run
        /// @return a temp loop variable to prevent compiler optimization
        public int BaseIncrement(int loops, int subLoops)
        {
            int temp = 0;
            for (int i1 = 0; i1 < loops; i1++)
            {
                for (int i2 = 0; i2 < subLoops; i2++)
                {
                    temp++;
                }
            }
            return temp;
        }


        public int BaseRandom(NestedLoop args)
        {
            return BaseRandom(args.Loop1, args.Loop2);
        }

        /// <summary>Base loop and Math.random() calls same as other loops without array lookup</summary>
        /// @return some random value after looping through the array to ensure that the VM JIT does not simplify this method call
        public int BaseRandom(int loops, int subLoops)
        {
            int r = 0;
            int temp = 0;
            for (int i1 = 0; i1 < loops; i1++)
            {
                r = randIndexes[i1];
                for (int i2 = 0; i2 < subLoops; i2++)
                {
                    temp++;
                }
            }
            return r + temp;
        }


        public int ArrayRandom(NestedLoop args)
        {
            return ArrayRandom(args.Loop1, args.Loop2);
        }

        /// @return some random value after looping through the array to ensure that the VM JIT does not simplify this method call
        public int ArrayRandom(int loops, int subLoops)
        {
            int r = 0;
            int temp = 0;
            for (int i1 = 0; i1 < loops; i1++)
            {
                r = randIndexes[i1];
                for (int i2 = 0; i2 < subLoops; i2++)
                {
                    temp = array[r];
                }
            }
            return temp;
        }


        public int ArrayOrdered(NestedLoop args)
        {
            return ArrayOrdered(args.Loop1, args.Loop2);
        }

        /// @return some random value after looping through the array to ensure that the VM JIT does not simplify this method call
        public int ArrayOrdered(int loops, int subLoops)
        {
            int temp = 0;
            for (int i1 = 0; i1 < loops; i1++)
            {
                for (int i2 = 0; i2 < subLoops; i2++)
                {
                    temp = array[i2];
                }
            }
            return temp;
        }

    }
}
