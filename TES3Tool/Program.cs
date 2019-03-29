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
            //string file = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Morrowind\\Data Files\\minitest.ESP";
            //var tes3 = TES3Load(file);
            //tes3.TES3Save("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Morrowind\\Data Files\\minitest.ESP");

            ////string file = "D:\\Out.esp";
            //var tes3 = TES3Load(file);
            //tes3.TES3Save("D:\\Out.esp");

            //string file = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Morrowind\\Data Files\\Morrowind.esm";
            //var tes3 = TES3Load(file, new List<string> { "CELL" });
            //tes3.TES3Save("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Morrowind\\Data Files\\dd.esp");


            //string file = "D:\\Program Files\\Steam\\steamapps\\common\\Oblivion\\Data\\BOBOLIONTEST.esp";
            // string file = "D:\\Program Files\\Steam\\steamapps\\common\\Oblivion\\Data\\CONVERIX.ESP";


            string fileESM = "D:\\Program Files\\Steam\\steamapps\\common\\Oblivion\\Data\\Oblivion.ESM";
            //var stat = TES4Load(fileESM, new List<string> {
            //    "STAT", "CELL","SOUN", "MISC","KEYM","FURN","ACTI","LIGH","CONT","LVLI",
            //    "FLOR","DOOR","WEAP","INGR","BOOK","ENCH","ALCH","AMMO","APPA", "ARMO","CLOT"
            //});

            var stat = TES4Load(fileESM, new List<string> { "CREA","LVLC" });


            var test = ConvertInteriorCells(stat);
            //test.TES3Save("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Morrowind\\Data Files\\OUTPUT.esp"); //yep i use this kind of shitty path




            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
