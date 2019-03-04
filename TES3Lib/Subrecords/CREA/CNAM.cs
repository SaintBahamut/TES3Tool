using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.CREA
{
    /// <summary>
    /// Sound set creature uses 
    /// </summary>
    public class CNAM : Subrecord
    {
        public string SoundGen { get; set; }

        public CNAM()
        {

        }

        public CNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SoundGen = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}