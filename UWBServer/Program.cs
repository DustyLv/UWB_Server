using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWBServer
{
    class Program
    {
        static void Main(string[] args)     
        {
            Console.Title = "UWB Server";

            Server.Start(50, 26950);

            Console.ReadKey();
        }
    }
}
