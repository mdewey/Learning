using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace MaxMinFairness
{
    class Program
    {
        private static IEnumerable<int> LogicOnArray(int size, int[] numbers)
        {
            var front = 0;
            var back = size -1;
            var smallest = int.MaxValue;
            var smallestFront = front;
            var smallestBack = back;
            while (back <= numbers.Length - 1)
            {
                var diff = numbers[back] - numbers[front];
                if (diff < smallest)
                {
                    smallest = diff;
                    smallestBack = back;
                    smallestFront = front;
                }
                front++;
                back++;
            }

            return numbers.Skip(smallestFront).Take(size);
        }


        private static int FindUnFairness(IList<int> group)
        {
            var max = group.Max();
            var min = group.Min();
            return max - min;
        }

        static void Main(string[] args)
        {
            var countOfItems = Convert.ToInt32(Console.ReadLine());
            var k = Convert.ToInt32(Console.ReadLine());
            var items = new List<int>();
            for (var i = 0; i < countOfItems; i++)
            {
                items.Add(Convert.ToInt32(Console.ReadLine()));
            }

            var klist1 = LogicOnArray(k, items.OrderBy(o => o).ToArray());
            //var kList = Logic(k, items.OrderBy(o => o), int.MaxValue, new List<int>());
            Console.WriteLine(FindUnFairness(klist1.ToList()));
            //Console.WriteLine(FindUnFairness(kList.ToList()));
            //Console.ReadLine();
        }
    }
}
