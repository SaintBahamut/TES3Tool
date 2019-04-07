using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Enums.Flags;
using Utility;

namespace TES4Lib.Subrecords.WRLD
{
    public class DATA : Subrecord
    {
        public HashSet<WorldSpaceFlag> Flags { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flags = reader.ReadFlagBytes<WorldSpaceFlag>(base.Data);
        }
    }
}
