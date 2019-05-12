using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Interation with other race or faction
    /// </summary>
    public class XNAM : Subrecord
    {
        /// <summary>
        /// FormId of referenced entity 
        /// </summary>
        public string FormId { get; set; }

        /// <summary>
        /// Disposition towards entity
        /// </summary>
        public int Disposition { get; set; }

        public XNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            FormId = reader.ReadFormId(base.Data);
            Disposition = reader.ReadBytes<int>(base.Data);        
        }
    }
}
