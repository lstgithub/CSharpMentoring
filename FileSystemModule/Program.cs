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
            //new TaskOneHandler().TaskHandler();
            new TaskTwoHandler().TaskHandler();
        }
    }

    public class TaskOneHandler
    {
        public void TaskHandler()
        {
            // There is much better way to get location of the folder "Downloads", via WinAPI, however I decided not to overcomplicate the task

            string userFolder = Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadsFolder = Path.Combine(userFolder, "Downloads");
            string fileName = "TextFile.txt";
            string fullCombinedPath = Path.Combine(downloadsFolder, fileName);

            if (File.Exists(fullCombinedPath))
                File.Delete(fullCombinedPath);
            else
                File.Create(fullCombinedPath);

            using var fileStreamOne = new FileStream(fullCombinedPath, FileMode.Append);
            using var streamWriterOne = new StreamWriter(fileStreamOne);

            int[] numbers = { 1, 7, 4, 3, 11 };
            foreach (int i in numbers)
                streamWriterOne.WriteLine(i);

            streamWriterOne.Dispose(); // Freeing text file to avoid exception

            var receivedOriginalText = File.ReadLines(fullCombinedPath);
            var squaredChangedText = new List<int>();

            foreach (string a in receivedOriginalText)
            {
                int parsedValue = Int32.Parse(a);
                int squaredValue = parsedValue * parsedValue;
                squaredChangedText.Add(squaredValue);
            }

            int[] newSquaredValues = squaredChangedText.ToArray();

            File.WriteAllText(fullCombinedPath, ""); // Removing existing content

            using var fileStreamTwo = new FileStream(fullCombinedPath, FileMode.Append);
            using var streamWriterTwo = new StreamWriter(fileStreamTwo);

            foreach (int i in newSquaredValues)
                streamWriterTwo.WriteLine(i);

            streamWriterTwo.Dispose();
        }
    }

    public class TaskTwoHandler
    {
        public void TaskHandler()
        {
            string userFolder = Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadsFolder = Path.Combine(userFolder, "Downloads");
            List<string> subDirectories = Directory.GetDirectories(downloadsFolder).ToList();
            List<string> allFiles = new List<string>();
            string outputFileName = "SearchResults.txt";

            foreach (string f in Directory.GetFiles(downloadsFolder).ToList())
                allFiles.Add(f);

            foreach (string a in subDirectories)
            {
                List<string> newFiles = Directory.GetFiles(a).ToList();
                foreach (string n in newFiles)
                    allFiles.Add(n);
            }

            Console.WriteLine("Enter search request:");
            string request = Console.ReadLine();

            List<string> searchResults = allFiles.FindAll(r => r.Contains(request));
            
            if (searchResults.Count == 0)
                Console.WriteLine("No matches.");
            else
            {
                foreach (string m in searchResults)
                    Console.WriteLine(m);
            }


            foreach (string z in allFiles)
            {
                var a = new FileInfo(z);
                Console.WriteLine(z + a.Length);

            }

        }
    }
}
