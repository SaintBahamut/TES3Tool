using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    public class SCRI : Subrecord
    {
        public string ScriptName { get; set; }

        public SCRI()
        {

        }

        public SCRI(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ScriptName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
