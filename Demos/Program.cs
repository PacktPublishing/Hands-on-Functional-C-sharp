﻿using System;
using System.Linq;
using Demos;

namespace Demos
{
    class Program
    {
        static void Main(string[] args)
        {
            var titles = new string[] { "The three body problem", "The dark forest", "Death's end" };

            // need a new instance first and call its method then
            var instance = new MethodIsInNonStaticClass();
            var lenghts = instance.Lenghts(titles);

            // now
            lenghts = titles.Lenghts();

            // need to name the full name of class and then the name of method
            var longest = MethodIsInAStaticClass.GetLongest(titles);

            // now
            longest = titles.GetLongest();

            // somewhat non-trivial use of LINQ
            var stringsAndLengths = titles
                // zip-up together the titles and their lengths
                .Zip(lenghts, (t, len) => $"{t}[{len}]")
                // aggregate (or reduce) to a single string
                .Aggregate((prev, next) => string.Concat(prev, Environment.NewLine, next));

            Console.WriteLine(stringsAndLengths);
            Console.WriteLine("And the longest is " + longest);
            Console.ReadLine();
        }
    }
}
