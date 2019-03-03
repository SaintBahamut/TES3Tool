using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    /// <summary>
    /// Cell name for DODT record, if interior
    /// </summary>
    public class DNAM : Subrecord
    {
        public string CellName { get; set; }

        public DNAM()
        {

        }

        public DNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            CellName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}