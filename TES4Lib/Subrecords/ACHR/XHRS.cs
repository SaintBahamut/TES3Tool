using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.ACHR
{
    /// <summary>
    /// Horse
    /// </summary>
    public class XHRS : Subrecord
    {
        public string ParentFormId { get; set; }

        public uint Flags { get; set; }

        public XHRS(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ParentFormId = reader.ReadFormId(base.Data);
            //Flags = reader.ReadBytes<uint>(base.Data);
        }
    }
}
