using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.STAT
{
    public class NAME : Subrecord
    {
        public string Name { get; set; }

        public NAME(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Name = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
