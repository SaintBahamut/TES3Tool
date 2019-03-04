using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// Cell name for DODT record, if interior
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