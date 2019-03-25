using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// Target cell name
    /// Cell name for coordinates from DODT record, (interior or travel service)
    /// </summary>
    public class DNAM : Subrecord
    {
        public string InteriorCellName { get; set; }

        public DNAM()
        {
        }

        public DNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            InteriorCellName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}