using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LTEX
{
    public class DATA : Subrecord
    {
        public string FileName { get; set; }

        public DATA()
        {
        }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            FileName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}