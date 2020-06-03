using System;

using simplemlir;

namespace simplemlir.sample
{
    class Program
    {
        static void Main(string[] args)
        {
            string contents = TileGen.ReadSource("kernel.cs.in");
            Console.WriteLine($"The contents are {contents}.");
            string nsName = TileGen.KernelName(contents);
            Console.WriteLine($"The name of this namespace is {nsName}.");
            Console.WriteLine("Done!");
        }
    }
}
