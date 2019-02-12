using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.CELL
{
    public class XCLR : Subrecord
    {
        /// <summary>
        ///  FormId: Regions containing the cell
        /// </summary>
        public float RegionsContainingCell { get; set; }

        public XCLR(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            RegionsContainingCell = reader.ReadBytes<float>(base.Data);
        }
    }
}
