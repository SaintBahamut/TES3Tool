using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.PGRD
{
    /// <summary>
    /// Unknown
    /// </summary>
    public class PGAG : Subrecord
    {
        public PGAG(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            byte shit = reader.ReadBytes<byte>(base.Data);
        }
    }
}
