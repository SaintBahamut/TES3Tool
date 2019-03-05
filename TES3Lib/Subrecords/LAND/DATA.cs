using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LAND
{
    /// <summary>
    /// Changing this value makes the land 'disappear' in the 
    /// </summary>
    public class DATA : Subrecord
    {
        public int Unknown { get; set; }

        public DATA()
        {
            Unknown = 0x09;
        }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Unknown = reader.ReadBytes<int>(base.Data);
        }
    }
}