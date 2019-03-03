using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class KNAM : Subrecord
    {
        public string HairModel { get; set; }

        public KNAM()
        {

        }

        public KNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            HairModel = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}