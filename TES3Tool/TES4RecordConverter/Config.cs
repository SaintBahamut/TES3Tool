using System;
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
        public static Dictionary<string, string> OblivionMorrowindRecordsMap { get; private set; }

        public static Dictionary<string, List<string>> MWRaceHairs { get; private set; }

        public static Dictionary<string, List<string>> MWRaceFaces { get; private set; }


        static Config()
        {
            InitializeMWMappings();

            InitializeMWRacesDict();

            InitializeMWHairsDict();
        }

        static void InitializeMWMappings()
        {
            OblivionMorrowindRecordsMap = new Dictionary<string, string>
            {
                {"0000000", string.Empty },
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
                {"00023FE9","Argonian"},
                {"000224FC","Breton"},
                {"000191C1","Dark Elf"},
                {"00019204","High Elf"},
                {"00000907","Imperial"},
                {"000223C7","Khajiit"},
                {"000224FD","Nord"},
                {"000191C0","Orc"},
                {"00000D43","Redguard"},
                {"000223C8","Wood Elf"},
                {"00000019","Imperial"},//oblivion vampire
                //{"0005308E","Sheogorath"},
                //{"0001208F","Golden Saint"},
                //{"0001208E","Dark Seducer"},
                //{"00038010","Dremora" }
                {"00007E37","Battlemage\0"},
                {"000C792A","Guard\0"},
                {"00082DD2","Mage\0"},
                {"0006D676","Assassin\0"},
                {"00066211","Guard\0"},
                {"000651D1","Guard\0"},
                {"0018B124","Guard\0"},
                {"0005D2D1","Battlemage\0"},
                {"000982E6","Monk\0"},
                {"00093488","Blade\0"},
                {"00092D99","Mage\0"},
                {"0004B90A","Conjurer\0"},
                {"0003D7FC","Guard\0"},
                {"0003D06A","Thief Service\0"},
                {"00034521","Warrior\0"},
                {"00034520","Warrior\0"},
                {"0003451F","Mage\0"},
                {"0003451E","Archer\0"},
                {"0003451D","Warrior\0"},
                {"0003451C","Warlock\0 "},
                {"0003451B","Archer\0"},
                {"0003451A","Warrior\0"},
                {"000353E0","Necromancer\0"},
                {"00034E6E","Guard\0"},
                {"000294CD","Master-at-Arms\0"},
                {"00023F05","Agent\0"},
                {"00023F04","Thief\0"},
                {"00023F03","Nightblade\0"},
                {"00023EA1","Warrior\0"},
                {"00023E76","Mage\0"},
                {"00023E75","Smith\0"},
                {"00023E74","Trader Service\0"},
                {"00023E71","Scout\0"},
                {"00023E6E","Witch\0"},
                {"00023E6D","Warlock\0"},
                {"00023E6C","Sharpshooter\0"},
                {"00023E6B","Savant\0"},
                {"00023E6A","Publican\0"},
                {"00023E69","Pawnbroker\0"},
                {"00023E67","Pauper\0"},
                {"00023E63","Noble\0"},
                {"00023E61","Merchant\0"},
                {"00023E5F","Enchanter\0"},
                {"00023E5A","Drillmaster\0"},
                {"00023D9C","Hunter\0"},
                {"00023CCD","Herder\0"},
                {"00023CC8","Farmer\0"},
                {"00023C0E","Enforcer\0"},
                {"00023C0D","Commoner\0"},
                {"00023C0B","Champion\0"},
                {"00023C0A","Bookseller\0"},
                {"00023C09","Alchemist\0"},
                {"00023C08","Nightblade\0"},
                {"00023C07","Spellsword\0"},
                {"00023998","Witchhunter\0"},
                {"00023997","Battlemage\0"},
                {"000237AC","Sorcerer\0"},
                {"000237A9","Monk\0"},
                {"000237A8","Acrobat\0"},
                {"00023793","Assassin\0"},
                {"00023792","Agent\0"},
                {"0002378F","Thief\0"},
                {"0002378C","Rogue\0"},
                {"0002378B","Archer\0"},
                {"0002378A","Scout\0"},
                {"00023789","Crusader\0"},
                {"00023788","Barbarian\0"},
                {"00023787","Warrior\0"},
                {"00023785","Priest\0"},
                {"0001C824","Bard\0"},
                {"0001C823","Smith\0"},
                {"00024163","Trader\0"},
                {"00024162","Clothier\0"},
                {"000105D6","Mage\0"},
                {"00000832","Pilgrim\0"},
                {"00000836","Knight\0"},
                {"00000910","Healer\0"},
            };
        }

        static void InitializeMWRacesDict()
        {
            MWRaceHairs = new Dictionary<string, List<string>>();

            MWRaceHairs.Add("Argonian M", new List<string>());
            MWRaceHairs.Add("Argonian F", new List<string>());
            MWRaceHairs.Add("Khajiit M", new List<string>());
            MWRaceHairs.Add("Khajiit F", new List<string>());
            MWRaceHairs.Add("Orc M", new List<string>());
            MWRaceHairs.Add("Orc F", new List<string>());
            MWRaceHairs.Add("Imperial M", new List<string>());
            MWRaceHairs.Add("Imperial F", new List<string>());
            MWRaceHairs.Add("Nord M", new List<string>());
            MWRaceHairs.Add("Nord F", new List<string>());
            MWRaceHairs.Add("Breton M", new List<string>());
            MWRaceHairs.Add("Breton F", new List<string>());
            MWRaceHairs.Add("Redguard M", new List<string>());
            MWRaceHairs.Add("Redguard F", new List<string>());
            MWRaceHairs.Add("Dark Elf M", new List<string>());
            MWRaceHairs.Add("Dark Elf F", new List<string>());
            MWRaceHairs.Add("Wood Elf M", new List<string>());
            MWRaceHairs.Add("Wood Elf F", new List<string>());
            MWRaceHairs.Add("High Elf M", new List<string>());
            MWRaceHairs.Add("High Elf F", new List<string>());

            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_01\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_02\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_03\0");
            MWRaceHairs["Breton M"].Add("b_n_breton_m_hair_01\0");
            MWRaceHairs["Breton M"].Add("b_n_breton_m_hair_03\0");
            MWRaceHairs["Breton M"].Add("b_n_breton_m_hair_02\0");
            MWRaceHairs["Nord M"].Add("b_n_nord_m_hair03\0");
            MWRaceHairs["Nord M"].Add("b_n_nord_m_hair02\0");
            MWRaceHairs["Nord M"].Add("b_n_nord_m_hair01\0");
            MWRaceHairs["Nord M"].Add("b_n_nord_m_hair04\0");
            MWRaceHairs["Nord M"].Add("b_n_nord_m_hair05\0");
            MWRaceHairs["Khajiit M"].Add("b_n_khajiit_m_hair01\0");
            MWRaceHairs["Breton F"].Add("b_n_breton_f_hair_01\0");
            MWRaceHairs["Breton F"].Add("b_n_breton_f_hair_02\0");
            MWRaceHairs["Breton F"].Add("b_n_breton_f_hair_03\0");
            MWRaceHairs["Breton F"].Add("b_n_breton_f_hair_04\0");
            MWRaceHairs["Breton M"].Add("b_n_breton_m_hair_00\0");
            MWRaceHairs["High Elf F"].Add("b_n_high elf_f_hair_02\0");
            MWRaceHairs["High Elf F"].Add("b_n_high elf_f_hair_01\0");
            MWRaceHairs["High Elf F"].Add("b_n_high elf_f_hair_03\0");
            MWRaceHairs["Imperial F"].Add("b_n_imperial_f_hair_02\0");
            MWRaceHairs["Imperial F"].Add("b_n_imperial_f_hair_01\0");
            MWRaceHairs["Imperial F"].Add("b_n_imperial_f_hair_03\0");
            MWRaceHairs["Nord F"].Add("b_n_nord_f_hair_02\0");
            MWRaceHairs["Nord F"].Add("b_n_nord_f_hair_01\0");
            MWRaceHairs["Nord F"].Add("b_n_nord_f_hair_03\0");
            MWRaceHairs["Redguard F"].Add("b_n_redguard_f_hair_02\0");
            MWRaceHairs["Redguard F"].Add("b_n_redguard_f_hair_01\0");
            MWRaceHairs["Redguard F"].Add("b_n_redguard_f_hair_03\0");
            MWRaceHairs["Wood Elf F"].Add("b_n_wood elf_f_hair_02\0");
            MWRaceHairs["Wood Elf F"].Add("b_n_wood elf_f_hair_01\0");
            MWRaceHairs["Wood Elf F"].Add("b_n_wood elf_f_hair_03\0");
            MWRaceHairs["Argonian F"].Add("b_n_argonian_f_hair01\0");
            MWRaceHairs["Argonian M"].Add("b_n_argonian_m_hair01\0");
            MWRaceHairs["Khajiit F"].Add("b_n_khajiit_f_hair01\0");
            MWRaceHairs["High Elf M"].Add("b_n_high elf_m_hair_01\0");
            MWRaceHairs["High Elf M"].Add("b_n_high elf_m_hair_02\0");
            MWRaceHairs["High Elf M"].Add("b_n_high elf_m_hair_03\0");
            MWRaceHairs["Imperial M"].Add("b_n_imperial_m_hair_00\0");
            MWRaceHairs["Imperial M"].Add("b_n_imperial_m_hair_01\0");
            MWRaceHairs["Imperial M"].Add("b_n_imperial_m_hair_02\0");
            MWRaceHairs["Imperial M"].Add("b_n_imperial_m_hair_03\0");
            MWRaceHairs["Khajiit M"].Add("b_n_khajiit_m_hair02\0");
            MWRaceHairs["Redguard M"].Add("b_n_redguard_m_hair_00\0");
            MWRaceHairs["Redguard M"].Add("b_n_redguard_m_hair_01\0");
            MWRaceHairs["Redguard M"].Add("b_n_redguard_m_hair_02\0");
            MWRaceHairs["Wood Elf M"].Add("b_n_wood elf_m_hair_01\0");
            MWRaceHairs["Wood Elf M"].Add("b_n_wood elf_m_hair_02\0");
            MWRaceHairs["Wood Elf M"].Add("b_n_wood elf_m_hair_03\0");
            MWRaceHairs["Argonian F"].Add("b_n_argonian_f_hair02\0");
            MWRaceHairs["Argonian F"].Add("b_n_argonian_f_hair03\0");
            MWRaceHairs["Argonian F"].Add("b_n_argonian_f_hair04\0");
            MWRaceHairs["Argonian F"].Add("b_n_argonian_f_hair05\0");
            MWRaceHairs["Argonian M"].Add("b_n_argonian_m_hair02\0");
            MWRaceHairs["Argonian M"].Add("b_n_argonian_m_hair03\0");
            MWRaceHairs["Argonian M"].Add("b_n_argonian_m_hair04\0");
            MWRaceHairs["Argonian M"].Add("b_n_argonian_m_hair05\0");
            MWRaceHairs["Argonian M"].Add("b_n_argonian_m_hair06\0");
            MWRaceHairs["Breton F"].Add("b_n_breton_f_hair_05\0");
            MWRaceHairs["Breton M"].Add("b_n_breton_m_hair_04\0");
            MWRaceHairs["Breton M"].Add("b_n_breton_m_hair_05\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_01\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_02\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_03\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_04\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_05\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_06\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_07\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_08\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_09\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_10\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_11\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_12\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_13\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_14\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_15\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_16\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_17\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_18\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_19\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_20\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_21\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_22\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_23\0");
            MWRaceHairs["Dark Elf F"].Add("b_n_dark elf_f_hair_24\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_04\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_05\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_06\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_07\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_08\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_09\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_10\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_11\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_12\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_13\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_14\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_15\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_16\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_17\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_18\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_19\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_20\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_21\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_22\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_23\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_24\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_25\0");
            MWRaceHairs["Dark Elf M"].Add("b_n_dark elf_m_hair_26\0");
            MWRaceHairs["High Elf F"].Add("b_n_high elf_f_hair_04\0");
            MWRaceHairs["High Elf M"].Add("b_n_high elf_m_hair_04\0");
            MWRaceHairs["High Elf M"].Add("b_n_high elf_m_hair_05\0");
            MWRaceHairs["Imperial F"].Add("b_n_imperial_f_hair_04\0");
            MWRaceHairs["Imperial F"].Add("b_n_imperial_f_hair_05\0");
            MWRaceHairs["Imperial M"].Add("b_n_imperial_m_hair_04\0");
            MWRaceHairs["Imperial M"].Add("b_n_imperial_m_hair_05\0");
            MWRaceHairs["Khajiit F"].Add("b_n_khajiit_f_hair02\0");
            MWRaceHairs["Khajiit F"].Add("b_n_khajiit_f_hair03\0");
            MWRaceHairs["Khajiit F"].Add("b_n_khajiit_f_hair04\0");
            MWRaceHairs["Khajiit F"].Add("b_n_khajiit_f_hair05\0");
            MWRaceHairs["Khajiit M"].Add("b_n_khajiit_m_hair03\0");
            MWRaceHairs["Khajiit M"].Add("b_n_khajiit_m_hair04\0");
            MWRaceHairs["Khajiit M"].Add("b_n_khajiit_m_hair05\0");
            MWRaceHairs["Nord F"].Add("b_n_nord_f_hair_04\0");
            MWRaceHairs["Nord F"].Add("b_n_nord_f_hair_05\0");
            MWRaceHairs["Nord M"].Add("b_n_nord_m_hair00\0");
            MWRaceHairs["Orc F"].Add("b_n_orc_f_hair01\0");
            MWRaceHairs["Orc F"].Add("b_n_orc_f_hair02\0");
            MWRaceHairs["Orc F"].Add("b_n_orc_f_hair03\0");
            MWRaceHairs["Orc F"].Add("b_n_orc_f_hair04\0");
            MWRaceHairs["Orc F"].Add("b_n_orc_f_hair05\0");
            MWRaceHairs["Orc M"].Add("b_n_orc_m_hair_01\0");
            MWRaceHairs["Orc M"].Add("b_n_orc_m_hair_02\0");
            MWRaceHairs["Orc M"].Add("b_n_orc_m_hair_03\0");
            MWRaceHairs["Orc M"].Add("b_n_orc_m_hair_04\0");
            MWRaceHairs["Orc M"].Add("b_n_orc_m_hair_05\0");
            MWRaceHairs["Redguard F"].Add("b_n_redguard_f_hair_04\0");
            MWRaceHairs["Redguard F"].Add("b_n_redguard_f_hair_05\0");
            MWRaceHairs["Redguard M"].Add("b_n_redguard_m_hair_03\0");
            MWRaceHairs["Redguard M"].Add("b_n_redguard_m_hair_04\0");
            MWRaceHairs["Redguard M"].Add("b_n_redguard_m_hair_05\0");
            MWRaceHairs["Wood Elf F"].Add("b_n_wood elf_f_hair_04\0");
            MWRaceHairs["Wood Elf F"].Add("b_n_wood elf_f_hair_05\0");
            MWRaceHairs["Wood Elf M"].Add("b_n_wood elf_m_hair_04\0");
            MWRaceHairs["Wood Elf M"].Add("b_n_wood elf_m_hair_05\0");
            MWRaceHairs["Wood Elf M"].Add("b_n_wood elf_m_hair_06\0");
            MWRaceHairs["Redguard M"].Add("B_N_Redguard_M_Hair_06\0");
            MWRaceHairs["Nord M"].Add("B_N_Nord_M_Hair07\0");
            MWRaceHairs["Nord M"].Add("B_N_Nord_M_Hair06\0");
            MWRaceHairs["Imperial F"].Add("B_N_Imperial_F_Hair_07\0");
            MWRaceHairs["Imperial F"].Add("B_N_Imperial_F_Hair_06\0");
            MWRaceHairs["Imperial M"].Add("B_N_Imperial_M_Hair_09\0");
            MWRaceHairs["Imperial M"].Add("B_N_Imperial_M_Hair_08\0");
            MWRaceHairs["Imperial M"].Add("B_N_Imperial_M_Hair_07\0");
            MWRaceHairs["Imperial M"].Add("B_N_Imperial_M_Hair_06\0");
        }

        static void InitializeMWHairsDict()
        {
            MWRaceFaces = new Dictionary<string, List<string>>();

            MWRaceFaces.Add("Argonian M", new List<string>());
            MWRaceFaces.Add("Argonian F", new List<string>());
            MWRaceFaces.Add("Khajiit M", new List<string>());
            MWRaceFaces.Add("Khajiit F", new List<string>());
            MWRaceFaces.Add("Orc M", new List<string>());
            MWRaceFaces.Add("Orc F", new List<string>());
            MWRaceFaces.Add("Imperial M", new List<string>());
            MWRaceFaces.Add("Imperial F", new List<string>());
            MWRaceFaces.Add("Nord M", new List<string>());
            MWRaceFaces.Add("Nord F", new List<string>());
            MWRaceFaces.Add("Breton M", new List<string>());
            MWRaceFaces.Add("Breton F", new List<string>());
            MWRaceFaces.Add("Redguard M", new List<string>());
            MWRaceFaces.Add("Redguard F", new List<string>());
            MWRaceFaces.Add("Dark Elf M", new List<string>());
            MWRaceFaces.Add("Dark Elf F", new List<string>());
            MWRaceFaces.Add("Wood Elf M", new List<string>());
            MWRaceFaces.Add("Wood Elf F", new List<string>());
            MWRaceFaces.Add("High Elf M", new List<string>());
            MWRaceFaces.Add("High Elf F", new List<string>());

            MWRaceFaces["Breton M"].Add("b_n_breton_m_head_01\0");
            MWRaceFaces["Dark Elf M"].Add("b_n_dark elf_m_head_01\0");
            MWRaceFaces["Dark Elf M"].Add("b_n_dark elf_m_head_02\0");
            MWRaceFaces["Dark Elf M"].Add("b_n_dark elf_m_head_03\0");
            MWRaceFaces["Dark Elf M"].Add("b_n_dark elf_m_head_04\0");
            MWRaceFaces["High Elf F"].Add("b_n_high elf_f_head_02\0");
            MWRaceFaces["High Elf M"].Add("b_n_high elf_m_head_01\0");
            MWRaceFaces["Imperial M"].Add("b_n_imperial_m_head_01\0");
            MWRaceFaces["Redguard M"].Add("b_n_redguard_m_head_01\0");
            MWRaceFaces["Wood Elf M"].Add("b_n_wood elf_m_head_01\0");
            MWRaceFaces["Dark Elf F"].Add("b_n_dark elf_f_head_01\0");
            MWRaceFaces["Nord F"].Add("b_n_nord_f_head_01\0");
            MWRaceFaces["Redguard F"].Add("b_n_redguard_f_head_01\0");
            MWRaceFaces["Wood Elf F"].Add("b_n_wood elf_f_head_01\0");
            MWRaceFaces["Imperial F"].Add("b_n_imperial_f_head_01\0");
            MWRaceFaces["Khajiit M"].Add("b_n_khajiit_m_head_01\0");
            MWRaceFaces["Breton F"].Add("b_n_breton_f_head_01\0");
            MWRaceFaces["Breton F"].Add("b_n_breton_f_head_02\0");
            MWRaceFaces["Breton F"].Add("b_n_breton_f_head_03\0");
            MWRaceFaces["Breton M"].Add("b_n_breton_m_head_02\0");
            MWRaceFaces["Breton M"].Add("b_n_breton_m_head_03\0");
            MWRaceFaces["High Elf F"].Add("b_n_high elf_f_head_01\0");
            MWRaceFaces["High Elf F"].Add("b_n_high elf_f_head_03\0");
            MWRaceFaces["High Elf M"].Add("b_n_high elf_m_head_02\0");
            MWRaceFaces["High Elf M"].Add("b_n_high elf_m_head_03\0");
            MWRaceFaces["Imperial F"].Add("b_n_imperial_f_head_02\0");
            MWRaceFaces["Imperial F"].Add("b_n_imperial_f_head_03\0");
            MWRaceFaces["Imperial M"].Add("b_n_imperial_m_head_02\0");
            MWRaceFaces["Imperial M"].Add("b_n_imperial_m_head_03\0");
            MWRaceFaces["Nord F"].Add("b_n_nord_f_head_02\0");
            MWRaceFaces["Nord F"].Add("b_n_nord_f_head_03\0");
            MWRaceFaces["Nord M"].Add("b_n_nord_m_head_03\0");
            MWRaceFaces["Nord M"].Add("b_n_nord_m_head_04\0");
            MWRaceFaces["Redguard F"].Add("b_n_redguard_f_head_02\0");
            MWRaceFaces["Redguard F"].Add("b_n_redguard_f_head_03\0");
            MWRaceFaces["Redguard M"].Add("b_n_redguard_m_head_03\0");
            MWRaceFaces["Redguard M"].Add("b_n_redguard_m_head_02\0");
            MWRaceFaces["Redguard M"].Add("b_n_redguard_m_head_04\0");
            MWRaceFaces["Redguard M"].Add("b_n_redguard_m_head_05\0");
            MWRaceFaces["Wood Elf F"].Add("b_n_wood elf_f_head_03\0");
            MWRaceFaces["Wood Elf F"].Add("b_n_wood elf_f_head_02\0");
            MWRaceFaces["Wood Elf F"].Add("b_n_wood elf_f_head_04\0");
            MWRaceFaces["Wood Elf M"].Add("b_n_wood elf_m_head_03\0");
            MWRaceFaces["Wood Elf M"].Add("b_n_wood elf_m_head_02\0");
            MWRaceFaces["Wood Elf M"].Add("b_n_wood elf_m_head_04\0");
            MWRaceFaces["Wood Elf M"].Add("b_n_wood elf_m_head_05\0");
            MWRaceFaces["Argonian M"].Add("b_n_argonian_m_head_01\0");
            MWRaceFaces["Khajiit F"].Add("b_n_khajiit_f_head_01\0");
            MWRaceFaces["Argonian F"].Add("b_n_argonian_f_head_01\0");
            MWRaceFaces["Orc F"].Add("b_n_orc_f_head_01\0");
            MWRaceFaces["Orc M"].Add("b_n_orc_m_head_01\0");
            MWRaceFaces["Orc M"].Add("b_n_orc_m_head_02\0");
            MWRaceFaces["Orc M"].Add("b_n_orc_m_head_03\0");
            MWRaceFaces["Argonian F"].Add("b_n_argonian_f_head_02\0");
            MWRaceFaces["Argonian F"].Add("b_n_argonian_f_head_03\0");
            MWRaceFaces["Argonian M"].Add("b_n_argonian_m_head_02\0");
            MWRaceFaces["Argonian M"].Add("b_n_argonian_m_head_03\0");
            MWRaceFaces["Breton F"].Add("b_n_breton_f_head_04\0");
            MWRaceFaces["Breton F"].Add("b_n_breton_f_head_05\0");
            MWRaceFaces["Breton M"].Add("b_n_breton_m_head_04\0");
            MWRaceFaces["Breton M"].Add("b_n_breton_m_head_05\0");
            MWRaceFaces["Breton M"].Add("b_n_breton_m_head_06\0");
            MWRaceFaces["Dark Elf F"].Add("b_n_dark elf_f_head_02\0");
            MWRaceFaces["Dark Elf F"].Add("b_n_dark elf_f_head_03\0");
            MWRaceFaces["Dark Elf F"].Add("b_n_dark elf_f_head_04\0");
            MWRaceFaces["Dark Elf F"].Add("b_n_dark elf_f_head_05\0");
            MWRaceFaces["Dark Elf F"].Add("b_n_dark elf_f_head_06\0");
            MWRaceFaces["Dark Elf F"].Add("b_n_dark elf_f_head_07\0");
            MWRaceFaces["Dark Elf F"].Add("b_n_dark elf_f_head_08\0");
            MWRaceFaces["Dark Elf F"].Add("b_n_dark elf_f_head_09\0");
            MWRaceFaces["Dark Elf F"].Add("b_n_dark elf_f_head_10\0");
            MWRaceFaces["Dark Elf M"].Add("b_n_dark elf_m_head_05\0");
            MWRaceFaces["Dark Elf M"].Add("b_n_dark elf_m_head_06\0");
            MWRaceFaces["Dark Elf M"].Add("b_n_dark elf_m_head_07\0");
            MWRaceFaces["Dark Elf M"].Add("b_n_dark elf_m_head_08\0");
            MWRaceFaces["Dark Elf M"].Add("b_n_dark elf_m_head_09\0");
            MWRaceFaces["Dark Elf M"].Add("b_n_dark elf_m_head_10\0");
            MWRaceFaces["Dark Elf M"].Add("b_n_dark elf_m_head_11\0");
            MWRaceFaces["Dark Elf M"].Add("b_n_dark elf_m_head_12\0");
            MWRaceFaces["High Elf F"].Add("b_n_high elf_f_head_04\0");
            MWRaceFaces["High Elf F"].Add("b_n_high elf_f_head_05\0");
            MWRaceFaces["High Elf M"].Add("b_n_high elf_m_head_04\0");
            MWRaceFaces["High Elf M"].Add("b_n_high elf_m_head_05\0");
            MWRaceFaces["Imperial F"].Add("b_n_imperial_f_head_04\0");
            MWRaceFaces["Imperial F"].Add("b_n_imperial_f_head_05\0");
            MWRaceFaces["Imperial M"].Add("b_n_imperial_m_head_04\0");
            MWRaceFaces["Imperial M"].Add("b_n_imperial_m_head_05\0");
            MWRaceFaces["Khajiit F"].Add("b_n_khajiit_f_head_02\0");
            MWRaceFaces["Khajiit F"].Add("b_n_khajiit_f_head_03\0");
            MWRaceFaces["Khajiit M"].Add("b_n_khajiit_m_head_02\0");
            MWRaceFaces["Khajiit M"].Add("b_n_khajiit_m_head_03\0");
            MWRaceFaces["Nord F"].Add("b_n_nord_f_head_04\0");
            MWRaceFaces["Nord F"].Add("b_n_nord_f_head_05\0");
            MWRaceFaces["Nord M"].Add("b_n_nord_m_head_01\0");
            MWRaceFaces["Nord M"].Add("b_n_nord_m_head_02\0");
            MWRaceFaces["Nord M"].Add("b_n_nord_m_head_05\0");
            MWRaceFaces["Redguard F"].Add("b_n_redguard_f_head_04\0");
            MWRaceFaces["Redguard F"].Add("b_n_redguard_f_head_05\0");
            MWRaceFaces["Wood Elf F"].Add("b_n_wood elf_f_head_05\0");
            MWRaceFaces["Orc F"].Add("b_n_orc_f_head_02\0");
            MWRaceFaces["Orc F"].Add("b_n_orc_f_head_03\0");
            MWRaceFaces["Dark Elf M"].Add("b_n_dark elf_m_head_16\0");
            MWRaceFaces["Dark Elf M"].Add("b_n_dark elf_m_head_15\0");
            MWRaceFaces["Dark Elf M"].Add("b_n_dark elf_m_head_14\0");
            MWRaceFaces["Dark Elf M"].Add("b_n_dark elf_m_head_13\0");
            MWRaceFaces["Wood Elf M"].Add("B_N_Wood Elf_M_Head_08\0");
            MWRaceFaces["Wood Elf M"].Add("B_N_Wood Elf_M_Head_07\0");
            MWRaceFaces["Redguard M"].Add("B_N_Redguard_M_Head_06\0");
            MWRaceFaces["Dark Elf M"].Add("B_N_Dark Elf_M_Head_17\0");
            MWRaceFaces["Orc M"].Add("B_N_Orc_M_Head_04\0");
            MWRaceFaces["Breton M"].Add("B_N_Breton_M_Head_08\0");
            MWRaceFaces["Breton M"].Add("B_N_Breton_M_Head_07\0");
            MWRaceFaces["Nord F"].Add("B_N_Nord_F_Head_08\0");
            MWRaceFaces["Breton F"].Add("B_N_Breton_F_Head_06\0");
            MWRaceFaces["High Elf M"].Add("B_N_High Elf_M_Head_06\0");
            MWRaceFaces["High Elf F"].Add("B_N_High Elf_F_Head_06\0");
            MWRaceFaces["Wood Elf F"].Add("B_N_Wood Elf_F_Head_06\0");
            MWRaceFaces["Wood Elf M"].Add("B_N_Wood Elf_M_Head_06\0");
            MWRaceFaces["Redguard F"].Add("B_N_Redguard_F_Head_06\0");
            MWRaceFaces["Nord M"].Add("B_N_Nord_M_Head_06\0");
            MWRaceFaces["Imperial M"].Add("B_N_Imperial_M_Head_07\0");
            MWRaceFaces["Imperial M"].Add("B_N_Imperial_M_Head_06\0");
            MWRaceFaces["Imperial F"].Add("B_N_Imperial_F_Head_07\0");
            MWRaceFaces["Imperial F"].Add("B_N_Imperial_F_Head_06\0");
            MWRaceFaces["Nord F"].Add("B_N_Nord_F_Head_07\0");
            MWRaceFaces["Nord F"].Add("B_N_Nord_F_Head_06\0");
            MWRaceFaces["Khajiit F"].Add("B_N_Khajiit_F_Head_04\0");
            MWRaceFaces["Nord M"].Add("B_N_Nord_M_Head_08\0");
            MWRaceFaces["Nord M"].Add("B_N_Nord_M_Head_07\0");
            MWRaceFaces["Khajiit M"].Add("B_N_Khajiit_M_Head_04\0");
        }
    }
}
