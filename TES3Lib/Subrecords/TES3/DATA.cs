using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.TES3
{
    internal class DATA : Subrecord
    {
        public long Data { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Data = reader.ReadBytes<long>(base.Data, Size);
        }
    }
}
