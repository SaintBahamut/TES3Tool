using System;
using static TES4Lib.TES4;
using static TES3Lib.TES3;
using System.Threading;

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

            //string file = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Morrowind\\Data Files\\Morrowind.esm";
            //var tes3 = TES3Load(file);

            //string file = "D:\\Program Files\\Steam\\steamapps\\common\\Oblivion\\Data\\BOBOLIONTEST.esp";
            //string file = "D:\\Program Files\\Steam\\steamapps\\common\\Oblivion\\Data\\CONVERIX.ESP";
            string file = "D:\\Program Files\\Steam\\steamapps\\common\\Oblivion\\Data\\Oblivion.ESM";
            var tes4 = TES4Load(file);




            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
