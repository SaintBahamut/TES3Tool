using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// Icon path (32 characters max!)
    /// </summary>
    public class ITEX : Subrecord
    {
        public string IconPath { get; set; }

        public ITEX()
        {
        }

        public ITEX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            IconPath = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
