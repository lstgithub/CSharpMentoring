using System;
using System.Linq;

namespace FundamentalsArrayModule
{
    // TASK 1, VARIANT 1
    class Program
    {
        static void Main(string[] args)
        {
            var sourceArray = ArrayUtils.CreateArray(20);
            foreach (int i in sourceArray)
                Console.WriteLine(i + " source array");
            int maxNegativeIndex = Array.IndexOf(sourceArray, sourceArray.Where(i => i < 0).Min());
            int minPositiveIndex = Array.IndexOf(sourceArray, sourceArray.Where(i => i > 0).Min());
            var swappedArray = ArrayUtils.SwapArray(sourceArray, maxNegativeIndex, minPositiveIndex);
            foreach (int i in swappedArray)
                Console.WriteLine(i + " swapped array");
        }
    }

    class ArrayUtils
    {
        public static int[] CreateArray(int count)
        {
            Random random = new Random();
            int[] array = new int[count];
            for (int i = 0; i < count; ++i)
                array[i] = random.Next(-10, 10);
            return array;
        }

        public static int[] SwapArray(int[] array, int maxIndex, int minIndex)
        {
            (array[maxIndex], array[minIndex]) = (array[minIndex], array[maxIndex]);
            return array;
        }
    }
}
