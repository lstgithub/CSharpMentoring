using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsModule
{
    class TaskThreeHandler
    {
        public void TaskThree()
        {
            List<int> skillValues = new List<int> { 8, 6, 5, 4, 9, 7, 8 }; // Last element serves to demonstrate exception handling

            int[] initialArray = new TaskTwoHelpers().DynamicSizeArray(skillValues);
            TaskTwoHelpers.SortArray sortArray = TaskTwoHelpers.Asc;
            var ascInitialArray = sortArray(initialArray);

            int size = 0;
            if (skillValues.Count() % 2 == 0)
                size = skillValues.Count();
            else
                size = skillValues.Count() - 1;

            int[] fixedArray = new int[size];

            if (initialArray.Length / 2 != 0)
                fixedArray = new TaskTwoHelpers().RemoveFromArray(ascInitialArray, 0);
            else
                fixedArray = ascInitialArray;

            int arraysAmount = fixedArray.Length / 2;

            int[][] playerPairs = new int[arraysAmount][];

            int counter = 0;
            for (int i = 0; i < arraysAmount; i++)
            {
                int[] pair = new int[0];
                pair = new TaskTwoHelpers().AddToArray(pair, fixedArray[counter]);
                pair = new TaskTwoHelpers().AddToArray(pair, fixedArray[counter + 1]);
                counter = counter + 2;
                playerPairs[i] = pair;
            }

            foreach (int[] x in playerPairs)
                foreach (int i in x)
                    Console.WriteLine(i + " pair");
        }
    }
}
