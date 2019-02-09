using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class NAME: Subrecord
    {
        public string NPCId { get; set; }

        public NAME(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            NPCId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
