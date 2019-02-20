using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// Sound name
    /// </summary>
    public class SNAM : Subrecord
    {
        public string SoundName { get; set; }

        public SNAM()
        {

        }

        public SNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SoundName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
