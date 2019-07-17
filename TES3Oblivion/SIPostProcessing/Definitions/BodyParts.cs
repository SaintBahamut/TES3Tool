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
            Creators.BodyPart("SEOrderKnightGroinM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Groin,BodyPartType.Armor),
            Creators.BodyPart("SEOrderKnightPauldronM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Clavicle,BodyPartType.Armor),
            Creators.BodyPart("SEOrderKnightFootM\0","si\\a\\SEOrderKnight.nif\0",false,BodyPart.Foot,BodyPartType.Armor),
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
