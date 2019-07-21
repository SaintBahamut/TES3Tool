using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Enums;
using TES3Lib.Functions;
using TES3Lib.Records;
using TES3Lib.Subrecords.ARMO;

namespace TES3Oblivion.Records.SIPostProcessing.Definitions
{
    public static class EquipementProcessing
    {
        public static void SESylsDress(IEquipement input)
        {
            input.ITEX.IconPath = "si\\c\\SESylsDress.dds\0";
            input.MODL.ModelPath = "si\\c\\SESylsDressGND.nif\0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM,CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.Chest, null, "SESylsDressChestF\0"),
                Creators.EquipementBodyPart(BodyPartSlot.Neck, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.AnkleLeft, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.AnkleRight, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.WristLeft, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.WristRight, null, null)
            };

            (input as CLOT).CTDT.Type = ClothingType.Robe;
        }

        #region Knight of order
    
        public static void SEOrderKnightHelm(IEquipement input)
        {
            input.ITEX.IconPath = "si\\a\\SEOrderKnightH.dds\0";
            input.MODL.ModelPath = "si\\a\\SEOrderKnightHGND.nif\0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.Head, "SEOrderKnightHeadM\0", null),
                Creators.EquipementBodyPart(BodyPartSlot.Neck, "SEOrderKnightNeckM\0", null),
            };

            (input as ARMO).AODT.Type = ArmorType.Helmet;
        }

        public static void SEOrderKnightCuirass(IEquipement input)
        {
            input.ITEX.IconPath = "si\\a\\SEOrderKnightC.dds\0";
            input.MODL.ModelPath = "si\\a\\SEOrderKnightCGND.nif\0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.Chest, "SEOrderKnightChestM\0", null),            
            };

            (input as ARMO).AODT.Type = ArmorType.Cuirass;
        }

        public static void SEOrderKnightGreaves(IEquipement input)
        {
            input.ITEX.IconPath = "si\\a\\SEOrderKnightGR.dds\0";
            input.MODL.ModelPath = "si\\a\\SEOrderKnightGRGND.nif\0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.Groin, "SEOrderKnightGroinM\0", null),
                Creators.EquipementBodyPart(BodyPartSlot.UpperLegLeft, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.UpperLegRight, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.KneeLeft, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.KneeRight, null, null),
            };

            (input as ARMO).AODT.Type = ArmorType.Greaves;
        }

        public static void SEOrderKnightBoots(IEquipement input)
        {
            input.ITEX.IconPath = "si\\a\\SEOrderKnightB.dds\0";
            input.MODL.ModelPath = "si\\a\\SEOrderKnightBGND.nif\0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.FootRight, "SEOrderKnightFootM\0", null),
                Creators.EquipementBodyPart(BodyPartSlot.FootLeft, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.AnkleLeft, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.AnkleRight, null, null),
            };

            (input as ARMO).AODT.Type = ArmorType.Boots;
        }

        public static void SEOrderKnightGloveRight(IEquipement input)
        {
            input.ITEX.IconPath = "si\\a\\SEOrderKnightG.dds\0";
            input.MODL.ModelPath = "si\\a\\SEOrderKnightGGND.nif\0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.HandRight, "SEOrderKnightHandM\0", null),
                Creators.EquipementBodyPart(BodyPartSlot.WristRight, null, null),
            };

            (input as ARMO).AODT.Type = ArmorType.LGauntlet;
        }

        public static void SEOrderKnightGloveLeft(IEquipement input)
        {
            input.ITEX.IconPath = "si\\a\\SEOrderKnightG.dds\0";
            input.MODL.ModelPath = "si\\a\\SEOrderKnightGGND.nif\0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.HandLeft, "SEOrderKnightHandM\0", null),
                Creators.EquipementBodyPart(BodyPartSlot.WristLeft, null, null),
            };

            (input as ARMO).AODT.Type = ArmorType.RGauntlet;
        }

        public static void SEOrderKnightPauldronRight(IEquipement input)
        {
            input.ITEX.IconPath = "si\\a\\SEOrderKnightP.dds\0";
            input.MODL.ModelPath = "si\\a\\SEOrderKnightPGND.nif\0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.PauldronRight, "SEOrderKnightPauldronM\0", null),
                Creators.EquipementBodyPart(BodyPartSlot.UpperArmRight, "SEOrderKnightUpperArmM\0", null),
                Creators.EquipementBodyPart(BodyPartSlot.ForearmRight, null, null),
            };

            (input as ARMO).AODT.Type = ArmorType.RPauldron;
        }

        public static void SEOrderKnightPauldronLeft(IEquipement input)
        {
            input.ITEX.IconPath = "si\\a\\SEOrderKnightP.dds\0";
            input.MODL.ModelPath = "si\\a\\SEOrderKnightPGND.nif\0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.PauldronLeft, "SEOrderKnightPauldronM\0", null),
                Creators.EquipementBodyPart(BodyPartSlot.UpperArmLeft, "SEOrderKnightUpperArmM\0", null),
                Creators.EquipementBodyPart(BodyPartSlot.ForearmLeft, null, null),
            };

            (input as ARMO).AODT.Type = ArmorType.LPauldron;
        }

        #endregion
    }
}
