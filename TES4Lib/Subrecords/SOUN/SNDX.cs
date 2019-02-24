using System;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.SOUN
{
    /// <summary>
    /// Only occurs twice, looks to be the first 8 bytes of a SNDX subrecord.
    /// </summary>
    public class SNDX : Subrecord
    {
        /// <summary>
        /// Minimum attentuation distance (multiply by 5 to get value in game units).
        /// </summary>
        public byte MinAttentuationDist { get; set; }

        /// <summary>
        /// Maximum attentuation distance (multiply by 100 to get value in game units)
        /// </summary>
        public byte MaxAttentuationDist { get; set; }

        /// <summary>
        /// Frequency adjustment, %, signed value
        /// </summary>
        public byte FrequencyAdj { get; set; }

        public byte Unknown { get; set; }

        /// <summary>
        /// 0x0001 = Random Frequency Shift
        /// 0x0002 = Play At Random
        /// 0x0004 = Environment Ignored
        /// 0x0008 = Random Location
        /// 0x0010 = Loop
        /// 0x0020 = Menu Sound
        /// 0x0040 = 2D
        /// 0x0080 = 360 LFE
        /// </summary>
        public short Flags { get; set; }

        public short Unknown2 { get; set; }

        /// <summary>
        /// Static attentuation (divide by 100 to get value in dB).
        /// </summary>
        public short StaticAttentuation { get; set; }

        /// <summary>
        /// Stop time (multiply by 1440/256 to get value in minutes)
        /// </summary>
        public byte StopTime { get; set; }

        /// <summary>
        /// Start time (multiply by 1440/256 to get value in minutes)
        /// </summary>
        public byte StartTime { get; set; }

        public SNDX(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MinAttentuationDist = reader.ReadBytes<byte[]>(base.Data, 1)[0];
            MaxAttentuationDist = reader.ReadBytes<byte[]>(base.Data, 1)[0];
            FrequencyAdj = reader.ReadBytes<byte[]>(base.Data, 1)[0];
            Unknown = reader.ReadBytes<byte[]>(base.Data, 1)[0];
            Flags = reader.ReadBytes<short>(base.Data);
            Unknown2 = reader.ReadBytes<short>(base.Data);
            StaticAttentuation = reader.ReadBytes<short>(base.Data);
            StopTime = reader.ReadBytes<byte[]>(base.Data,1)[0];
            StartTime = reader.ReadBytes<byte[]>(base.Data,1)[0];
        }

    }
}
