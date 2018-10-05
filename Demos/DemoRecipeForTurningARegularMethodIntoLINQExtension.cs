using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demos
{
    public class MethodIsInNonStaticClass
    {
        public IEnumerable<int> Lenghts(IEnumerable<string> incoming)
        {
            return incoming.Select(s => s.Length);
        }
    }

    public static class StringCollectionExtensions
    {
        public static IEnumerable<int> Lenghts(this IEnumerable<string> incoming)
        {
            return incoming.Select(s => s.Length);
        }
    }


    public static class MethodIsInAStaticClass
    {
        public static T LongestBy<T>(this IEnumerable<T> collection, Func<T, object> lenght)
        {
            return collection.OrderByDescending(lenght).First();
        }
    }
}
