using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    public class TNAM : Subrecord
    {
        public string TrapName { get; set; }

        public TNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            TrapName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
