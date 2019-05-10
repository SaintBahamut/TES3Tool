using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Enums.Flags;
using Utility;

namespace TES4Lib.Subrecords.FACT
{
    /// <summary>
    /// Faction flags
    /// </summary>
    public class DATA : Subrecord
    {
        public HashSet<FactionFlag> Flags { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            Flags = reader.ReadFlagBytes<FactionFlag>(base.Data);        
        }
    }
}
