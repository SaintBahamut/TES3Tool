using TES4Lib.Base;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Unknown, follows a MO2T (variable size)
    /// </summary>
    public class MO2T : Subrecord
    {

        public MO2T(byte[] rawData) : base(rawData)
        {
        }
    }
}