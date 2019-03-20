using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Male world model filename (variable length string)
    /// </summary>
    public class MOD2 : Subrecord
    {
        /// <summary>
        /// Models path
        /// </summary>
        public string ModelPath { get; set; }

        public MOD2(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ModelPath = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
