using System;
namespace Targil0
{
   partial class Program
    {
        static void Main(string[] args)
        {
            Welcome1514();
            Welcome0344();
            Console.ReadKey();
        }

        private static void Welcome1514()
        {
            Console.WriteLine("Enter your name");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", userName);
        }
        static partial void Welcome0344();
        
    }
}