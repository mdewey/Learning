using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCredit
{
    class Program
    {
        private static IEnumerable<int> LogicOnArray(int size, int[] numbers)
        {
            var front = 0;
            var back = size - 1;
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

        public class TestCase
        {
            public int Credits { get; set; }
            public int numOfItems { get; set; }
            public IEnumerable<int> Items { get; set; }
        }

        public List<int> FindNumbers(TestCase items)
        {
            
        }

        static void Main(string[] args)
        {
            var numTestCases = Convert.ToInt32(Console.ReadLine());
            var testCases = new List<TestCase>();
            for (int i = 0; i < numTestCases; i++)
            {
                var c = Convert.ToInt32(Console.ReadLine());
                var num = Convert.ToInt32(Console.ReadLine());
                var items = Console.ReadLine().Split(' ').Select(s => int.Parse(s));
                testCases.Add(new TestCase{Credits = c, Items = items, numOfItems = num});
            }
            
            Console.ReadLine();
        }
    }
}
