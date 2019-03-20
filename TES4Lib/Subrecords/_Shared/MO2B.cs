using TES4Lib.Base;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Unknown, follows a MOD2 (4 bytes)
    /// </summary>
    public class MO2B : Subrecord
    {
        public MO2B(byte[] rawData) : base(rawData)
        {
        }
    }
}