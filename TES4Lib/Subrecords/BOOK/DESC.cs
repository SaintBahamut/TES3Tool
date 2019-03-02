using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.BOOK
{
    /// <summary>
    /// Book text
    /// </summary>
    public class DESC : Subrecord
    {
        /// <summary>
        /// Book text
        /// </summary>
        public string Text { get; set; }

        public DESC(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Text = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
