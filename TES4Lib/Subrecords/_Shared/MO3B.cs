using TES4Lib.Base;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    ///  Unknown, follows a MOD3 (4 bytes)
    /// </summary>
    public class MO3B : Subrecord
    {
        public MO3B(byte[] rawData) : base(rawData)
        {
        }
    }
}