using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Effect Script effect name, originaly FULL
    /// </summary>
    public class SULL : Subrecord
    {
        public string ScriptEffectName { get; set; }

        public SULL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ScriptEffectName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
