using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Icon file
    /// </summary>
    public class ICON : Subrecord
    {
        /// <summary>
        /// Icon path
        /// </summary>
        public string IconFilePath { get; set; }

        public ICON(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            IconFilePath = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}