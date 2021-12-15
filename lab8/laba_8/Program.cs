using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace laba_8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь в dll файлу");
            string path = Console.ReadLine();
            if (!File.Exists(path))
            {
                Console.WriteLine("Неверный путь");
                Console.ReadLine();
                return;
            }

            string extension = Path.GetExtension(path);

            if (extension.Equals(".dll") || extension.Equals(".exe"))
            {
                Assembly asm = Assembly.LoadFrom(path);
                var types = asm.GetTypes();
            
                foreach (var type in types)
                {
                    if (type.IsPublic)
                    {
                        var attributes = type.GetCustomAttributes();
                        foreach (var attr in attributes)
                        {
                            if (attr.GetType().Equals(typeof(ExportClass)))
                            {
                                Console.WriteLine(type.FullName);
                                break;
                            }
                        }
                    }
                }

            }
            else
            {
                Console.WriteLine("У файла неверное расширение");
            }
            Console.ReadLine();
        }
    }

    [ExportClass]
    public class First {
    }

    public class Second {
    }

    [ExportClass]
    public class Third {
    }


}
