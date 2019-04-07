using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.WRLD
{
    public class WNAM : Subrecord
    {
        /// <summary>
        /// Parent world space FormId
        /// </summary>
        public string ParentWorldSpace { get; set; }

        public WNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ParentWorldSpace = reader.ReadFormId(base.Data);
        }
    }
}
