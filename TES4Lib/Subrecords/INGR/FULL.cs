using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.INGR
{
    /// <summary>
    /// Script effect name
    /// </summary>
    public class FULL : Subrecord
    {
        public string ScriptEffectName { get; set; }

        public FULL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ScriptEffectName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
