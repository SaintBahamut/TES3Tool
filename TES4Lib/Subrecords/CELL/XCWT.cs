using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.CELL
{
    public class XCWT : Subrecord
    {
        /// <summary>
        /// FormId: water type?
        /// </summary>
        public byte[] Water { get; set; }

        public XCWT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Water = reader.ReadBytes<byte[]>(base.Data, base.Size);
        }
    }
}
