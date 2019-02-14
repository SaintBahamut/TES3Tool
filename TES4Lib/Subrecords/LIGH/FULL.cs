using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.LIGH
{
    public class FULL : Subrecord
    {
        public string FullName { get; set; }

        public FULL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            FullName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
