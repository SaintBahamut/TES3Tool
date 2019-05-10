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
            //ConvertSI();

            OblivionLoadTest();

            // MWLoadTest();



            Console.WriteLine("Done");
            Console.ReadLine();
        }

        public static void ConvertSI()
        {
            string fileESM = "D:\\Program Files\\Steam\\steamapps\\common\\Oblivion\\Data\\Oblivion.esm";

            var stat = TES4Load(fileESM, new List<string> {
                "STAT","WRLD","SOUN", "MISC","KEYM","FURN","ACTI","LIGH","CONT","LVLC","LVLI","CELL","NPC_",
                "FLOR","WEAP","INGR","BOOK","ENCH","ALCH","AMMO","APPA", "ARMO","CLOT","CREA","DOOR","FACT"
            });
            var testEX = ConvertInteriorsAndExteriors(stat);
            testEX.TES3Save("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Morrowind\\Data Files\\SI.esp");
        }

        public static void MWLoadTest()
        {
            string fileESM = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Morrowind\\Data Files\\minigrid.esp";

            var tes3 = TES3Load(fileESM, new List<string> { "CELL", "PGRD" });

            tes3.TES3Save("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Morrowind\\Data Files\\minigridx.esp");
        }

        public static void OblivionLoadTest()
        {
            string file = "D:\\Program Files\\Steam\\steamapps\\common\\Oblivion\\Data\\Oblivion.esm";
            var tes4 = TES4Load(file, new List<string> { "FACT" });
        }
    }
}
