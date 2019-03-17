using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.INGR
{
    public class EFID : Subrecord
    {
        public string MagicEffectCode { get; set; }

        public EFID(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MagicEffectCode = reader.ReadBytes<string>(base.Data, 4);
        }
    }
}