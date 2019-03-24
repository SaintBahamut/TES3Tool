using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LEVC
{
    /// <summary>
    ///  Number of items in list
    /// </summary>
    public class INDX : Subrecord
    {
        public int CreatureCount { get; set; }

        public INDX()
        {
        }

        public INDX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            CreatureCount = reader.ReadBytes<int>(base.Data, base.Size);
        }
    }
}
