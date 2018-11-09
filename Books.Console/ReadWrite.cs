using System;

namespace Books.ConsoleApp
{
    public class ReadWrite : IReadWrite
    {
        public string Read()
        {
            return Console.ReadLine();
        }

        public void Write(string output)
        {
            Console.WriteLine(output);
        }
    }
}
