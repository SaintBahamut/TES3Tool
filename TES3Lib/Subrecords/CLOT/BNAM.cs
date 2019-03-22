using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.CLOT
{
    /// <summary>
    /// Male part name
    /// </summary>
    public class BNAM : Subrecord
    {
        /// <summary>
        /// Body part without female tag
        /// </summary>
        public string MalePartName { get; set; }

        public BNAM()
        {

        }

        public BNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MalePartName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
