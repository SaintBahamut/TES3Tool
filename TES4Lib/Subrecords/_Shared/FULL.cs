using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Display name of record
    /// </summary>
    public class FULL : Subrecord
    {
        /// <summary>
        /// Ingame display name of record
        /// </summary>
        public string DisplayName { get; set; }

        public FULL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            DisplayName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
