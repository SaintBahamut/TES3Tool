using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.REFR
{
    public class TNAM : Subrecord
    {
        /// <summary>
        /// Present when the reference is a map marker.
        /// 0x00 = None?
        /// 0x01 = Camp
        /// 0x02 = Cave
        /// 0x03 = City
        /// 0x04 = Elven Ruin
        /// 0x05 = Fort Ruin
        /// 0x06 = Mine
        /// 0x07 = Landmark
        /// 0x08 = Tavern
        /// 0x09 = Settlement
        /// 0x0A = Daedric Shrine
        /// 0x0B = Oblivion Gate
        /// 0x0C = Unknown? (door icon)
        /// </summary>

        public byte[] MarkerData { get; set; }

        public TNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MarkerData = reader.ReadBytes<byte[]>(base.Data, base.Size);
        }
    }
}
