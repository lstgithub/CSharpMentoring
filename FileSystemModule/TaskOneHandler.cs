using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemModule
{
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
}
