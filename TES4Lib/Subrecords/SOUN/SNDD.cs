using System;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.SOUN
{
    /// <summary>
    /// Only occurs twice, looks to be the first 8 bytes of a SNDX subrecord.
    /// </summary>
    public class SNDD : Subrecord
    {
        public byte[] Unknown { get; set; }
       
        public SNDD(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Unknown = reader.ReadBytes<byte[]>(base.Data, base.Size);
        }
    }
}
