using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.BOOK
{
    public class TEXT : Subrecord
    {
        public string BookText { get; set; }

        public TEXT()
        {

        }

        public TEXT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            BookText = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}