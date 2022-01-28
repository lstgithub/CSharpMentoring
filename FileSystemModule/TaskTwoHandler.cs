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
            string outputFileName = Path.Combine(downloadsFolder + "\\" + "SearchResults.txt");

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

            if (File.Exists(outputFileName))
                File.Delete(outputFileName);
            else
                File.Create(outputFileName);

            if (searchResults.Count == 0)
                Console.WriteLine("No matches.");
            else
                foreach (string m in searchResults)
                {
                    Console.WriteLine(m);
                    using var fileStreamOne = new FileStream(outputFileName, FileMode.Append);
                    using var streamWriterOne = new StreamWriter(fileStreamOne);
                    streamWriterOne.WriteLine(m);
                    streamWriterOne.Dispose();
                }

            // Biggest files

            Dictionary<string, long> filesWithSize = new Dictionary<string, long>();
            foreach (string x in allFiles)
            {
                string file = x;
                var size = new FileInfo(file).Length;
                filesWithSize.Add(file, size);
            }

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

            List<string> alphabet = new();

            for (char c = 'A'; c < 'Z'; c++)
                alphabet.Add(c.ToString());

            List<string> fileNamesOnly = new();

            foreach (string t in allFiles)
                fileNamesOnly.Add(t.Replace(downloadsFolder + "\\", ""));

            Dictionary<string, int> pairCharInt = new();

            foreach (string f in fileNamesOnly)
            {
                var initialChar = f.Remove(1, f.Length - 1).ToUpper();

                var counter = fileNamesOnly.Count(s => s.StartsWith(initialChar));

                if (alphabet.Contains(initialChar))
                {
                    if (!pairCharInt.ContainsKey(initialChar))
                        pairCharInt.Add(initialChar, counter);
                }

            }

            foreach (var x in pairCharInt)
                Console.WriteLine(x.Value + " files starting with " + x.Key);
        }
    }
}
