using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    /// <summary>
    /// Cell escort/follow for AI Package (optional)
    /// </summary>
    public class CNDT : Subrecord
    {
        public string CellName { get; set; }

        public CNDT()
        {
        }

        public CNDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            CellName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}