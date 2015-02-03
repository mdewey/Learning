using System;
using System.CodeDom;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlippingBits
{
    class Program
    {

        static long GetPowerOfTwo(int power)
        {
            return (long)Math.Pow(2 , power);
        }

        static Dictionary<int, long> powersOfTwo = new Dictionary<int, long>();


        static string GetIntAs32bitNumber(long number)
        {
            var control = powersOfTwo.Count() - 1 ;
            var rv = "";
            while (control >=0)
            {
                if (number < powersOfTwo[control])
                {
                    rv += "1";
                    
                }
                else
                {
                    rv += "0";
                    number = number - powersOfTwo[control];
                }
                
                control--;
                
            }
            return rv;

        }

       static public long Get32bitAsNumber(string binary)
        {
            long rv = 0 ;
            var charArray = binary.ToCharArray();
            for (var pos = 0; pos < charArray.Length; pos++)
            {
                if (charArray[pos] == '1')
                {
                    rv += powersOfTwo[charArray.Length - 1 - pos];
                }
             
            }
            return rv;

        }

        static void Main(string[] args)
        {
            var countOfItems = Convert.ToInt32(Console.ReadLine());
            var items = new List<UInt32>();
            for (var i = 0; i < countOfItems; i++)
            {
                items.Add(Convert.ToUInt32(Console.ReadLine()));
            }

            for (var i = 0; i <= 31; i++)
            {
                powersOfTwo.Add(i, GetPowerOfTwo(i));
            }
            foreach (var item in items)
            {
                Console.WriteLine(Get32bitAsNumber(GetIntAs32bitNumber(item)));
            }
        }
    }
}
