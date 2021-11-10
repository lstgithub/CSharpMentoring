using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CollectionsModule
{
    class Program
    {
        static void Main(string[] args)
        {
            int peopleAmount = 40;

            List<int> people = new List<int>();

            for (int i = 1; i < peopleAmount + 1; i++) // Initial array
                people.Add(i);

            Console.WriteLine("Initial array:");
            foreach (int i in people)
                Console.WriteLine(i);

            var reducedList = from i in people.GetNth(2) select i;

            Console.WriteLine("Array after first iteration:");
            foreach (int i in reducedList)
                Console.WriteLine(i);

            //

            while (people.Count != 1)
            {

            }
        }
    }

    public static class ListNthElementSelector
    {
        public static IEnumerable<T> GetNth<T>(this List<T> list, int n)
        {
            for (int i = 0; i < list.Count; i += n)
                yield return list[i];
        }
    }
}
