using System;
using System.Collections.Generic;
using TES3Lib;

namespace TES3Landgen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }


        public static void MWLoadTest()
        {
            string fileESM = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Morrowind\\Data Files\\Morrowind.esm";

            var tes3 = TES3.TES3Load(fileESM, new List<string> { "LAND" });
        }
    }
}
