using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;

namespace TES3Lib.Records
{
    /// <summary>
    /// Static object record
    /// </summary>
    public class STAT : Record
    {
        public NAME NAME { get; set; }

        public MODL MODL { get; set; }

        public STAT()
        {
        }

        public STAT(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
