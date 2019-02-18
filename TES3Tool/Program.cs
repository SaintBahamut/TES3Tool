using System;
using static TES4Lib.TES4;
using static TES3Lib.TES3;
using static TES3Tool.TES4RecordConverter.Oblivion2Morrowind;
using static TES4Lib.Base.Group;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace TES3Tool
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
            string file = "D:\\Program Files\\Steam\\steamapps\\common\\Oblivion\\Data\\CONVERIX.ESP";
            string fileesm = "D:\\Program Files\\Steam\\steamapps\\common\\Oblivion\\Data\\Oblivion.ESM";


            var stat = TES4Load(fileesm, new List<string> { "STAT" });
           

            var tes4 = TES4Load(file, new List<string> {"CELL"});

            tes4.Groups.Add(stat.Groups.ToList()[0]);
          

           

            var test = ConvertInteriorCells(tes4);

            test.TES3Save("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Morrowind\\Data Files\\OUTPUT.esp"); //yep i use this kind of shitty path


            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
