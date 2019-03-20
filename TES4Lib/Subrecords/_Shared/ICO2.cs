using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Icon file for female item variants
    /// </summary>
    public class ICO2 : Subrecord
    {
        /// <summary>
        /// Icon path
        /// </summary>
        public string IconFilePath { get; set; }

        public ICO2(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            IconFilePath = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}