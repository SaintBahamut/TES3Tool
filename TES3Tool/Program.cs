using System;
using static TES4Lib.TES4;
using static TES3Lib.TES3;

namespace ESMLab
{
    class Program
    {
        static void Main(string[] args)
        {
            //string file = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Morrowind\\Data Files\\TEST2.ESP";
            ////string file = "D:\\Out.esp";
            //var tes3 = TES3Load(file);
            //tes3.TES3Save("D:\\Out.esp");

            string file = "D:\\Program Files\\Steam\\steamapps\\common\\Oblivion\\Data\\oblivion.esm";
            var tes4 = TES4Load(file);


            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
