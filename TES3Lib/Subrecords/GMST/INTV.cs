using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.GMTS
{
    /// <summary>
    /// Integer value (4 bytes)
    /// </summary>
    public class INTV : Subrecord
    {
        public int IntegerValue { get; set; }

        public INTV()
        {
        }

        public INTV(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            IntegerValue = reader.ReadBytes<int>(base.Data);
        }
    }
}