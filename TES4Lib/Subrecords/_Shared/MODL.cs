using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Model of record
    /// </summary>
    public class MODL : Subrecord
    {
        /// <summary>
        /// Models path
        /// </summary>
        public string ModelPath { get; set; }

        public MODL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ModelPath = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
