using TES4Lib.Base;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    ///  Unknown, follows a MOD4 (4 bytes)
    /// </summary>
    public class MO4B : Subrecord
    {
        public MO4B(byte[] rawData) : base(rawData)
        {
        }
    }
}