using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.REFR
{
    /// <summary>
    /// Present only if the object is "open by default" 
    /// </summary>
    public class ONAM : Subrecord
    {
        public ONAM(byte[] rawData) : base(rawData)
        {
        }
    }
}
