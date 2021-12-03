using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemModule
{
    public class TaskThreeHandler
    {
        public static string UserFolder = Environment.GetEnvironmentVariable("USERPROFILE");
        public static string TargetControlFolder = Path.Combine(UserFolder, "Downloads", "TextVersionControl");
        public static List<string> filesUnderControl = new();
        public static string currentDateTime = DateTime.Now.ToString();
        public static string formattedDateTime = currentDateTime.Replace("/", "-").Replace(":", "-");
        public static string versioningFile = "VersionControl";
        public static List<string> availableStates = new();

        public void TextVersionControl()
        {
            Console.WriteLine("Select app mode - seeking for changes (1) or reverting changes (2)");
            var mode = Console.ReadLine();

            if (mode == "1")
            {
                Console.WriteLine("Observation mode ON, to exit press (s)" + "\n" + "Current state has been saved");
                ChangeDetector();
            }

            if (mode == "2")
            {
                Console.WriteLine("Restoration mode ON");
                Restore();
            }
        }

        public void ChangeDetector()
        {
            FileSystemWatcher watcher = new FileSystemWatcher(TargetControlFolder, "*.txt");
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += ChangeDetectorHelper;
            watcher.EnableRaisingEvents = true;

            Console.ReadKey();
        }

        public void ChangeDetectorHelper(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Change detected - " + e.Name);
            Backup();
        }

        public void Backup()
        {

            foreach (string f in Directory.GetFiles(TargetControlFolder).ToList())
                filesUnderControl.Add(f);

            var newDirectory = Directory.CreateDirectory(TargetControlFolder + "\\" + formattedDateTime);

            availableStates.Add(formattedDateTime);

            if (!File.Exists(TargetControlFolder + "\\" + versioningFile))
                File.Create(TargetControlFolder + "\\" + versioningFile).Dispose();

            using var fileStreamOne = new FileStream(TargetControlFolder + "\\" + versioningFile, FileMode.Append);
            using var streamWriterOne = new StreamWriter(fileStreamOne);

            foreach (string i in availableStates)
                streamWriterOne.WriteLine(i);
            streamWriterOne.Dispose();

            foreach (string f in filesUnderControl)
            {
                var originalName = new FileInfo(f).Name;
                if (originalName != versioningFile)
                    File.Copy(TargetControlFolder + "\\" + originalName, newDirectory + "\\" + originalName);
            }
        }

        public void Restore()
        {
            // Показать доступные состояния из availableStates

            var g = File.ReadAllLines(TargetControlFolder + "\\" + versioningFile);

            var l = g.Length;

            for (int i = 0; i < l; i++)
            {
                Console.WriteLine(g[i] + " - press " + i + " to restore");
            }

            // Связать их с нажатием клавиши

            var t = Int32.Parse(Console.ReadLine());

            // После нажатия удалять оригинальные файлы в корне

            foreach (string f in Directory.GetFiles(TargetControlFolder).ToList())
            {
                if (new FileInfo(f).Name != versioningFile)
                    File.Delete(f);
            }

            var selectedState = g[t];
            // И записывать выбранные из папки в корень
            foreach (string f in Directory.GetFiles(TargetControlFolder + "\\" + selectedState).ToList())
            {
                File.Copy(f, TargetControlFolder + "\\" + new FileInfo(f).Name);
            }
        }
    }
}
