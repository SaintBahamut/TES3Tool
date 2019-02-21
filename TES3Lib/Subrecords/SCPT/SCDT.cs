using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.SCPT
{
    /// <summary>
    /// Contains compliled script data
    /// </summary>
    public class SCDT : Subrecord
    {
        /// <summary>
        /// The compiled script data
        /// </summary>
        public byte[] CompiledScript { get; set; }
       
        public SCDT()
        {
        }

        public SCDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            CompiledScript = reader.ReadBytes<byte[]>(base.Data, base.Size);
        }
    }
}
