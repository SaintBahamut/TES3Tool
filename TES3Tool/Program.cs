using System;
using static TES3Lib.TES3;

namespace ESMLab
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Morrowind\\Data Files\\TEST2.ESP";
            //string file = "D:\\Out.esp";
            var tes3 = TES3Load(file);
            tes3.TES3Save("D:\\Out.esp");

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
