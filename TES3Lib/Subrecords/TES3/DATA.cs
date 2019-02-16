using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.TES3
{
    public class DATA : Subrecord
    {
        public long MasterDataSize { get; set; }

        public DATA()
        {

        }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MasterDataSize = reader.ReadBytes<long>(base.Data, Size);
        }
    }
}
