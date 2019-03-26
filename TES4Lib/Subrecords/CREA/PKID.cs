using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// AI packages
    /// </summary>
    public class PKID : Subrecord
    {
        /// <summary>
        /// FormId referencing PACK record
        /// </summary>
        public string AIPackageFormId { get; set; }

        public PKID(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            AIPackageFormId = reader.ReadFormId(base.Data);
        }
    }
}
