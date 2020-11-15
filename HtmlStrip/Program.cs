using System;
namespace HtmlStrip
{
    class Program
    {   
        private static void Main(string[] args)
        {
            Interface.LoadUI();
            Console.Write("Press enter to stop");
            string input = Console.ReadLine();
        }

    }
}
