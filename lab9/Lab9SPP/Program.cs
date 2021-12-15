using System;
using static System.Console;

namespace Lab9SPP
{
    class Program
    {
        static void Main()
        {
            DynamicList<string> list = new DynamicList<string>();
            list.Add("Привет");
            list.Add("Мир");
            list.Add("Это лаба 9");
            list.Add("по СПП");
            
            
            foreach(string s in list)
            {
                WriteLine(s);
            }
        }
    }
}
