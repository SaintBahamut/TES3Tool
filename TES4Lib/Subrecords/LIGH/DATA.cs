using System;
using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Enums.Flags;
using Utility;

namespace TES4Lib.Subrecords.LIGH
{
    /// <summary>
    /// Object coordinates : X , Y, Z, rX, rY, rZ. angles are in radian
    /// angles actually represent negative rotations.For example, a roation of 90 degrees about the x-axis would mean that when looking toward the origin from a positive coordinate on the x-axis, the rotation would appear to be clockwise rather than counterclockwise. (Most coordinate systems define positive roations as counterclockwise.)
    /// the rotation about the z axis is applied first, followed by the y rotation, and the x rotation is applied last
    /// </summary>
    public class DATA : Subrecord
    {
        /// <summary>
        /// Duration time in seconds (?) for a carried light.
        /// A value of -1 represents an infinite duration.
        /// Default value of -1.
        /// </summary>
        public int Time { get; set; }

        public int Radius { get; set; }

        public int Color { get; set; }

        /// <summary>
        /// Light flags:
        /// 0x00000001 = Dynamic
        /// 0x00000002 = Can be Carried
        /// 0x00000004 = Negative
        /// 0x00000008 = Flicker
        /// 0x00000020 = Off By Default
        /// 0x00000040 = Flicker Slow
        /// 0x00000080 = Pulse
        /// 0x00000100 = Pulse Slow
        /// 0x00000200 = Spot Light
        /// 0x00000400 = Spot Shadow
        /// </summary>
        public HashSet<LightFlag> Flags { get; set; }
        public float Falloff { get; set; }
        public float FOV { get; set; }
        public int Value { get; set; }
        public float Weight { get; set; }


        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Time = reader.ReadBytes<int>(base.Data);
            Radius = reader.ReadBytes<int>(base.Data);
            Color = reader.ReadBytes<int>(base.Data);
            Flags = reader.ReadBytes<LightFlag>(base.Data);

            if (base.Size == 32)
            {
                Falloff = reader.ReadBytes<float>(base.Data);
                FOV = reader.ReadBytes<float>(base.Data);
            }

            Value = reader.ReadBytes<int>(base.Data);
            Weight = reader.ReadBytes<float>(base.Data);
        }
    }
}

