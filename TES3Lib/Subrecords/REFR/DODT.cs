using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    /// <summary>
    /// Door teleport coordinates (door exit)
    /// </summary>
    public class DODT : Subrecord
    {
        public float XPos { get; set; }
        public float YPos { get; set; }
        public float ZPos { get; set; }
        public float XRotate { get; set; }
        public float YRotate { get; set; }
        public float ZRotate { get; set; }

        public DODT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            XPos = reader.ReadBytes<float>(base.Data);
            YPos = reader.ReadBytes<float>(base.Data);
            ZPos = reader.ReadBytes<float>(base.Data);
            XRotate = reader.ReadBytes<float>(base.Data);
            YRotate = reader.ReadBytes<float>(base.Data);
            ZRotate = reader.ReadBytes<float>(base.Data);

        }
    }
}
