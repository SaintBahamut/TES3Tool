using System.Diagnostics;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// Display name of object
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class FNAM : Subrecord
    {
        public new string Name { get; set; }

        public FNAM()
        {
        }

        public FNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Name = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}