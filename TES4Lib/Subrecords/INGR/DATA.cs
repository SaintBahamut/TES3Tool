using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.INGR
{
    /// <summary>
    /// Display name of record
    /// </summary>
    public class DATA : Subrecord
    {
        /// <summary>
        /// Ingredient weight
        /// </summary>
        public float Weight { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Weight = reader.ReadBytes<float>(base.Data);
        }
    }
}
