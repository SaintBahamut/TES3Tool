using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.CLOT
{
    /// <summary>
    /// Female part name
    /// </summary>
    public class CNAM : Subrecord
    {
        /// <summary>
        /// Female tagged bodpart id
        /// </summary>
        public string FemalePartName { get; set; }

        public CNAM()
        {

        }

        public CNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            FemalePartName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
