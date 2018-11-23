using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demos
{
    public class MethodIsInNonStaticClass
    {
        public IEnumerable<int> Lengths(IEnumerable<string> incoming)
        {
            return incoming.Select(s => s.Length);
        }
    }
    
    public static class MethodIsInAStaticClass
    {
        public static T LongestBy<T>(IEnumerable<T> collection, Func<T, object> length)
        {
            return collection.OrderByDescending(length).First();
        }
    }
}
