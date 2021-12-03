using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemModule
{
    public class TaskThreeHandler
    {
        public static string userFolder = Environment.GetEnvironmentVariable("USERPROFILE");
        public static string targetControlFolder = Path.Combine(userFolder, "Downloads", "TextVersionControl");
        public static List<string> filesUnderControl = new();

        public void TextVersionControl()
        {
            string currentDateTime = DateTime.Now.ToString();
            string formattedDateTime = currentDateTime.Replace("/", "-").Replace(":", "-");
            string versioningFile = "VersionControl";
            List<string> availableStates = new();

            // Entering selected mode
            Console.WriteLine("Select app mode - seeking for changes (1) or reverting changes (2)");
            var mode = Console.ReadLine();

            if (mode == "1")
            {
                Console.WriteLine("Observation mode ON, to exit press (s)");

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
                    var originalName = new FileInfo(f).Name;
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

                var t = Int32.Parse(Console.ReadLine());

                // После нажатия удалять оригинальные файлы в корне

                foreach (string f in Directory.GetFiles(targetControlFolder).ToList())
                {
                    if (new FileInfo(f).Name != versioningFile)
                        File.Delete(f);
                }

                var selectedState = g[t];
                // И записывать выбранные из папки в корень
                foreach (string f in Directory.GetFiles(targetControlFolder + "\\" + selectedState).ToList())
                {
                    File.Copy(f, targetControlFolder + "\\" + new FileInfo(f).Name);
                }
            }
        }

        public void ChangeDetector()
        {
            FileSystemWatcher watcher = new FileSystemWatcher(targetControlFolder, "*.txt");
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += ChangeDetectorHelper;
            watcher.EnableRaisingEvents = true;

            Console.ReadKey();
        }

        public void ChangeDetectorHelper(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Change detected - " + e.Name);
        }

        public void Backup()
        {

        }

        public void Restore()
        {

        }
    }
}
