using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.RACE
{
    /// <summary>
    /// Default Hair Color
    /// </summary>
    public class CNAM : Subrecord
    {
        /// <summary>
        /// Index of hair color
        /// </summary>
        public byte DefaultHairColor { get; set; }

        public CNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            DefaultHairColor = reader.ReadBytes<byte>(base.Data);
        }
    }
}
