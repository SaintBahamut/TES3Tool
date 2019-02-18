using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class FNAM : Subrecord
    {
        public string NPCName { get; set; }

        public FNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            NPCName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}