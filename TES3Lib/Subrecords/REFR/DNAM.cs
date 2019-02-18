using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.REFR
{
    public class DNAM : Subrecord
    {
        public string DoorName { get; set; }

        public DNAM()
        {

        }

        public DNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            DoorName = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
