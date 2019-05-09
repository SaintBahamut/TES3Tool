using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.NPC_
{
    /// <summary>
    /// FaceGen Geometry-Asymmetric
    /// </summary>
    public class FGGA : Subrecord
    {
        public byte[] fgGeoAsym { get; set; }

        public FGGA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            fgGeoAsym = reader.ReadBytes<byte[]>(this.Data, this.Size);
        }
    }
}
