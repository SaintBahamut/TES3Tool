using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    public class DELE : Subrecord
    {
        public int Deleted { get; set; }

        public DELE()
        {

        }

        public DELE(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Deleted = reader.ReadBytes<int>(base.Data);
        }
    }
}
