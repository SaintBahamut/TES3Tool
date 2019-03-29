using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Creature sound data 2: chence to play
    /// </summary>
    public class CSDC : Subrecord
    {
        /// <summary>
        /// Chance to play sound 0 - 100
        /// </summary>
        public byte Chance { get; set; }

        public CSDC(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Chance = reader.ReadBytes<byte>(base.Data);
        }
    }
}
