using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Enums;
using TES4Lib.Enums.Flags;
using Utility;

namespace TES4Lib.Subrecords.ALCH
{
    /// <summary>
    /// Alchemy flags
    /// </summary>
    public class ENIT : Subrecord
    {
        public int Value { get; set; }

        public HashSet<AlchemyFlag> Flags { get; set; }

        public byte[] Unknown { get; set; }

        public ENIT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Value = reader.ReadBytes<int>(base.Data);
            Flags = reader.ReadFlagBytes<AlchemyFlag>(base.Data);
            Unknown = reader.ReadBytes<byte[]>(base.Data,3);
        }
    }
}
