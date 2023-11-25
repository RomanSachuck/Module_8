using System;
using System.IO;

namespace Task3
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
        static void DeleteUnusedFolders(string url)
        {
            DirectoryInfo dir = new DirectoryInfo(url);
            int countDeletedFiles = 0;
            int countDeletedFolders = 0;
            long sizeDeletedObjects = 0;

            if (dir.Exists)
            {
                DirectoryInfo[] subDirs = dir.GetDirectories();
                FileInfo[] subFiles = dir.GetFiles();

                foreach (var subDir in subDirs)
                {
                    try
                    {
                        if (DateTime.Now - subDir.LastAccessTime >= TimeSpan.FromMinutes(30))
                        {
                            countDeletedFolders++;
                            sizeDeletedObjects += FolderSize(url + "\\" + subDir.Name);
                            subDir.Delete(true);
                        }    
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
                        {
                            countDeletedFiles++;
                            sizeDeletedObjects += subFile.Length;
                            subFile.Delete();
                        }   
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }

            }
            Console.WriteLine($"Освобождено: {sizeDeletedObjects} байт.\n" +
                $"Удалено {countDeletedFolders} папок и {countDeletedFiles} файлов.");
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
            Console.WriteLine("Task3");

            string url = GetCorrectUrl();
            Console.WriteLine($"Исходный размер папки: {FolderSize(url)}");
            DeleteUnusedFolders(url);
            Console.WriteLine($"Текущий размер папки: {FolderSize(url)}");
        }
    }
}
