using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;

namespace TES3Lib.Subrecords.DIAL
{
    /// <summary>
    /// Dialogue Type
    /// </summary>
    public class DATA : Subrecord
    {
        public DialogueType DialogueType { get; set; }

        public DATA()
        {
        }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            DialogueType = reader.ReadBytes<DialogueType>(base.Data);
        }
    }
}
