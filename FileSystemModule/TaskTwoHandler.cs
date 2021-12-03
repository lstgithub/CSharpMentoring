using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemModule
{
    public class TaskTwoHandler
    {
        public void TaskHandler()
        {
            string userFolder = Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadsFolder = Path.Combine(userFolder, "Downloads");
            List<string> subDirectories = Directory.GetDirectories(downloadsFolder).ToList();
            List<string> allFiles = new List<string>();
            //string outputFileName = "SearchResults.txt";

            foreach (string f in Directory.GetFiles(downloadsFolder).ToList())
                allFiles.Add(f);

            foreach (string a in subDirectories)
            {
                List<string> newFiles = Directory.GetFiles(a).ToList();
                foreach (string n in newFiles)
                    allFiles.Add(n);
            }

            // Search functionality

            Console.WriteLine("Enter search request:");
            string request = Console.ReadLine();
            List<string> searchResults = allFiles.FindAll(r => r.Contains(request));
            if (searchResults.Count == 0)
                Console.WriteLine("No matches.");
            else
                foreach (string m in searchResults)
                    Console.WriteLine(m);

            Dictionary<string, long> filesWithSize = new Dictionary<string, long>();
            foreach (string x in allFiles)
            {
                string file = x;
                var size = new FileInfo(file).Length;
                filesWithSize.Add(file, size);
            }

            // Biggest files

            var sortedFiles = from z in filesWithSize orderby z.Value descending select z.Key;

            const int topFilesAmount = 5;

            var biggestFilesNames = sortedFiles.Take(topFilesAmount).ToList();

            for (int i = 0; i < topFilesAmount; i++)
            {
                var file = biggestFilesNames[i];
                var a = new FileInfo(file);
                Console.WriteLine("Size of " + file + " is " + a.Length);
            }

            // Average size

            var fileSizes = from y in filesWithSize orderby y.Value descending select y.Value;
            var convertedFileSizes = fileSizes.ToArray();
            var averageBytes = convertedFileSizes.Average();
            var averageMegabytes = (int)averageBytes / 1024;

            Console.WriteLine("Average file size in the selected directory is " + averageMegabytes + " MB");

            // Amount of file names, starting with each alphabet letter

            List<string> fileNamesOnly = new List<string>();

            foreach (string t in allFiles)
            {
                string name = t.Replace(downloadsFolder + "\\", "");
                fileNamesOnly.Add(name);
            }

            if (subDirectories.Count > 0)
            {
                List<string> subDirNames = new List<string>();
                foreach (string dir in subDirectories)
                    subDirNames.Add(dir.Replace(downloadsFolder + "\\", ""));

                foreach (string subDir in subDirNames)
                {
                    foreach (string x in fileNamesOnly)
                    {
                        x.Replace(subDir + "\\", "");
                    }
                }
            }
        }
    }
}
