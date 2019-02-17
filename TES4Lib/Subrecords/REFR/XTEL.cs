using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.REFR
{
    public class XTEL : Subrecord
    {
        public string DestinationDoorReference { get; set; }
        public float DestLocX { get; set; }
        public float DestLocY { get; set; }
        public float DestLocZ { get; set; }
        public float DestRotX { get; set; }
        public float DestRotY { get; set; }
        public float DestRotZ { get; set; }


        public XTEL(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            var DestinationDoorReferenceBytes = reader.ReadBytes<byte[]>(base.Data, 4);
            DestinationDoorReference = BitConverter.ToString(DestinationDoorReferenceBytes.Reverse().ToArray()).Replace("-", "");
            DestLocX = reader.ReadBytes<float>(base.Data);
            DestLocY = reader.ReadBytes<float>(base.Data);
            DestLocZ = reader.ReadBytes<float>(base.Data);
            DestRotX = reader.ReadBytes<float>(base.Data);
            DestRotY = reader.ReadBytes<float>(base.Data);
            DestRotZ = reader.ReadBytes<float>(base.Data);
        }
    }
}
