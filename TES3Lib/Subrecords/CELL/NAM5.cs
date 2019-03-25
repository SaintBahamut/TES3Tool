using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.CELL
{

    /// <summary>
    /// Exterior only
    /// </summary>
    public class NAM5 : Subrecord
    {
        public int MapColor { get; set; }

        public NAM5()
        {
        }

        public NAM5(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MapColor = reader.ReadBytes<int>(base.Data);
        }
    }
}
