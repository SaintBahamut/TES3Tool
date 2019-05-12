using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.RACE
{
    /// <summary>
    /// FaceGen - Face clamp
    /// </summary>
    public class UNAM : Subrecord
    {
        public float FaceClampValue { get; set; }

        public UNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            FaceClampValue = reader.ReadBytes<float>(base.Data);
        }
    }
}
