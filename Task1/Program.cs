using System;
using System.IO;

namespace ConsoleApp5
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
        static void DeleteUnusedFolders()
        {
            string url = GetCorrectUrl();
            DirectoryInfo dir = new DirectoryInfo(url);

            if (dir.Exists)
            {
                DirectoryInfo[] subDirs = dir.GetDirectories();
                FileInfo[] subFiles = dir.GetFiles();

                foreach (var subDir in subDirs)
                {
                    try
                    {
                        if (DateTime.Now - subDir.LastAccessTime >= TimeSpan.FromMinutes(30))
                            subDir.Delete(true);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Error: {ex.Message}");
                    }

                }
                foreach (var subFile in subFiles)
                {
                    try
                    {
                        if (DateTime.Now - subFile.LastAccessTime >= TimeSpan.FromMinutes(30))
                            subFile.Delete();
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }

            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Task1");
            DeleteUnusedFolders();
        }
    }
}
