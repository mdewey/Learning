using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonelyInteger
{
    class Program
    {
        static int lonelyinteger(int[] a)
        {
            //slower by .02s
            // var test = a.GroupBy(key => key, value => a.Count(y => y == value)).ToDictionary(k => k.Key, val => val.Distinct().First());
            // return test.First(w => w.Value == 1).Key;

            var data = new Dictionary<int, int>();
            foreach (var num in a)
            {
                if (data.ContainsKey(num))
                {
                    data[num]++;
                }
                else
                {
                    data.Add(num, 1);
                }
            }
            var rv = data.First(w => w.Value == 1);
            return rv.Key;
            
        }
        static void Main(String[] args)
        {
            int res;

            int _a_size = Convert.ToInt32(Console.ReadLine());
            int[] _a = new int[_a_size];
            int _a_item;
            String move = Console.ReadLine();
            String[] move_split = move.Split(' ');
            for (int _a_i = 0; _a_i < move_split.Length; _a_i++)
            {
                _a_item = Convert.ToInt32(move_split[_a_i]);
                _a[_a_i] = _a_item;
            }

            res = lonelyinteger(_a);
            Console.WriteLine(res);

        }
    }
}
