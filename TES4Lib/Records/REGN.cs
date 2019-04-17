using TES4Lib.Base;
using TES4Lib.Subrecords.REGN;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class REGN : Record
    {
        public EDID EDID { get; set; }

        public ICON ICON { get; set; }

        public RCLR RCLR { get; set; }

        public WNAM WNAM { get; set; }

        public REGN(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}