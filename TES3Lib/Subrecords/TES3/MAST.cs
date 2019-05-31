using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.TES3
{
    public class MAST : Subrecord
    {
        public string Filename { get; set; }

        public MAST()
        {
        }

        public MAST(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Filename = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
