using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.NPC_
{
    /// <summary>
    /// FaceGen Texture-Symmetic
    /// </summary>
    public class FGTS : Subrecord
    {
        public byte[] fgTexSym { get; set; }

        public FGTS(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            fgTexSym = reader.ReadBytes<byte[]>(this.Data, this.Size);
        }
    }
}
