using TES4Lib.Base;
using TES4Lib.Enums;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Creature sound data: sound type
    /// </summary>
    public class CSDT : Subrecord
    {
        /// <summary>
        /// Soound type
        /// </summary>
        public SoundType SoundType { get; set; }

        public CSDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SoundType = reader.ReadBytes<SoundType>(base.Data);
        }
    }
}
