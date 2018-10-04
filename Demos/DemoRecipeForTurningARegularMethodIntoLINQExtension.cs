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

    public static class StringExtensions
    {
        public static IEnumerable<int> Lenghts(this IEnumerable<string> incoming)
        {
            return incoming.Select(s => s.Length);
        }
    }


    public static class MethodIsInAStaticClass
    {
        public static string GetLongest(this IEnumerable<string> strings)
        {
            return strings.OrderByDescending(s => s.Length).First();
        }
    }
}
