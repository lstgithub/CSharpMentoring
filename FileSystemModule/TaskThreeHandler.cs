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
        public static List<string> FilesUnderControl = new();
        public static string CurrentDateTime = "";
        public static string FormattedDateTime = "";
        public static string VersionFile = "VersionControl";
        public static List<string> AvailableStates = new();
        public string newBackUpDirectory = "";

        public string TimeDate()
        {
            CurrentDateTime = DateTime.Now.ToString();
            return FormattedDateTime = CurrentDateTime.Replace("/", "-").Replace(":", "-");
        }

        public void TextVersionControl()
        {
            Console.WriteLine("Press (1) to start observation or press (2) to start restoration");
            var mode = Console.ReadLine();

            if (mode == "1")
            {
                Console.WriteLine("Observation mode ON, to exit press (s)");
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
            Console.WriteLine("Monitoring was disabled, exiting application");
        }

        public void ChangeDetectorHelper(object sender, FileSystemEventArgs e)
        {
            string name = e.Name;
            Console.WriteLine("Change detected - " + name);
            string newTime = TimeDate();
            newBackUpDirectory = Directory.CreateDirectory(TargetControlFolder + "\\" + newTime).ToString();
            Console.WriteLine("All files were backed up");
            Backup(newTime);
        }

        public void Backup(string formattedTime) // Could be improved by making backup only for changed file
        {
            foreach (string f in Directory.GetFiles(TargetControlFolder).ToList())
                FilesUnderControl.Add(f);

            AvailableStates.Add(formattedTime);

            if (!File.Exists(TargetControlFolder + "\\" + VersionFile))
                File.Create(TargetControlFolder + "\\" + VersionFile).Dispose();

            using var fileStreamOne = new FileStream(TargetControlFolder + "\\" + VersionFile, FileMode.Append);
            using var streamWriterOne = new StreamWriter(fileStreamOne);

            foreach (string i in AvailableStates)
                streamWriterOne.WriteLine(i);
            streamWriterOne.Dispose();

            foreach (string f in FilesUnderControl)
            {
                var originalName = new FileInfo(f).Name;
                if (originalName != VersionFile)
                    File.Copy(TargetControlFolder + "\\" + originalName, newBackUpDirectory + "\\" + originalName);
            }

            FilesUnderControl.Clear();
        }

        public void Restore()
        {
            var stateLines = File.ReadAllLines(TargetControlFolder + "\\" + VersionFile);
            var stateAmount = stateLines.Length;

            for (int i = 0; i < stateAmount; i++)
                Console.WriteLine(stateLines[i] + " - press " + i + " to restore");

            var counter = Int32.Parse(Console.ReadLine());

            foreach (string f in Directory.GetFiles(TargetControlFolder).ToList())
                if (new FileInfo(f).Name != VersionFile)
                    File.Delete(f);

            var selectedState = stateLines[counter];

            foreach (string f in Directory.GetFiles(TargetControlFolder + "\\" + selectedState).ToList())
                File.Copy(f, TargetControlFolder + "\\" + new FileInfo(f).Name);

            Console.WriteLine("Selected state has been restored, exiting application");
        }
    }
}
