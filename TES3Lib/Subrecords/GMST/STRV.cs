using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.GMTS
{
    /// <summary>
    /// String Value
    /// </summary>
    public class STRV : Subrecord
    {
        public string StringValue { get; set; }

        public STRV()
        {
        }

        public STRV(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            StringValue = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
