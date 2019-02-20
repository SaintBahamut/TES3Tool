using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.DOOR
{
    public class ANAM : Subrecord
    {
        /// <summary>
        /// Sound name of door close
        /// </summary>
        public string SoundNameClose { get; set; }

        public ANAM()
        {
            
        }

        public ANAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SoundNameClose = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
