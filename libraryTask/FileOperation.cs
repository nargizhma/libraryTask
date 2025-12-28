using System;
using System.Collections.Generic;
using System.Text;

namespace libraryTask
{
    internal class FileOperation
    {
        public static string directoryPath = @"C:\Users\Nergiz\source\repos\libraryTask";
        public static string filePath = Path.Combine(directoryPath, "records.txt");

        public void saveToFile(string lineData)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(lineData);
            }
            return;
        }
        public List<string> ReadData()
        {
            List<string> books = new List<string>();
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string lineData = sr.ReadLine();
                    while (lineData != null)
                    {
                        books.Add(lineData);
                        lineData = sr.ReadLine();
                    }
                }
            }
            return books;

        }

    }

}
