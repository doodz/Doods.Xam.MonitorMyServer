using System;


namespace Doods.tool.ResxFromPoFile
{
    class Program
    {
      

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello doods!");
            //new PackageReference().Process();
            //new UpdatePoFiles().Process();
            //new UpdatePoFilesCockpit().Process();
            new UpdatePoFilesWebmin().Process();
        }

       
    }
}
