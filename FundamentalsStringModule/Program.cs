using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;

namespace FundamentalsStringModule
{
    // TASK 2, VARIANT 1
    class Program
    {
        static void Main(string[] args)
        {
            int stringAmount = 4;
            List<string> stringArray = new List<string>();

            for (int i = 0; i < stringAmount; i++)
            {
                var newStr = Console.ReadLine();
                stringArray.Add(newStr);
            }

            StringHelper.SymbolCounter(stringArray);
        }
    }

    class StringHelper
    {
        public static void SymbolCounter(List<string> stringArray)
        {
            List<string> newArray = new List<string>();

            foreach (string s in stringArray)
            {
                int length = s.Length;
                var longest = stringArray.Max(s => s.Length);
                var shortest = stringArray.Min(s => s.Length);
                if (length == longest)
                {
                    Console.WriteLine("String " + s + " is longest with a length " + s.Length);
                    newArray.Add(s);
                }
                if (length == shortest)
                {
                    Console.WriteLine("String " + s + " is shortest with a length " + s.Length);
                    newArray.Add(s);
                }
            }
        }
    }
}
