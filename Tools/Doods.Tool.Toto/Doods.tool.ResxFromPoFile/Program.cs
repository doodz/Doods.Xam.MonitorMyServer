using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Doods.tool.ResxFromPoFile
{
    class Program
    {
      

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello doods!");
            new Class1().Process();
            new UpdatePoFiles().Process();

        }

       
    }
}
