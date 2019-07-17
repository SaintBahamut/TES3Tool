using System.Collections.Generic;
using TES3Lib.Subrecords.ARMO;
using TES3Lib.Subrecords.Shared;
using BNAM = TES3Lib.Subrecords.ARMO.BNAM;

namespace TES3Lib.Base
{
    public interface IEquipement
    {
        NAME NAME { get; set; }

        MODL MODL { get; set; }

        FNAM FNAM { get; set; }

        List<(INDX INDX, BNAM BNAM, CNAM CNAM)> BPSL { get; set; }

        ITEX ITEX { get; set; }
    }
}
