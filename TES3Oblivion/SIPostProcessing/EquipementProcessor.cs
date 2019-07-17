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
            input.ITEX.IconPath = "si\\c\\SESylsDress.dds/0";
            input.MODL.ModelPath = "si\\c\\SESylsDressGND.nif/0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM,CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.Chest, null, "SESylsDressChestF"),
                Creators.EquipementBodyPart(BodyPartSlot.Neck, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.AnkleLeft, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.AnkleRight, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.WristLeft, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.WristRight, null, null)
            };
        }

        #region Knight of order
    
        public static void SEOrderKnightHelm(ref ARMO input)
        {
            input.ITEX.IconPath = "si\\c\\SEOrderKnightH.dds/0";
            input.MODL.ModelPath = "si\\c\\SEOrderKnightHGND.nif/0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.Head, "SEOrderKnightHeadM", null),
                Creators.EquipementBodyPart(BodyPartSlot.Neck, "SEOrderKnightNeckM", null),
            };
        }

        public static void SEOrderKnightCuirass(ref ARMO input)
        {
            input.ITEX.IconPath = "si\\c\\SEOrderKnightC.dds/0";
            input.MODL.ModelPath = "si\\c\\SEOrderKnightCGND.nif/0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.Chest, "SEOrderKnightChestM", null),            
            };
        }

        public static void SEOrderKnightGreaves(ref ARMO input)
        {
            input.ITEX.IconPath = "si\\c\\SEOrderKnightG.dds/0";
            input.MODL.ModelPath = "si\\c\\SEOrderKnightGGND.nif/0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.Groin, "SEOrderKnightGroinM", null),
                Creators.EquipementBodyPart(BodyPartSlot.UpperLegLeft, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.UpperLegRight, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.KneeLeft, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.KneeRight, null, null),
            };
        }

        public static void SEOrderKnightBoots(ref ARMO input)
        {
            input.ITEX.IconPath = "si\\c\\SEOrderKnightB.dds/0";
            input.MODL.ModelPath = "si\\c\\SEOrderKnightBGND.nif/0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.FootRight, "SEOrderKnight_foot_m", null),
                Creators.EquipementBodyPart(BodyPartSlot.FootLeft, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.AnkleLeft, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.AnkleRight, null, null),
            };
        }

        public static void SEOrderKnightGloveRight(ref ARMO input)
        {
            input.ITEX.IconPath = "si\\c\\SEOrderKnightG.dds/0";
            input.MODL.ModelPath = "si\\c\\SEOrderKnightGGND.nif/0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.HandRight, "SEOrderKnightHandM", null),
                Creators.EquipementBodyPart(BodyPartSlot.WristRight, null, null),
            };
        }

        public static void SEOrderKnightGloveLeft(ref ARMO input)
        {
            input.ITEX.IconPath = "si\\c\\SEOrderKnightG.dds/0";
            input.MODL.ModelPath = "si\\c\\SEOrderKnightGGND.nif/0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.HandLeft, "SEOrderKnightHandM", null),
                Creators.EquipementBodyPart(BodyPartSlot.WristLeft, null, null),
            };
        }

        public static void SEOrderKnightPauldronRight(ref ARMO input)
        {
            input.ITEX.IconPath = "si\\c\\SEOrderKnightP.dds/0";
            input.MODL.ModelPath = "si\\c\\SEOrderKnightPGND.nif/0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.PauldronRight, "SEOrderKnightPauldronM", null),
                Creators.EquipementBodyPart(BodyPartSlot.UpperArmRight, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.ForearmRight, null, null),
            };
        }

        public static void SEOrderKnightPauldronLeft(ref ARMO input)
        {
            input.ITEX.IconPath = "si\\c\\SEOrderKnightP.dds/0";
            input.MODL.ModelPath = "si\\c\\SEOrderKnightPGND.nif/0";

            input.BPSL = new List<(INDX INDX, BNAM BNAM, CNAM CNAM)>
            {
                Creators.EquipementBodyPart(BodyPartSlot.PauldronLeft, "SEOrderKnightPauldronM", null),
                Creators.EquipementBodyPart(BodyPartSlot.UpperArmLeft, null, null),
                Creators.EquipementBodyPart(BodyPartSlot.ForearmLeft, null, null),
            };
        }

        #endregion
    }
}
