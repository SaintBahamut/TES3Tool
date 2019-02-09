using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class BNAM : Subrecord
    {
        public string HeadModel { get; set; }

        public BNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            HeadModel = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
