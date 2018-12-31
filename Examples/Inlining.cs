using System;
using System.Runtime.CompilerServices;

namespace CsBenchmark.Examples
{
    using CsBenchmark.Measure;
    using CsBenchmark.Inputs;

    public class Inlining
    {
        static Random rnd = new Random();


        static int RunInline(int v)
        {
            v += 3;
            v = (v % 5 == 0 ? v - 4 : v - 1);
            v++;
            v++;
            if (v - 1 > 0)
                v = rnd.NextDouble() > 0.5 ? v : -v;
            else
                v = rnd.NextDouble() < 0.5 ? v : -v;
            v++;

            var vf = (float?)v;
            // Helper
            var vr = vf.HasValue ? ((int)Math.Round(vf.Value * 25)).ToString().PadLeft(8, '0') : "00000000";

            var e = int.Parse(vr.TrimStart(new char[] { '0' }));
            // ExtraMath
            if (e % 3 == 0)
                e = e / 3;
            else if (e % 5 == 0)
                e = e / 5;
            else
                e = (e & 1) == 1 ? e : e / 2;

            return (int)Math.Round((float)e);
        }


        static int RunWithHelper(int v)
        {
            v += 3;
            v = (v % 5 == 0 ? v - 4 : v - 1);
            v++;
            v++;
            if (v - 1 > 0)
                v = rnd.NextDouble() > 0.5 ? v : -v;
            else
                v = rnd.NextDouble() < 0.5 ? v : -v;
            v++;

            var vr = Helper(v);
            return ExtraMath(int.Parse(vr.TrimStart(new char[] { '0' })));
        }

        static string Helper(float? v)
        {
            return v.HasValue ? ((int)Math.Round(v.Value * 25)).ToString().PadLeft(8, '0') : "00000000";
        }

        static int ExtraMath(int e)
        {
            // ExtraMath
            if (e % 3 == 0)
                e = e / 3;
            else if (e % 5 == 0)
                e = e / 5;
            else
                e = (e & 1) == 1 ? e : e / 2;

            return (int)Math.Round((float)e);
        }


        static int RunWithHelperInlineHint(int v)
        {
            v += 3;
            v = (v % 5 == 0 ? v - 4 : v - 1);
            v++;
            v++;
            if (v - 1 > 0)
                v = rnd.NextDouble() > 0.5 ? v : -v;
            else
                v = rnd.NextDouble() < 0.5 ? v : -v;
            v++;

            var vr = HelperInlineHint(v);
            return ExtraMathInlineHint(int.Parse(vr.TrimStart(new char[] { '0' })));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static string HelperInlineHint(float? v)
        {
            return v.HasValue ? ((int)Math.Round(v.Value * 25)).ToString().PadLeft(8, '0') : "00000000";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int ExtraMathInlineHint(int e)
        {
            // ExtraMath
            if (e % 3 == 0)
                e = e / 3;
            else if (e % 5 == 0)
                e = e / 5;
            else
                e = (e & 1) == 1 ? e : e / 2;

            return (int)Math.Round((float)e);
        }


        public static PerformanceStepLog Measure(NestedLoop args, ProgressTracker progressCb = null)
        {
            int loops = args.Loop1;
            int innerLoops = args.Loop2;
            var res = new PerformanceStepLog("method inlining");
            var resInln = new PerformanceLoopLog("inline", innerLoops + " method calls per loop");
            var resHlpr = new PerformanceLoopLog("helper", innerLoops + " method calls per loop");
            var resHlprInln = new PerformanceLoopLog("helper-inline-annotation", innerLoops + " method calls per loop");
            int temp1 = 0;
            int temp2 = 0;
            int temp3 = 0;

            if (progressCb != null) { progressCb.ResetTotalSteps(loops); }

            for (int i = 0; i < loops; i++)
            {
                RunInline(i);
                RunWithHelper(i);
                RunWithHelperInlineHint(i);

                temp1 += TaskRunner.RunTimedLoop(innerLoops, RunInline, resInln);

                temp2 += TaskRunner.RunTimedLoop(innerLoops, RunWithHelper, resHlpr);

                temp3 += TaskRunner.RunTimedLoop(innerLoops, RunWithHelperInlineHint, resHlprInln);

                if (progressCb != null) { progressCb.Step(); }
            }

            res.Steps.Add(resInln);
            res.Steps.Add(resHlpr);
            res.Steps.Add(resHlprInln);

            res.TempResults.Add(temp1);
            res.TempResults.Add(temp2);
            res.TempResults.Add(temp3);

            return res;
        }

    }
}
