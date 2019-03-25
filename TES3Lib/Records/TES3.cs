using TES3Lib.Base;
using TES3Lib.Subrecords.TES3;

namespace TES3Lib.Records
{
    /// <summary>
    /// TES3 Header record
    /// </summary>
    public class TES3 : Record
    {
        public HEDR HEDR { get; set; }

        public MAST MAST { get; set; }

        public DATA DATA { get; set; }

        public TES3()
        {
        }

        public TES3(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
