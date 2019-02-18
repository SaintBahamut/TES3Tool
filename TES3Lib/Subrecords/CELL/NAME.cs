using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.CELL
{
    public class NAME : Subrecord
    {
        /// <summary>
        /// Cell ID string. Can be an empty string for exterior cells in which case
		/// the region name is used instead.
        /// </summary>
        public string CellName { get; set; }

        public NAME()
        {
        }

        public NAME(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            CellName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
