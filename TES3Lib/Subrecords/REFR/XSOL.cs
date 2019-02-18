using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    /// <summary>
    /// Soul Extra Data (ID string of creature)
    /// </summary>
    public class XSOL : Subrecord
    {
        public string CreatureId { get; set; }

        public XSOL()
        {

        }

        public XSOL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            CreatureId = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
