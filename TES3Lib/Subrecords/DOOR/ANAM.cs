using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.DOOR
{
    public class ANAM : Subrecord
    {
  
        public string SoundNameOpen { get; set; }

        public ANAM()
        {
            
        }

        public ANAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SoundNameOpen = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
