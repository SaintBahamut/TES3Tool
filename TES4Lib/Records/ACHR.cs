using TES4Lib.Base;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class ACHR : Record
    {
        public EDID EDID { get; set; }

        public ACHR(byte[] rawData) : base(rawData)
        {
            IsImplemented = false;
            BuildSubrecords();
        }
    }
}