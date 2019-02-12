using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.CELL
{
    public class XGBL : Subrecord
    {
        /// <summary>
        /// FormId:  Global variable (if owner is NPC)
        /// </summary>
        public byte[] IfOwnerIsNPC { get; set; }

        public XGBL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            IfOwnerIsNPC = reader.ReadBytes<byte[]>(base.Data, base.Size);
        }
    }
}
