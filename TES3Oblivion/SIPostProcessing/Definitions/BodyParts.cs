using System.Collections.Generic;
using System.Reflection;
using TES3Lib.Enums;
using TES3Lib.Functions;
using TES3Lib.Records;

namespace TES3Oblivion.SIPostProcessing.Definitions
{
    public static class BodyParts
    {      
        static List<BODY> SESylsDress = new List<BODY>
        {
            Creators.BodyPart("SESylsDressChestF\0","si\\c\\SESylsDressF.nif\0",true,BodyPart.Chest,BodyPartType.Clothing)
        };

        static List<BODY> SEOrderKnightArmor = new List<BODY>
        {
            Creators.BodyPart("SEOrderKnightHeadM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Head,BodyPartType.Armor),
            Creators.BodyPart("SEOrderKnightNeckM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Neck,BodyPartType.Armor),
            Creators.BodyPart("SEOrderKnightChestM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Chest,BodyPartType.Armor),
            Creators.BodyPart("SEOrderKnightHandM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Hand,BodyPartType.Armor),
            Creators.BodyPart("SEOrderKnightHandM1st\0","si\\a\\SEOrderKnight1st.nif\0",false,BodyPart.Hand,BodyPartType.Armor),
            Creators.BodyPart("SEOrderKnightGroinM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Groin,BodyPartType.Armor),
            Creators.BodyPart("SEOrderKnightPauldronM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Clavicle,BodyPartType.Armor),
            Creators.BodyPart("SEOrderKnightUpperArmM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Upperarm,BodyPartType.Armor),
            Creators.BodyPart("SEOrderKnightFootM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Foot,BodyPartType.Armor),
        };

        //static List<BODY> SEDementiaUpper = new List<BODY>
        //{
        //    Creators.BodyPart("SEOrderKnightHeadM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Head,BodyPartType.Clothing),
        //    Creators.BodyPart("SEOrderKnightNeckM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Neck,BodyPartType.Clothing),
        //    Creators.BodyPart("SEOrderKnightChestM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Chest,BodyPartType.Clothing),
        //    Creators.BodyPart("SEOrderKnightHandM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Hand,BodyPartType.Clothing),
        //    Creators.BodyPart("SEOrderKnightHandM1st\0","si\\a\\SEOrderKnight1st.nif\0",false,BodyPart.Hand,BodyPartType.Clothing),
        //    Creators.BodyPart("SEOrderKnightGroinM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Groin,BodyPartType.Clothing),
        //    Creators.BodyPart("SEOrderKnightPauldronM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Clavicle,BodyPartType.Clothing),
        //    Creators.BodyPart("SEOrderKnightUpperArmM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Upperarm,BodyPartType.Clothing),
        //    Creators.BodyPart("SEOrderKnightFootM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Foot,BodyPartType.Clothing),
        //};

        static List<BODY> SEManiaUpper = new List<BODY>
        {
            Creators.BodyPart("SEManiaUpperChestM\0","si\\c\\SEManiaUpperM.nif\0",true,BodyPart.Chest,BodyPartType.Clothing),
            Creators.BodyPart("SEManiaUpperGroinM\0","si\\c\\SEManiaUpperM.nif\0",true,BodyPart.Groin,BodyPartType.Clothing),
            Creators.BodyPart("SEManiaUpperUpperArmM\0","si\\c\\SEManiaUpperM.nif\0",true,BodyPart.Upperarm,BodyPartType.Clothing),
            Creators.BodyPart("SEManiaUpperFootM\0","si\\c\\SEManiaUpperM.nif\0",true,BodyPart.Foot,BodyPartType.Clothing),
            Creators.BodyPart("SEManiaUpperChestF\0","si\\c\\SEManiaUpperF.nif\0",true,BodyPart.Chest,BodyPartType.Clothing),
            Creators.BodyPart("SEManiaUpperGroinF\0","si\\c\\SEManiaUpperF.nif\0",true,BodyPart.Groin,BodyPartType.Clothing),
            Creators.BodyPart("SEManiaUpperUpperArmF\0","si\\c\\SEManiaUpperF.nif\0",true,BodyPart.Upperarm,BodyPartType.Clothing),
            Creators.BodyPart("SEManiaUpperFootF\0","si\\c\\SEManiaUpperF.nif\0",true,BodyPart.Foot,BodyPartType.Clothing),           
        };

        public static List<BODY> GetListOfBodyParts()
        {
            List<BODY> output = new List<BODY>();
            FieldInfo[] fields = typeof(BodyParts).GetFields(BindingFlags.NonPublic | BindingFlags.Static);

            foreach (var bodyPartSet in fields)
            {
                output.AddRange(bodyPartSet.GetValue(null) as List<BODY>);
            }

            return output;
        }
    }
}
