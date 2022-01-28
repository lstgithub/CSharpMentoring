using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsModule
{
    class TaskTwoHelpers
    {
        public int[] DynamicSizeArray(List<int> values)
        {
            List<int> dynamicList = new List<int>();

            foreach (int i in values)
                dynamicList.Add(i);

            var outputArray = dynamicList.ToArray();

            return outputArray;
        }

        public int[] FixedSizeArray()
        {
            return new int[8];
        }

        public int[] ExistingArrayCopier(int[] inputArray)
        {
            var outputArray = inputArray;
            return outputArray;
        }

        public int[] AddToArray(int[] inputArray, int elementToAdd)
        {
            int[] resultedArray = inputArray.Concat(new[] { elementToAdd }).ToArray();
            return resultedArray;
        }

        public int[] AddRangeToArray(int[] inputArray, int[] rangeToAdd)
        {
            int[] resultedArray = inputArray.Concat(rangeToAdd).ToArray();
            return resultedArray;
        }

        public int[] RemoveFromArray(int[] inputArray, int indexOfItemToRemove)
        {
            List<int> arrayToList = inputArray.ToList();
            arrayToList.RemoveAt(indexOfItemToRemove);
            int[] resultedArray = arrayToList.ToArray();
            return resultedArray;
        }

        public int[] InsertToArray(int[] inputArray, int index, int element)
        {
            List<int> arrayToList = inputArray.ToList();

            arrayToList.Insert(index, element);

            // Non-native solution to insert an element
            /*
            var count = arrayToList.Count - index;
            
            List<int> firstPart = arrayToList.GetRange(0, index).ToList();
            List<int> secondPart = arrayToList.GetRange(index, count).ToList();

            firstPart.Add(element);

            List<int> output = firstPart.Concat(secondPart).ToList();
            */

            foreach (int i in arrayToList)
                Console.WriteLine(i);
            Console.WriteLine("\n");

            return arrayToList.ToArray();
        }

        public int GetArrayLength(int[] inputArray)
        {
            ArrayList inputArrayList = new ArrayList(inputArray);
            var length = inputArrayList.Count;
            return length;
        }

        public int GetArrayCapacity(int[] inputArray)
        {
            ArrayList inputArrayList = new ArrayList(inputArray);
            var capacity = inputArrayList.Capacity;
            return capacity;
        }

        public int SpecificArrayElementHandler(int[] inputArray, int index)
        {
            var element = inputArray[index];
            Console.WriteLine("Element is " + element + "\n");
            return element;
        }

        public delegate int[] SortArray(int[] inputArray);
        public static int[] Asc(int[] inputArray)
        {
            Array.Sort(inputArray);
            return inputArray;
        }
        public static int[] Desc(int[] inputArray)
        {
            Array.Reverse(inputArray);
            return inputArray;
        }
    }
}
