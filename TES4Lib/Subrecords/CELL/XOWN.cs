using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.CELL
{
    public class XOWN : Subrecord
    {
        /// <summary>
        /// FormId
        /// </summary>
        public byte[] Owner { get; set; }

        public XOWN(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Owner = reader.ReadBytes<byte[]>(base.Data, base.Size);
        }
    }
}
