using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.RACE
{
    /// <summary>
    /// FaceGen - Main clamp
    /// </summary>
    public class PNAM : Subrecord
    {
        public float MainClampValue { get; set; }

        public PNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MainClampValue = reader.ReadBytes<float>(base.Data);
        }
    }
}
