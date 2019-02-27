using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.CONT
{
    public class DATA : Subrecord
    {
        /// <summary>
        /// Flags
        /// 0x02 = Respawns
        /// </summary>
        public byte Flags { get; set; }

        public float Weight { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flags = reader.ReadBytes<byte>(base.Data);
            Weight = reader.ReadBytes<float>(base.Data);
        }
    }
}
