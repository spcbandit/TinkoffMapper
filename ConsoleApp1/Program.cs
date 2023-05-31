
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using TinkoffMapper;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var dic = Versions.CheckDependency(out var error);
                if (dic)
                {
                    Console.WriteLine("Все ок");
                }
                else { Console.WriteLine(error); }
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            };
            Console.ReadKey();
        }
    }
}
