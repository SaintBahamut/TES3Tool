using TES4Lib.Base;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Unknown, follows a MO3T (variable size)
    /// </summary>
    public class MO3T : Subrecord
    {

        public MO3T(byte[] rawData) : base(rawData)
        {
        }
    }
}