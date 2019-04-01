using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Enums.Flags;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// NPC AI data subrecord
    /// </summary>
    public class AIDT : Subrecord
    {
        public byte Hello { get; set; }

        public byte Unknown1 { get; set; }

        public byte Fight { get; set; }

        public byte Flee { get; set; }

        public byte Alarm { get; set; }

        public byte Unknown2 { get; set; }

        public byte Unknown3 { get; set; }

        public byte Unknown4 { get; set; }

        public HashSet<ServicesFlag> Flags { get; set; }

        public AIDT()
        {
        }

        public AIDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Hello = reader.ReadBytes<byte>(base.Data);
            Unknown1 = reader.ReadBytes<byte>(base.Data);
            Fight = reader.ReadBytes<byte>(base.Data);
            Flee = reader.ReadBytes<byte>(base.Data);
            Alarm = reader.ReadBytes<byte>(base.Data);
            Unknown2 = reader.ReadBytes<byte>(base.Data);
            Unknown3 = reader.ReadBytes<byte>(base.Data);
            Unknown4 = reader.ReadBytes<byte>(base.Data);
            Flags = reader.ReadFlagBytes<ServicesFlag>(base.Data);
        }
    }
}