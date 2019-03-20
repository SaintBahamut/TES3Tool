using TES4Lib.Base;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Unknown, follows a MO4T (variable size)
    /// </summary>
    public class MO4T : Subrecord
    {
        public MO4T(byte[] rawData) : base(rawData)
        {
        }
    }
}