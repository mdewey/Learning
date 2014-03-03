using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtopianTree
{
    class Program
    {

        public class OutCondition
        {
            public int number { get; set; }
            public string output { get; set; }

            public OutCondition(int n, string o)
            {
                this.number = n;
                this.output = o;
            }
        }

        public class FizzBanging
        {
            private int height;
            public int TreeHeigth { get { return this.height; } }


            public IEnumerable<OutCondition> OutConditions { get; set; }

            public FizzBanging(IEnumerable<OutCondition> outConditions, int h = 0)
            {
                this.OutConditions = outConditions;
                this.height = h;
            }

            public void DoTheFizzBanging(int start = 0, int end = 100)
            {
                if (start > 0)
                    this.CheckFizzBanging(start);
                if (start + 1 <= end)
                {
                    this.DoTheFizzBanging(start + 1, end);
                }

            }

            public void CheckFizzBanging(int i)
            {
                foreach (var condition in OutConditions)
                {
                    switch (i % condition.number)
                    {
                        case 0:
                            this.GrowEvenCycle();
                            break;
                        default:
                            GrowOddCycle();
                            break;
                    }
                }
            }

            private void GrowEvenCycle()
            {
                this.height += 1;
            }

            private void GrowOddCycle()
            {
                this.height *= 2;
            }
        }

        static void Main(string[] args)
        {

            var numberOfYears = new List<int>();

            string line;
            while ((line = Console.ReadLine()) != null && line != "")
            {
                numberOfYears.Add(int.Parse(line));
            }


            var con = new List<OutCondition>
            {
                new OutCondition(2, "even")
            };

            
            foreach (var year in numberOfYears.Skip(1))
            {
                var tree = new FizzBanging(con, 1);
                tree.DoTheFizzBanging(0, year);    
                Console.WriteLine(tree.TreeHeigth);
    
            }
            


            Console.ReadLine();
        }
    }
}
