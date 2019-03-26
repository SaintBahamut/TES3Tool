using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Creature attack reach
    /// </summary>
    public class RNAM : Subrecord
    {
        public byte AttackReach { get; set; }

        public RNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            AttackReach = reader.ReadBytes<byte>(base.Data);
        }
    }
}
