using System;
using Demos;
using Demos.AnotherNamespace;

namespace Demos
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = string.Empty;            
            
            Console.WriteLine("Hello World! " + s.ThisIsAnExtensionMethodToString());
            Console.ReadLine();
        }
    }
}
