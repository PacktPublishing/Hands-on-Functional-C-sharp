using System;
using System.Linq;
using Demos;

namespace Demos
{
    class Program
    {
        static void Main(string[] args)
        {
            var titles = new string[] { "The Three-Body Problem", "The Dark forest", "Death's End" };

            // need a new instance first and call its method then
            var instance = new MethodIsInNonStaticClass();
            var lenghts = instance.Lenghts(titles);

            // need to name the full name of class and then the name of method
            var longest = MethodIsInAStaticClass.LognestBy(titles, t => t.Length);

            // somewhat non-trivial use of LINQ
            var stringsAndLengths = titles
                // zip-up together the titles and their lengths
                .Zip(titles.Lenghts(), (t, len) => $"{t}[{len}]")
                // aggregate (or reduce) to a single string
                .Aggregate((prev, next) => string.Concat(prev, Environment.NewLine, next));

            Console.WriteLine(stringsAndLengths);
            Console.WriteLine("And the longest is " + titles.LognestBy(t => t.Lenght));
            Console.ReadLine();
        }
    }
}
