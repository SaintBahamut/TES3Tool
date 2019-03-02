using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.DOOR
{
    /// <summary>
    /// Random teleport destination
    /// </summary>
    public class TNAM : Subrecord
    {
        /// <summary>
        /// Destination formId
        /// </summary>
        public string Destination { get; set; }

        public TNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            var baseFormIdBytes = reader.ReadBytes<byte[]>(base.Data, base.Size);
            Destination = BitConverter.ToString(baseFormIdBytes.Reverse().ToArray()).Replace("-", "");
        }
    }
}

