using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.LIGH
{
    public class MODL : Subrecord
    {
        public string ModelFileName { get; set; }

        public MODL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ModelFileName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}