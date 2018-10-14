using System;
using System.Linq;
using Books.ConsoleApp;
using Demos;

namespace Demos
{
    class Program
    {
        private readonly Pure pure = new Pure();
        private readonly PureIsh pureIsh = new PureIsh(
            () => new[] { new Book { title = "this is the one" } }
        );

        static void Main(string[] args)
        {
            Console.WriteLine("demo");
            Console.ReadLine();
        }
    }
}
