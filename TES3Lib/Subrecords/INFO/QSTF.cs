using System.Diagnostics;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.INFO
{
    /// <summary>
    /// Marks INFO entry as Finished (Journal DIAL type)
    /// </summary>
    [DebuggerDisplay("Finished: {IsFinished}")]
    public class QSTF : Subrecord
    {
        public bool IsFinished { get; set; }

        public QSTF()
        {
            IsFinished = false;
        }

        public QSTF(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            IsFinished = reader.ReadBytes<bool>(base.Data);
        }
    }
}
