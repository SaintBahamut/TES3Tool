using System.Diagnostics;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.INFO
{
    /// <summary>
    /// Marks INFO entry as Restart (Journal DIAL type)
    /// </summary>
    [DebuggerDisplay("Restart: {IsRestart}")]
    public class QSTR : Subrecord
    {
        public bool IsRestart { get; set; }

        public QSTR()
        {
            IsRestart = false;
        }

        public QSTR(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            IsRestart = reader.ReadBytes<bool>(base.Data);
        }
    }
}
