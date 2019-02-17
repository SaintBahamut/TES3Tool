using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.STAT
{
    public class MODB : Subrecord
    {
        public string ModelFileName { get; set; }

        public MODB(byte[] rawData) : base(rawData)
        {
        }
    }
}