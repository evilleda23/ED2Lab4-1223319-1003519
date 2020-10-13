using Compressors;
using System;

namespace TestLZW
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese el texto para codificar");
            string text = Console.ReadLine();
            var lzw = new LZWCompressor("..//..//..");
            Console.WriteLine("El texto comprimido es:");
            Console.WriteLine(lzw.ShowCompress(text));
            Console.ReadLine();
        }
    }
}
