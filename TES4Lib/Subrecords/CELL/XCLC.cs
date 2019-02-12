using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.CELL
{
    public class XCLC : Subrecord
    {
        /// <summary>
        ///  FormId: (X, Y) grid (only used for exterior cells)
        /// </summary>
        public int GridX { get; set; }
        public int GridY { get; set; }

        public XCLC(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            GridX = reader.ReadBytes<int>(base.Data);
            GridY = reader.ReadBytes<int>(base.Data);
        }
    }
}