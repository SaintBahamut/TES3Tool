using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.RACE
{
    /// <summary>
    /// Voices used by race
    /// </summary>
    public class VNAM : Subrecord
    {
        public string MaleVoice { get; set; }

        public string FemaleVoice { get; set; }

        public VNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MaleVoice = reader.ReadFormId(base.Data);
            FemaleVoice = reader.ReadFormId(base.Data);
        }
    }
}
