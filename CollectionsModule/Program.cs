using System;
using System.Collections.Generic;
using System.Linq;

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

            int iterationIndex = 0;
            while (people.Count != 1)
            {
                iterationIndex++;
                var reducedList = from i in people.GetNth(2) select i;
                people = reducedList.ToList();
                Console.WriteLine("Iteration " + iterationIndex);
                foreach (var i in people)
                {
                    Console.WriteLine(i);
                }
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
