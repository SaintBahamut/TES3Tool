using System.Collections.Generic;
using TES3Lib.Enums;
using TES3Lib.Enums.Flags;
using TES3Lib.Records;
using TES3Lib.Subrecords.ARMO;
using TES3Lib.Subrecords.BODY;
using TES3Lib.Subrecords.Shared;
using BNAM = TES3Lib.Subrecords.ARMO.BNAM;

namespace TES3Lib.Functions
{
    public static class Creators
    {
        public static BODY BodyPart(string editorId, string modelPath, bool isFemale, BodyPart bodyPart, BodyPartType type)
        {
            return new BODY
            {
                NAME = new NAME { EditorId = editorId },
                MODL = new MODL { ModelPath = modelPath },
                BYDT = new BYDT
                {
                    BodyPart = bodyPart,
                    IsVampire = 0,
                    Flags = new HashSet<BodyPartFlag>(),
                    PartType = type,
                }
            };
        }

        public static (INDX INDX, BNAM BNAM, CNAM CNAM) EquipementBodyPart(BodyPartSlot bodyPartSlot, string maleBodyPartId = "", string femaleBodyPartId = "")
        {
            return
            (
                new INDX { Type = bodyPartSlot },
                string.IsNullOrEmpty(maleBodyPartId) ? null : new BNAM { MalePartName = maleBodyPartId },
                string.IsNullOrEmpty(femaleBodyPartId) ? null : new CNAM { FemalePartName = femaleBodyPartId }
            );
        }
    }
}
