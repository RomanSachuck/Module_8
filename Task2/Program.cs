using System;
using System.IO;

namespace Task2
{
    internal class Program
    {
        static string GetCorrectUrl()
        {
            string url;
            Console.WriteLine("Введите адрес директории:");
            url = Console.ReadLine();

            while (url.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                Console.WriteLine("Введен некорректный адрес! Повторите попытку:");
                url = Console.ReadLine();
            }

            return url;
        }
        static long FolderSize(string str)
        {
            long i = 0;
            DirectoryInfo workingDirectory = new DirectoryInfo(str);
            DirectoryInfo[] folders = workingDirectory.GetDirectories();
            FileInfo[] files = workingDirectory.GetFiles();

            foreach (FileInfo fl in files)
            {
                i += fl.Length;
            }

            for (int j = 0; j < folders.Length; j++)
            {
                i += FolderSize(str + "\\" + folders[j].Name);
            }

            return i;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Task2");
            Console.WriteLine(FolderSize(GetCorrectUrl()));
        }
    }
}
