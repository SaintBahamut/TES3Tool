using System;
using TES4Lib.Structures.Base;
using Utility;

namespace TES4Lib.Subrecords.REFR
{
    /// <summary>
    /// Object coordinates : X , Y, Z, rX, rY, rZ. angles are in radian
    /// angles actually represent negative rotations.For example, a roation of 90 degrees about the x-axis would mean that when looking toward the origin from a positive coordinate on the x-axis, the rotation would appear to be clockwise rather than counterclockwise. (Most coordinate systems define positive roations as counterclockwise.)
    /// the rotation about the z axis is applied first, followed by the y rotation, and the x rotation is applied last
    /// </summary>
    public class DATA : Subrecord
    {
        public float LocX { get; set; }
        public float LocY { get; set; }
        public float LocZ { get; set; }
        public float RotX { get; set; }
        public float RotY { get; set; }
        public float RotZ { get; set; }


        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            LocX = reader.ReadBytes<float>(base.Data);
            LocY = reader.ReadBytes<float>(base.Data);
            LocZ = reader.ReadBytes<float>(base.Data);
            RotX = reader.ReadBytes<float>(base.Data);
            RotY = reader.ReadBytes<float>(base.Data);
            RotZ = reader.ReadBytes<float>(base.Data);
        }
    }
}

