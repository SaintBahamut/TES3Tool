using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Records text description
    /// </summary>
    public class DESC : Subrecord
    {
        /// <summary>
        /// Block of text
        /// </summary>
        public string Description { get; set; }

        public DESC(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Description = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
