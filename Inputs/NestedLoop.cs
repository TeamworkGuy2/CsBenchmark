namespace CsBenchmark.Inputs
{
    public class NestedLoop
    {
        public int Loop1 { get; private set; }
        public int Loop2 { get; private set; }


        NestedLoop(int loop1, int loop2)
        {
            this.Loop1 = loop1;
            this.Loop2 = loop2;
        }


        public static NestedLoop Of(int loop1, int loop2)
        {
            return new NestedLoop(loop1, loop2);
        }

    }
}
