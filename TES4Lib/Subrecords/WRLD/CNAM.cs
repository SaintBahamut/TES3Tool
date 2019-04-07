using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.WRLD
{
    public class CNAM : Subrecord
    {
        /// <summary>
        /// Climate FormId
        /// </summary>
        public string Climate { get; set; }

        public CNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Climate = reader.ReadFormId(base.Data);
        }
    }
}
