using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.SCPT
{
    /// <summary>
    /// Script text
    /// </summary>
    public class SCTX : Subrecord
    {
        public string ScriptText { get; set; }

        public SCTX()
        {
        }

        public SCTX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ScriptText = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
