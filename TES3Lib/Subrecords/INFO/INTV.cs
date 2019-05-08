using System.Diagnostics;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.INFO
{
    /// <summary>
    /// Value to compare functions result
    /// </summary>
    [DebuggerDisplay("{ValueToCompare}")]
    public class INTV : Subrecord
    {
        public int ValueToCompare { get; set; }

        public INTV()
        {
        }

        public INTV(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ValueToCompare = reader.ReadBytes<int>(base.Data, base.Size);
        }
    }
}
