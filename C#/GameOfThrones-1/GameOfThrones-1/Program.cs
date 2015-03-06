using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThrones_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var passord = Console.ReadLine() ?? "";

            var output = (passord.Distinct().Select(s => new { count = passord.Count(a => a == s), letter = s }).Count(w => w.count % 2 == 1) <= 1) ? "YES" : "NO";

            Console.WriteLine(output);
            Console.ReadLine();

        }
    }
}
