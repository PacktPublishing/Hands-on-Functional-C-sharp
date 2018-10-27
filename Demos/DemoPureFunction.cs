using System;

namespace Demos
{
    public class DemoPureFunction
    {
        public static int Add(int x, int y)
        {
            return x + y;
        }

        public static int AddAndLog(int x, int y)
        {
            Console.WriteLine($"Adding {x} and {y}");
            var result = x + y;
            Console.WriteLine($"Result is {result}");
            return result;
        }
    }
}
