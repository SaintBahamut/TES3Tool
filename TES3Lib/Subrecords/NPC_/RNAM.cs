using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class RNAM : Subrecord
    {
        public string RaceName { get; set; }

        public RNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            RaceName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
