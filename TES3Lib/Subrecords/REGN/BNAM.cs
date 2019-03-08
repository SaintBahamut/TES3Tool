using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.REGN
{
    public class BNAM : Subrecord
    {
        /// <summary>
        /// Id of creature that wokes player when resting
        /// </summary>
        public string SleepCreatureId { get; set; }

        public BNAM()
        {
        }

        public BNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SleepCreatureId = reader.ReadBytes<string>(base.Data, base.Size);        
        }
    }
}
