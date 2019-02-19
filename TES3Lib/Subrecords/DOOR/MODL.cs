using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.DOOR
{
    public class MODL : Subrecord
    {
        public string ModelPath { get; set; }

        public MODL()
        {
           
        }

        public MODL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ModelPath = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
