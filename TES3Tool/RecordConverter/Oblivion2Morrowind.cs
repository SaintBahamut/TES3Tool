using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMLab.RecordConverter
{
    public static class Oblivion2Morrowind
    {
        static TES3Lib.Records.LIGH ConvertLIGH(TES4Lib.Records.LIGH obLIGH)
        {
            return null;
        }

        static TES3Lib.Records.STAT ConvertREF(TES4Lib.Records.STAT obSTAT)
        {
            return new TES3Lib.Records.STAT()
            {
                Flags = 0,
                Header = 0,
                MODL = new TES3Lib.Subrecords.STAT.MODL(obSTAT.MODL.ModelFileName),
                NAME = new TES3Lib.Subrecords.STAT.NAME(obSTAT.EDID.EditorId),
            };
        }

        static TES3Lib.Records.CELL ConvertCELL(TES4Lib.Records.CELL obLIGH)
        {
            return null;
        }

        static TES3Lib.Records.REFR ConvertREFR(TES4Lib.Records.REFR obLIGH)
        {
            return null;
        }

      
    }
}
