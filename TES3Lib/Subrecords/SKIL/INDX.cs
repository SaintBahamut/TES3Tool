using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;

namespace TES3Lib.Subrecords.SKILL
{
    public class INDX : Subrecord
    {
        public Skill Skill { get; }

        public INDX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Skill = (Skill)reader.ReadBytes<byte>(base.Data);
        }
    }
}