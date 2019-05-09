using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.NPC_
{
    /// <summary>
    /// FaceGen Geometry-Symmetric
    /// </summary>
    public class FGGS : Subrecord
    {
        public byte[] fgGeoSym { get; set; }

        public FGGS(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            fgGeoSym = reader.ReadBytes<byte[]>(this.Data, this.Size);
        }
    }
}
