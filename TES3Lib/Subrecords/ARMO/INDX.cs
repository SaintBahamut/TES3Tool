using System.Diagnostics;
using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;

namespace TES3Lib.Subrecords.ARMO
{
    /// <summary>
    /// Body Part Index (1 byte)
    /// </summary>
    [DebuggerDisplay("{Type}")]
    public class INDX : Subrecord
    {
        public BodyPartSlot Type { get; set; }

        public INDX()
        {
        }

        public INDX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Type = reader.ReadBytes<BodyPartSlot>(base.Data);
        }
    }
}
