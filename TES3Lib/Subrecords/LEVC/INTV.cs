using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LEVC
{
    public class INTV : Subrecord
    {
        /// <summary>
        /// PC level for previous CNAM (item in CNAM/INTV pair)
        /// </summary>
        public short PCLevelOfPrevious { get; set; }

        public INTV()
        {
        }

        public INTV(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            PCLevelOfPrevious = reader.ReadBytes<short>(base.Data);
        }
    }
}
