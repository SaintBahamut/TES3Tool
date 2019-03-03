using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.NPC_
{
    public class DODT : Subrecord
    {
        public float PositionX { get; set; }

        public float PositionY { get; set; }

        public float PositionZ { get; set; }

        public float RotationX { get; set; }

        public float RotationY { get; set; }

        public float RotationZ { get; set; }

        public DODT()
        {

        }

        public DODT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            PositionX = reader.ReadBytes<float>(base.Data);
            PositionY = reader.ReadBytes<float>(base.Data);
            PositionZ = reader.ReadBytes<float>(base.Data);
            RotationX = reader.ReadBytes<float>(base.Data);
            RotationY = reader.ReadBytes<float>(base.Data);
            RotationZ = reader.ReadBytes<float>(base.Data);
        }
    }
}