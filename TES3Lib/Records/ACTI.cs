using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;

namespace TES3Lib.Records
{
    /// <summary>
    /// Activator Record
    /// </summary>
    public class ACTI : Record
    {
        public NAME NAME { get; set; }
        public MODL MODL { get; set; }
        public FNAM FNAM { get; set; }
        public SCRI SCRI { get; set; }

        public ACTI()
        {

        }

        public ACTI(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
