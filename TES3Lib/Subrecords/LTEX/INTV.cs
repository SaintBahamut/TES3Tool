using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LTEX
{
    /// <summary>
    /// Not realy know, numebrs seem increment for next entries from 0
    /// </summary>
    public class INTV : Subrecord
    {
        public int IdNumber { get; set; }

        public INTV()
        {
        }

        public INTV(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            IdNumber = reader.ReadBytes<int>(base.Data);
        }
    }
}
