using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class BNAM : Subrecord
    {
        /// <summary>
        /// Head Body Part Id
        /// </summary>
        public string HeadModel { get; set; }

        public BNAM()
        {

        }

        public BNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            HeadModel = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
