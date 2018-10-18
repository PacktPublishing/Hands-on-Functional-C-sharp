using System;

namespace Demos
{
    public class DemoPureFunction
    {
        public static int Add(int x, int y)
        {
            return x + y;
        }

        // some really interesting properies:
        // 1.Carry all their dependencies as parameters
        // 2.Do not depend on or mutate outside state


        // However many times get called - the result is boringly unsurprising
        // However shared state is mutated - the result is boringly unsurprising 
        // Caching the result is ok - since it will still be the same 
        // One function at a time - makes our code easy to think about


        // example of the pure counterpart - side-effect function
        public static int AddAndLog(int x, int y)
        {
            Console.WriteLine($"Adding {x} and {y}");
            var result = x + y;
            Console.WriteLine($"Result is {result}");
            return result;
        }
    }
}
