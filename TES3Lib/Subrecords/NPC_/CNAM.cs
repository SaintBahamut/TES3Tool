using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class CNAM : Subrecord
    {
        public string ClassName { get; set; }

        public CNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ClassName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}