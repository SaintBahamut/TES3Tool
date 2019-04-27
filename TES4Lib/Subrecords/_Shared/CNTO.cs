using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Represents item held by container
    /// </summary>
    public class CNTO : Subrecord
    {
        /// <summary>
        /// Item formId
        /// </summary>
        public string ItemId { get; set; }

        public int ItemCount { get; set; }

        public CNTO(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ItemId = reader.ReadFormId(base.Data);
            ItemCount = reader.ReadBytes<int>(base.Data);
        }
    }
}
