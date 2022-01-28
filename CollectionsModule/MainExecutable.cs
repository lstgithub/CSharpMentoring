﻿using System;

namespace CollectionsModule
{
    class MainExecutable
    {
        static void Main(string[] args)
        {
            int[] inputArray = { 1, 4, 2, 9, 0, 5, 3, 7, 6 }; // Hardcoded array for further handling

            try
            {
                new TaskTwoHelpers().SpecificArrayElementHandler(inputArray, 3);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Error, incorrect index has been provided" + "\n");
            }

            try
            {
                new TaskTwoHelpers().InsertToArray(inputArray, 100, 2);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Error, incorrect index has been provided" + "\n");
            }

            new TaskOneHandler().TaskOne();

            new TaskThreeHandler().TaskThree();
        }
    }
}