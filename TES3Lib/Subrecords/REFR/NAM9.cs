using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    /// <summary>
    /// Its a mystery...\o/
    /// </summary>
    public class NAM9 : Subrecord
    {
        public int Unknown { get; set; }

        public NAM9()
        {

        }

        public NAM9(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Unknown = reader.ReadBytes<int>(base.Data);
        }
    }
}
