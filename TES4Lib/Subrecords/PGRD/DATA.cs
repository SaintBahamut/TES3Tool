using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.PGRD
{

    public class DATA : Subrecord
    {
        public short NumberOfNodes { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            NumberOfNodes = reader.ReadBytes<short>(this.Data);
        }
    }
}
