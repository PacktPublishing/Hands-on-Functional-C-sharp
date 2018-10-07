using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demos
{
    public class DemoPureFunction
    {
        public static int Add(int x, int y)
        {
            return x + y;
        }

        // some real interesting properies:
        // 1.Carry all their dependencies as parameters
        // 2.Do not reach or mutate outside state


        // However many times get called - the result is boringly unsurprising
        // However shared state is mutated - the result is boringly unsurprising 
        // Caching the result is ok - since it will still be the same 
        // One function at a time - makes our code easy to think about
    }
}
