using System.Collections.Generic;

namespace TES3Tool
{
    public static class Config
    {
        //positon shift
        public static int mwCellSize = 8192;
        public static int cellShiftX = 50;
        public static int cellShiftY = 50;

        public static string convertedRootFolder = "SI";

        //path for records
        public static string ARMOPath = "a";
        public static string MISCPath = "m";
        public static string STATPath = "s";
        public static string ACTIPath = "t";
        public static string KEYMPath = "k";
        public static string DOORPath = "d";
        public static string LIGHPath = "l";
        public static string CLOTPath = "c";
        public static string SOUNPath = "s";
        public static string FURNPath = "u";
        public static string WEAPPath = "w";
        public static string CONTPath = "o";
        public static string FLORPath = "f";
        public static string INGRPath = "i";
        public static string BOOKPath = "p";
        public static string ALCHPath = "h";
        public static string APPAPath = "h";

        public static string CREABiped = "r\\Skeleton.NIF\0";
        public static string CREAamphibious = "r\\swimmer.NIF\0";
        public static string CREAWeapon = "r\\Goblin01.NIF\0";
        public static string CREADefault = "r\\CaveMudcrab.nif\0";

        //maps to known equivements from both games
        public static Dictionary<string, string> OblivionMorrowindRecordsMap = new Dictionary<string, string>
        {
            {"0000000",string.Empty },
            {"0000000F","Gold_001\0" },
            {"0000000A","pick_journeyman_01\0" },
            {"00000002","TravelMarker\0" },
            {"0000000C","repair_journeyman_01\0" },
            {"00000006","TempleMarker\0" },
            {"00000003","NorthMarker\0" },
            {"00000001","DoorMarker\0" },
            {"00000005","DivineMarker\0" },
            {"0000000E","LootBag\0" },
            {"00000004","PrisonMarker\0" },
            {"0001EC8F","ingred_daedras_heart_01\0" },
            {"0001EBFF","ingred_bonemeal_01\0" },
            {"0001EBFE","ingred_ectoplasm_01\0" },
            {"0003366C","ingred_crab_meat_01\0" },
            {"00033675","ingred_fire_salts_01\0" },
            {"00022E5B","ingred_frost_salts_01\0" },
            {"0003369F","ingred_void_salts_01\0" },
            {"0003368F","ingred_rat_meat_01\0" },
            {"00009182","ingred_vampire_dust_01\0" },
            {"00023D89","ingred_bread_01\0" },
        };
    }
}
