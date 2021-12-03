using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net.Mime;

namespace FileSystemModule
{
    class Program
    {
        static void Main(string[] args)
        {
            //new TaskOneHandler().TaskHandler();
            //new TaskTwoHandler().TaskHandler();
            new TaskThreeHandler().TextVersionControl();
            //new TaskThreeHandler().ChangeDetector();
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

            Console.WriteLine("");
        }
    }

    public class TaskThreeHandler
    {
        public static string userFolder = Environment.GetEnvironmentVariable("USERPROFILE");
        public static string targetControlFolder = Path.Combine(userFolder, "Downloads", "TextVersionControl");
        public void TextVersionControl()
        {
            string currentDateTime = DateTime.Now.ToString();
            string formattedDateTime = currentDateTime.Replace("/", "-").Replace(":", "-");
            string originalName = "";
            string versioningFile = "VersionControl";
            List<string> availableStates = new List<string>();

            // Entering selected mode
            Console.WriteLine("Select app mode - seeking for changes (1) or reverting changes (2)");
            var mode = Console.ReadLine();

            if (mode == "1")
            {
                Console.WriteLine("Observation mode ON, to exit press (s)");

                List<string> filesUnderControl = new List<string>();
                foreach (string f in Directory.GetFiles(targetControlFolder).ToList())
                    filesUnderControl.Add(f);

                var newDirectory = Directory.CreateDirectory(targetControlFolder + "\\" + formattedDateTime);

                availableStates.Add(formattedDateTime);

                if (!File.Exists(targetControlFolder + "\\" + versioningFile))
                    File.Create(targetControlFolder + "\\" + versioningFile).Dispose();

                using var fileStreamOne = new FileStream(targetControlFolder + "\\" + versioningFile, FileMode.Append);
                using var streamWriterOne = new StreamWriter(fileStreamOne);

                foreach (string i in availableStates)
                    streamWriterOne.WriteLine(i);
                streamWriterOne.Dispose();

                foreach (string f in filesUnderControl)
                {
                    originalName = new FileInfo(f).Name;
                    if (originalName != versioningFile)
                        File.Copy(targetControlFolder + "\\" + originalName, newDirectory + "\\" + originalName);
                }

                ChangeDetector();

                Console.WriteLine("Current state backed up");
            }

            if (mode == "2")
            {
                Console.WriteLine("Reverse mode ON");

                // Показать доступные состояния из availableStates

                var g = File.ReadAllLines(targetControlFolder + "\\" + versioningFile);

                var l = g.Length;

                for (int i = 0; i < l; i++)
                {
                    Console.WriteLine(g[i] + " - press " + i + " to restore");
                }

                // Связать их с нажатием клавиши

                var t = Console.ReadLine();

                if (Int32.Parse(t) > l || Int32.Parse(t) < l)
                    Console.WriteLine("Incorrect state selected");


                // После нажатия удалять оригинальные файлы в корне и перезаписывать их с теми из выбранной папки


            }

            Console.WriteLine("");
        }

        public void ChangeDetector()
        {
            FileSystemWatcher watcher = new FileSystemWatcher(targetControlFolder, "*.txt");
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;

            Console.ReadKey();
        }

        public void OnChanged(object sender, FileSystemEventArgs e)
        {
            
        }
    }
}