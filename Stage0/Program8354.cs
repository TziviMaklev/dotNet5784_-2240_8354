using System;

namespace Targil0
{
    partial class Program
    {
        private static void Main(string[] args)
        {
            Welcome8354();
            Welcome2240();
        }
        static partial void Welcome2240();

        private static void Welcome8354()
        {
            string name;
            Console.WriteLine("Enter your name: ");
            name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}