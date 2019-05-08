using System.Diagnostics;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// Description
    /// </summary>
    [DebuggerDisplay("{Description}")]
    public class DESC : Subrecord
    {
        /// <summary>
        /// Text description
        /// </summary>
        public string Description { get; set; }

        public DESC()
        {

        }

        public DESC(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Description = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}