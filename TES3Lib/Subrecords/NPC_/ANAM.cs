using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class ANAM : Subrecord
    {
        public string FactionName { get; set; }

        public ANAM()
        {

        }

        public ANAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            FactionName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
