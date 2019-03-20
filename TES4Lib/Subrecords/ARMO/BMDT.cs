using System;
using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Enums;
using TES4Lib.Enums.Flags;
using Utility;

namespace TES4Lib.Subrecords.ARMO
{
    /// <summary>
    /// Armor flags and body slot info
    /// </summary>
    public class BMDT : Subrecord
    {
        public ushort BodySlot { get; set; }

        public HashSet<BodySlot> BodySlots { get; set; }

        public ushort Flags { get; set; }

        public HashSet<EquipmentFlag> ArmorFlags { get; set; }

        public BMDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            BodySlot = reader.ReadBytes<ushort>(base.Data);
            if(!BodySlot.Equals(0))
            {
                BodySlots = new HashSet<BodySlot>();
                var flagValues = (ushort[])Enum.GetValues(typeof(BodySlot));
                foreach (var flag in flagValues)
                {
                    if ((flag & BodySlot) != 0)
                    {
                        BodySlots.Add((BodySlot)flag);
                    }
                }

            }

            Flags = reader.ReadBytes<ushort>(base.Data);
            if (!Flags.Equals(0))
            {
                ArmorFlags = new HashSet<EquipmentFlag>();
                var flagValues = (ushort[])Enum.GetValues(typeof(EquipmentFlag));
                foreach (var flag in flagValues)
                {
                    if ((flag & Flags) != 0)
                    {
                        ArmorFlags.Add((EquipmentFlag)flag);
                    }
                }
            }
        }
    }
}
