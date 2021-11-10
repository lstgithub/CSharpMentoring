using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;

namespace CollectionsModule
{
    class Program
    {
        static void Main(string[] args)
        {
            new TaskOne().TaskOneHandler();
            
            
        }
    }

    public static class ListNthElementSelector
    {
        public static IEnumerable<T> GetNth<T>(this List<T> list, int n) // Returns every N element from the input list
        {
            for (int i = 0; i < list.Count; i += n)
                yield return list[i];
        }
    }

    public class TaskOne
    {
        public void TaskOneHandler()
        {
            int peopleAmount = 40; // Amount of items in the list

            List<int> people = new List<int>();

            for (int i = 1; i < peopleAmount + 1; i++) // Initial list creation
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
                    Console.WriteLine(i);
            }
        }
    }

    public class TaskTwo
    {
        public Array DynamicSizeArray(int arrayLenght)
        {
            return new int[arrayLenght];
        }

        public Array FixedSizeArray()
        {
            return new int[8];
        }

        public Array ExistingArrayCopier(int[] inputArray)
        {
            Array outputArray = inputArray;
            return outputArray;
        }

        public Array AddToArray(int[] inputArray, int elementToAdd)
        {
            int[] resultedArray = inputArray.Concat(new [] { elementToAdd }).ToArray();
            return resultedArray;
        }

        public Array AddRangeToArray(int[] inputArray, int[] rangeToAdd)
        {
            int[] resultedArray = inputArray.Concat(rangeToAdd).ToArray();
            return resultedArray;
        }

        public Array RemoveFromArray(int[] inputArray, int indexOfItemToRemove)
        {
            List<int> arrayToList = inputArray.ToList();
            arrayToList.RemoveAt(indexOfItemToRemove);
            int[] resultedArray = arrayToList.ToArray();
            return resultedArray;
        }

        public Array InsertToArray(int[] inputArray, int element, int index)
        {
            List<int> arrayToList = inputArray.ToList();

            ExistingArrayCopier(inputArray);

            var indexToRemove = index + 1;
            var elementCountToRemove = arrayToList.Last() - indexToRemove;

            arrayToList.RemoveRange(indexToRemove, elementCountToRemove);
            
            return resultedArray;
        }

        public void GetArrayLength()
        {
            
        }

        public void GetArrayCapacity()
        {

        }

        public void SpecificArrayElementHandler()
        {

        }

        public void SortArray()
        {

        }
    }

    public class TaskThree
    {
        public void TaskThreeHandler()
        {

        }
    }
}
