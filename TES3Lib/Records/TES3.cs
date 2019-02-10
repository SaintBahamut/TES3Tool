using TES3Lib.Structures.Base;
using TES3Lib.Subrecords.TES3;

namespace TES3Lib.Records
{
    internal class TES3 : Record
    {
        public HEDR HEDR { get; set; }

        public MAST MAST { get; set; }

        public DATA DATA { get; set; }

        public TES3(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
