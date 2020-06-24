using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using TES3Lib;

namespace TES3Landgen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MW Land Load Test");
            MWLoadTest();
        }

        public static void MWLoadTest()
        {
            string fileESM = "C:\\Software\\Steam\\steamapps\\common\\Morrowind\\Data Files\\seWORLD.esp";
            //string BM = "C:\\Software\\Steam\\steamapps\\common\\Morrowind\\Data Files\\Bloodmoon.esm";
            var timer = Stopwatch.StartNew();
            TES3 tes3 = TES3.TES3Load(fileESM, new List<string> { "LAND","LTEX" });
            //TES3 bm = TES3.TES3Load(BM, new List<string> { "LAND","LTEX" });

            //tes3.TES3Save("C:/mapstuff/out.esp");

            //var heightmap = new TES3HeightMap(tes3);

            //var options = new ExportOptions() { HeightMap = true, VertexColorMap = false, TexturePlacementMap = false, ExportHeightAsRaw = true };
            //heightmap.ReadMapData(@"C:/mapstuff/output", options);


            //heightmap.ImportMapFromImage("C:/mapstuff/output");

            var test = new TES3HeightMap();
            test.ImportMapFromImage("C:/mapstuff/SEWorld", -50, -20);

            //var options2 = new ExportOptions() { HeightMap = true, VertexColorMap = false, TexturePlacementMap = false, ExportHeightAsRaw = false };
            //heightmap.ReadMapData(@"C:/mapstuff/output", options2);
            timer.Stop();

            Console.WriteLine($"Done in {timer.ElapsedMilliseconds} ms");
        }
    }
}
