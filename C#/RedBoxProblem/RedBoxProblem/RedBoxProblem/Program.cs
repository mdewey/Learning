using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace RedBoxProblem
{
    class Program
    {
        private const int totalSpots = 100;
        public static int Calc(List<int> requestedSpots)
        {
            var rv = 0;
            for (var i = 1; i < requestedSpots.Count(); i++)
            {
                var directRoute = Math.Abs(requestedSpots[i] - requestedSpots[i - 1]);
                var roundAbout = totalSpots - Math.Abs(requestedSpots[i - 1] - requestedSpots[i]);
                rv += ((directRoute < roundAbout) ? directRoute : roundAbout);
            }
            return rv;
        }


        public static int Calc_R(List<int> requestedSpots)
        {
            var i = 1;
            if (requestedSpots.Count() <= 1) return 0;
            var ticks = Math.Abs(requestedSpots[i] - requestedSpots[i - 1]);
            var directRoute = ticks;
            var roundAbout = totalSpots - ticks;
            var value = ((directRoute < roundAbout) ? directRoute : roundAbout);
            value += Calc_R(requestedSpots.Skip(1).ToList());
            return value;
        }

        static void Main(string[] args)
        {
            var result1 = Calc_R(new List<int> { 5, 3, 7 });
            Console.WriteLine(result1);
            var result2 = Calc_R(new List<int> { 99, 2, 70 });
            Console.WriteLine(result2);
            var result3 = Calc_R(new List<int> { 1, 100, 99 });
            Console.WriteLine(result3);
            Console.ReadLine();
        }
    }
}
