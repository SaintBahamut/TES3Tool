using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.CONT
{
    /// <summary>
    /// Container flags
    /// </summary>
    public class FLAG : Subrecord
    {
        /// <summary>
        ///	0x0001  = Organic
        ///	0x0002	= Respawns, organic only
        ///	0x0008	= Default, unknown
        /// </summary>
        public int Flags { get; set; }

        public FLAG()
        {

        }

        public FLAG(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flags = reader.ReadBytes<int>(base.Data);
        }
    }
}
