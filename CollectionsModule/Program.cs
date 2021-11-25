﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CollectionsModule
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] inputArray = { 1, 4, 2, 9, 0, 5, 3, 7, 6 }; // Hardcoded array for further handling

            /*try
            {
                new TaskTwo().SpecificArrayElementHandler(inputArray, 3);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Error, incorrect index has been provided" + "\n");
            }

            try
            {
                new TaskTwo().InsertToArray(inputArray, 100, 2);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Error, incorrect index has been provided" + "\n");
            }

            TaskTwo.SortArray sortArray = TaskTwo.Asc;
            var asc = sortArray(inputArray);
            foreach (int i in asc)
                Console.WriteLine(i);
            */
            new TaskThree().TaskThreeHandler();
        }
    }

    public static class ListNthElementSelector
    {
        public static IEnumerable<T> GetNth<T>(this List<T> list, int n) // Returns every Nth element from the input list
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
        public int[] DynamicSizeArray(List<int> values)
        {
            List<int> dynamicList = new List<int>();

            foreach (int i in values)
                dynamicList.Add(i);

            var outputArray = dynamicList.ToArray();

            return outputArray;
        }

        public Array FixedSizeArray()
        {
            return new int[8];
        }

        public Array ExistingArrayCopier(int[] inputArray)
        {
            var outputArray = inputArray;
            return outputArray;
        }

        public int[] AddToArray(int[] inputArray, int elementToAdd)
        {
            int[] resultedArray = inputArray.Concat(new [] { elementToAdd }).ToArray();
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
        
        public Array InsertToArray(int[] inputArray, int index, int element)
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

    public class TaskThree
    {
        public void TaskThreeHandler()
        {
            List<int> skillValues = new List<int> { 8, 6, 5, 4, 9, 7, 8 }; // Last element serves to demonstrate exception handling

            int[] initialArray = new TaskTwo().DynamicSizeArray(skillValues);
            TaskTwo.SortArray sortArray = TaskTwo.Asc;
            var ascInitialArray = sortArray(initialArray);

            int arraysAmount = 0;

            int size = 0;
            if (skillValues.Count() % 2 == 0)
                size = skillValues.Count();
            else
                size = skillValues.Count() - 1;

            int[] fixedArray = new int[size];

            if (initialArray.Length / 2 != 0)
                fixedArray = new TaskTwo().RemoveFromArray(ascInitialArray, 0);
            else
                fixedArray = ascInitialArray;

            arraysAmount = fixedArray.Length / 2;

            int[][] playerPairs = new int[arraysAmount][];

            int counter = 0;

            for (int i = 0; i < arraysAmount; i++)
            {
                int[] pair = new int[0];
                pair = new TaskTwo().AddToArray(pair, fixedArray[counter]);
                pair = new TaskTwo().AddToArray(pair, fixedArray[counter + 1]);
                counter = counter + 2;
                playerPairs[i] = pair;
            }

            foreach (int[] x in playerPairs)
                foreach (int i in x)
                {
                    Console.WriteLine(i);
                }
        }
    }
}