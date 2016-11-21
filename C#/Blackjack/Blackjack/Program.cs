using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            var playing = true;
            while (playing)
            {
                Logic.WriteBreak();
                Logic.PlayGame();
                Logic.WriteBreak();
                Console.WriteLine("Play again? Y or N?");
                var again = Console.ReadLine().ToLower();
                if (again == "n")
                {
                    playing = false;
                }
            }
            Console.WriteLine("Goodbye");
            // End 
            Console.ReadLine();

        }
    }
}
