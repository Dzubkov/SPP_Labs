using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace SPP_laba_2
{
    delegate void TaskDelegate(string from, string to);

    class Program
    {

        private static ThreadPool threadPool;
        private static string source_path;
        private static string aim_path;

        static void Main(string[] args)
        {
            Console.WriteLine("Введите исходный каталог");
            source_path = Console.ReadLine();

            Console.WriteLine("Введите целевой каталог");
            aim_path = Console.ReadLine();


            if (Directory.Exists(source_path) && Directory.Exists(aim_path))
            {
                threadPool = new ThreadPool(10);
                List<string> list = new List<string>();
                recursia(list, source_path);


                foreach (string file in list)
                {
                    threadPool.EnqueueTask(copyFile, new CopyFileInfo(file, aim_path));
                }
            }
            else
            {
                Console.WriteLine("Неверно введены каталоги");
            }

            while (threadPool.getTasks() != 0)
            {

            }
            Console.WriteLine($"Скопировано {threadPool.count} файлов");


            Console.ReadLine();
        }

        public static void recursia(List<string> list, string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach(string file in files)
            {
                list.Add(file);
            }
            string[] dirs = Directory.GetDirectories(path);
            foreach(string dir in dirs)
            {
                recursia(list, dir);
            }
        }

        public static void copyFile(string from, string to)
        {
            File.Copy(from, to, true);
            //   Console.WriteLine(Thread.CurrentThread.Name); 
            Thread.Sleep(1);
        }
    }
}
