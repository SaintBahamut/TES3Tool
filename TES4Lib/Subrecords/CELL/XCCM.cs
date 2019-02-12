using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.CELL
{
    public class XCCM : Subrecord
    {
        /// <summary>
        /// FormId: Climate (if interior cell behaves like exterior cell)
        /// </summary>
        public byte[] Climate { get; set; }

        public XCCM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Climate = reader.ReadBytes<byte[]>(base.Data, base.Size);
        }
    }
}
