using System;
using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Enums;
using Utility;

namespace TES4Lib.Subrecords.ARMO
{
    /// <summary>
    /// Armor flags and body slot info
    /// </summary>
    public class BMDT : Subrecord
    {
        public int BodySlot { get; set; }

        public HashSet<BodySlot> BodySlots { get; set; }

        public short Flags { get; set; }

        public HashSet<ArmorFlags> ArmorFlags { get; set; }

        public byte Unused { get; set; }

        public BMDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            BodySlot = reader.ReadBytes<int>(base.Data);
            if(!BodySlot.Equals(0))
            {
                BodySlots = new HashSet<BodySlot>();
                var flagValues = (int[])Enum.GetValues(typeof(BodySlot));
                foreach (var flag in flagValues)
                {
                    if ((flag & BodySlot) != 0)
                    {
                        BodySlots.Add((BodySlot)flag));
                    }
                }

            }

            Flags = reader.ReadBytes<short>(base.Data);
            if (!Flags.Equals(0))
            {
                ArmorFlags = new HashSet<ArmorFlags>();
                var flagValues = (int[])Enum.GetValues(typeof(ArmorFlags));
                foreach (var flag in flagValues)
                {
                    if ((flag & Flags) != 0)
                    {
                        ArmorFlags.Add((ArmorFlags)flag));
                    }
                }
            }
            
            Unused = reader.ReadBytes<byte>(base.Data);
        }
    }
}
