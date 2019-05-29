using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.SNDG
{
    /// <summary>
    /// Body Part Index (1 byte)
    /// </summary>
    public class DATA : Subrecord
    {
        /// <summary>
        /// 0 = Regular Topic
        /// 1 = Voice
        /// 2 = Greeting
        /// 3 = Persuasion
        /// 4 = Journal
        /// </summary>
        public byte DialogueType { get; set; }

        public DATA()
        {

        }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            DialogueType = reader.ReadBytes<byte[]>(base.Data, base.Size)[0];
        }
    }
}
