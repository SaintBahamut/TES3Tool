using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LEVI
{
    /// <summary>
    ///  Number of items in list
    /// </summary>
    public class INDX : Subrecord
    {
        public int ItemCount { get; set; }

        public INDX()
        {
        }

        public INDX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ItemCount = reader.ReadBytes<int>(base.Data, base.Size);
        }
    }
}
