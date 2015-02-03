using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MaximizingXOR
{
    class Program
    {
        static bool xor(char left, char right)
        {
            return (left == '1') ^ (right == '1');
        }

        static char[] paddByteArray(char[] shorty, char[] bigger)
        {
            var list = shorty.ToList();
            while (list.Count() < bigger.Length)
            {
                list.Insert(0, '0');
            }
            return list.ToArray();
        }

        static int maxXor(int l, int r)
        {
            var maxValue = -1;
            for (int i = l; i < r; i++)
            {
                for (int j = l; j <= r; j++)
                {
                    var firstNum = Convert.ToString(i, 2).ToCharArray();
                    var secondNum = Convert.ToString(j, 2).ToCharArray();

                    Console.Out.WriteLine("=======");

                    if (firstNum.Length > secondNum.Length)
                    {
                        secondNum = paddByteArray(secondNum, firstNum);
                    }
                    else if (firstNum.Length < secondNum.Length)
                    {
                        firstNum = paddByteArray(firstNum, secondNum);
                    }

                    Console.Out.WriteLine(firstNum);
                    Console.Out.WriteLine(secondNum);

                    var xored = new List<string>();


                    for (int b = 0; b < firstNum.Length; b++)
                    {
                        var xo = xor(firstNum[b], secondNum[b]);
                        xored.Insert(0, xo ? "1" : "0");
                        Console.Out.WriteLine("{0}|{1}|{2}", firstNum[b], secondNum[b], xo);
                    }
                    xored.Reverse();
                    var stringed = String.Join("", xored);
                    Console.Out.WriteLine(stringed);
                    var intValue = Convert.ToInt32(stringed, 2);
                    Console.Out.WriteLine(intValue);
                    if (intValue >= maxValue)
                    {
                        maxValue = intValue;
                    }

                }
            }
            return maxValue;
        }

        static void Main(String[] args)
        {
            int res;
            int _l;
            _l = Convert.ToInt32(Console.ReadLine());

            int _r;
            _r = Convert.ToInt32(Console.ReadLine());

            res = maxXor(_l, _r);
            Console.WriteLine(res);
            Console.ReadLine();
        }
    }
}
