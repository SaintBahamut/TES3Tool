using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.INGR
{
    /// <summary>
    /// This subrecord is always 8 bytes long and holds the ingredient enchantment data.
    /// </summary>
    public class ENIT : Subrecord
    {
        /// <summary>
        /// Ingredient value used for both auto-calc and manual.
        /// </summary>
        public int Value { get; set; }

        /// <summary> 
        /// 0x00000001 = Manual Value(Auto-Calc Off)
        /// 0x00000002 = Food Item
        ///0xCDCDCD00 = Default Value
        /// </summary>
        public int Flags { get; set; }

        public ENIT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Value = reader.ReadBytes<int>(base.Data);
            Flags = reader.ReadBytes<int>(base.Data);
        }
    }
}
