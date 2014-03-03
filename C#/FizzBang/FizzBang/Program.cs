using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBang
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
            public IEnumerable<OutCondition> OutConditions { get; set; }

            public FizzBanging(IEnumerable<OutCondition> outConditions)
            {
                this.OutConditions = outConditions;
            }

            public void DoTheFizzBanging(int start = 0, int end = 100)
            {

                this.CheckFizzBanging(start);
                if (start != end)
                {
                    this.DoTheFizzBanging(start + 1);
                }
                
            }

            public void CheckFizzBanging(int i)
            {
                var doPrint = true;

                foreach (var condition in OutConditions.Where(condition => i %  condition.number == 0))
                {
                    Console.Write(condition.output);
                    doPrint = false;
                }
                if (doPrint)
                    Console.Write(i);
                Console.WriteLine();
            }
        }


        static void Main(string[] args)
        {
            var con = new List<OutCondition>
            {
                new OutCondition(3, "Fizz"),
                new OutCondition(5, "Bang")
            };

            new FizzBanging(con).DoTheFizzBanging();

            Console.ReadLine();
        }
    }
}
