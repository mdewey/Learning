using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ACMICPCTeam
{
    class Program
    {

        public class Team
        {
            public string mom{ get; set; }
            public string dad{ get; set; }
            public string child{ get; set; }
            public int topics { get; set; }
            
        }

        public static Team DoOr(string A, string B)
        {
            var rv = "";
            for (var i = 0; i < A.Length; i++)
            {
                if (A[i] == '1' || B[i] == '1')
                {
                    rv += "1";
                }
                else
                {
                    rv += "0";
                }
            }
            return new Team { mom = A, dad = B, child = rv, topics = rv.Count(c => c == '1') };
        }

        static void Main(string[] args)
        {
            var numbers  =  Console.ReadLine().Split(' ');
            var numPeople = int.Parse(numbers[0]);
            
            var items = new List<string>();
            for (var i = 0; i < numPeople; i++)
            {
                items.Add(Console.ReadLine());
            }

            var parents = items.ToArray();
            var deets = new List<Team>();
            for (int i = 0; i < parents.Length-1; i++)
            {
                for (int j = 0; j < parents.Length - 1; j++)
                {
                    deets.Add(DoOr(parents[i], parents[j]));
                }
            }

            var max = deets.Select(s => s.topics).Max();
            var count = deets.Count(c => c.topics == max);

            Console.WriteLine(max);
            Console.WriteLine(count);

            Console.ReadLine();
        }
    }
}
