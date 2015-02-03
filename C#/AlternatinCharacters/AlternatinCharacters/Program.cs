using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AlternatinCharacters
{
    class Program
    {

        private static int GetMinCount(char[] haystack)
        {
            var current = haystack[0];
            var deletions = 0;
            for (var i = 1; i < haystack.Length; i++)
            {
                if (current == haystack[i])
                {
                    //deletion needs to happen
                    deletions++;
                }
                else
                {
                    current = haystack[i];
                }
            }
            return deletions;
        }


        static void Main(string[] args)
        {
            var countOfItems = Convert.ToInt32(Console.ReadLine());
            var items = new List<char[]>();
            for (var i = 0; i < countOfItems; i++)
            {
                items.Add(Console.ReadLine().ToCharArray());
            }

           
            foreach (var item in items)
            {
                Console.WriteLine(GetMinCount(item));
            }
            
        }
    }
}
