using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Female world model filename (variable length string)
    /// </summary>
    public class MOD4 : Subrecord
    {
        /// <summary>
        /// Models path
        /// </summary>
        public string ModelPath { get; set; }

        public MOD4(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ModelPath = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
