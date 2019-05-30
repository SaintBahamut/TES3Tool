using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Enums.Flags;
using Utility;

namespace TES3Lib.Subrecords.CONT
{
    /// <summary>
    /// Container flags
    /// </summary>
    public class FLAG : Subrecord
    {
        public HashSet<ContainerFlag> Flags { get; set; }

        public FLAG()
        {
        }

        public FLAG(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flags = reader.ReadFlagBytes<ContainerFlag>(base.Data);
        }
    }
}
