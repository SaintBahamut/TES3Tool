using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.SOUN
{
    /// <summary>
    /// Sound Data (3 bytes)
    /// </summary>
    public class DATA : Subrecord
    {
        public byte Volume { get; set; }
        public byte MinRange { get; set; }
        public byte MaxRange { get; set; }

        public DATA()
        {
        }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Volume = reader.ReadBytes<byte>(base.Data);
            MinRange = reader.ReadBytes<byte>(base.Data);
            MaxRange = reader.ReadBytes<byte>(base.Data);

        }
    }
}
