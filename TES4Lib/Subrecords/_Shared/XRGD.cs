using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    public class XRGD : Subrecord
    {
        public byte[] RagDollData { get; set; }

        public XRGD(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            var RagDollData = reader.ReadBytes<byte[]>(base.Data, base.Size);    
        }
    }
}
