using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemModule
{
    class Program
    {
        static void Main(string[] args)
        {
            new TaskOneHandler().TaskHandler();
            Console.WriteLine("\n");
            new TaskTwoHandler().TaskHandler();
            Console.WriteLine("\n");
            new TaskThreeHandler().TextVersionControl();
        }
    }
}