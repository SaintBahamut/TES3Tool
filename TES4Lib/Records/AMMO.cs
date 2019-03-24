using TES4Lib.Base;
using TES4Lib.Subrecords.AMMO;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class AMMO : Record
    {
        public EDID EDID { get; set; }

        public FULL FULL { get; set; }

        public MODL MODL { get; set; }

        public MODB MODB { get; set; }

        public MODT MODT { get; set; }

        public ICON ICON { get; set; }

        public ENAM ENAM { get; set; }

        public ANAM ANAM { get; set; }

        public DATA DATA { get; set; }

        public AMMO(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}