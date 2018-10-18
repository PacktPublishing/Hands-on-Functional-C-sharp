using System;
using System.Linq;
using static Demos.DemoPureFunction;

namespace Demos
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = Add(1, 2);
            var y = Add(1, 2);
            var z = Add(1, 2);

            Console.WriteLine($"x = {x} y = {y} z = {z}");
            Console.WriteLine($"x == y == z is {x == y && y == z && x == z}");


            var a = AddAndLog(1, 2);
            Console.WriteLine($"x = {x} a = {a}");
            Console.WriteLine($"a == y is {a == x}");

            Console.ReadLine();
        }
    }
}
