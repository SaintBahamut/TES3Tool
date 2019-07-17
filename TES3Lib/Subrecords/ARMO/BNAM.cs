using System.Diagnostics;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.ARMO
{
    /// <summary>
    /// Male part name
    /// </summary>
    [DebuggerDisplay("{MalePartName}")]
    public class BNAM : Subrecord
    {
        /// <summary>
        /// Male tagged bodpart id
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
