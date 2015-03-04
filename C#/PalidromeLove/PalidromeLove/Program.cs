using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PalidromeLove
{
    class Program
    {

        private static char SubtractChar(char root)
        {
            var bt = Convert.ToByte(root);
            var newValue = bt - 1;
            var rv = Convert.ToChar(newValue);
            return rv;

        }

        private static char BoundSubtraction(char root)
        {
            return root == 'a' ? root : SubtractChar(root);
        }

        private static dynamic ConvertToPalidrome(string word)
        {
            var front = 0;
            var back = word.Length -1;
            var bytes = ConvertToStringToByteArray(word).ToArray();
            var count = 0;
            while ((back >= front))
            {
                while (bytes[front] != bytes[back])
                {
                    count++;
                    if (bytes[front] > bytes[back])
                    {
                        bytes[front] = Convert.ToByte(bytes[front] - 1);
                    }
                    else
                    {
                        bytes[back] = Convert.ToByte(bytes[back] - 1);
                    }
                }
                front++;
                back--;
            }
            return count;
            
        }

        private static IEnumerable<byte> ConvertToStringToByteArray(string word)
        {
            return word.Select(Convert.ToByte);

        }

        static void Main(string[] args)
        {
            // read from STDIN
            var countOfItems = Convert.ToInt32(Console.ReadLine());
            var items = new List<string>();
            for (var i = 0; i < countOfItems; i++)
            {
                items.Add(Console.ReadLine());
            }

            foreach (var item in items)
            {
                Console.WriteLine(ConvertToPalidrome(item));
            }

            Console.ReadLine();

        }
    }
}
