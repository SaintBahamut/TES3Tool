using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CREA
{
    /// <summary>
    /// Texture Files Hashes
    /// </summary>
    public class NIFT : Subrecord
    {
        public byte[] TextureHashes { get; set; }

        public NIFT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            TextureHashes = reader.ReadBytes<byte[]>(base.Data, base.Size);
        }
    }
}
