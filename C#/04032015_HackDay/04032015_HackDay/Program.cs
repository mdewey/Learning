using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace _04032015_HackDay
{
    class Program
    {

        public class Item
        {
            public string Name { get; set; }
            public DateTime TimeCreated { get; set; }
            public bool Completed { get; set; }
            public string CreatedBy { get; set; }
        }


        public static IList<Item> MyList { get; set; }

        static void Main(string[] args)
        {
            MyList = new List<Item>();

            for (int i = 0; i < 100; i++)
            {
                var completed = (i%2 == 0);
                MyList.Add(new Item(){Name = "task " + i, Completed = completed});
            }

            var notcompletedItems = MyList.Where(item => !item.Completed);
            var count = 0;
            
            Console.Write("Done items");
            foreach (var item in notcompletedItems)
            {
                count++;
                Console.WriteLine("{0}),{1}", count, item.Name);
            }
            
            Console.Write("not done items");
            count = 0;
            foreach (var item in MyList)
            {
                count++;
                Console.WriteLine("{0}),{1}", count, item.Name);
            }

            Console.WriteLine(notcompletedItems.Count());
            Console.WriteLine(MyList.Count());
            Console.ReadLine();

            MyList.First(f => f.Name == "Task 1").CreatedBy = "me";

            MyList.RemoveAt(3);
        }
    }
}
