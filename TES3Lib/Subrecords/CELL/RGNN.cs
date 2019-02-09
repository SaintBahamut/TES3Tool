using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.CELL
{
    public class RGNN : Subrecord
    {
        /// <summary>
        /// Cells region name
        /// </summary>
        public string RegionName { get; set; }

        public RGNN(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Name = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
