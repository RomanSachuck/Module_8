using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    [Serializable]
    internal class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    internal class Program
    {
        static Student[] ReadValues(string studentDatabase)
        {
            
            if (File.Exists(studentDatabase))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (var fs = new FileStream(studentDatabase, FileMode.OpenOrCreate))
                {
                    return (Student[])formatter.Deserialize(fs);
                }
            }
            return null;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("FinalTask");
            var Students = ReadValues(@"C:\Users\79892\Desktop\Students.dat");

            Directory.CreateDirectory(@"C:\Users\79892\Desktop\Students");

            for (int i = 0; i < Students.Length; i++)
            {
                File.AppendAllText(@"C:\Users\79892\Desktop\Students" + "\\" + Students[i].Group, $"{Students[i].Name}, {Students[i].DateOfBirth}\n");    
            }
        }
    }
}
