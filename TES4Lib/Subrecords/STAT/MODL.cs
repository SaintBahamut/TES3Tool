using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.STAT
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