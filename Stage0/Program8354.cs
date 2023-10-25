using System;

namespace Targil0
{
    partial class Program
    {
        private static void Main(string[] args)
        {
            Wellcome8354();
            Wellcome2240();
        }

        private static void Wellcome8354()
        {
            string name;
            Console.WriteLine("Enter your name: ");
            name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }

        static partial void Wellcome2240();
    }
}