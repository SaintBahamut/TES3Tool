using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LAND
{
    /// <summary>
    /// Vertex height map data
    /// </summary>
    public class VHGT : Subrecord
    {
        public const int CELL_SIDE = 65;
        public const int CELL_SIZE = CELL_SIDE* CELL_SIDE;

        /// <summary>
        /// A height offset for the entire cell.
        /// Decreasing this value will shift the
        /// entire cell land down
        /// </summary>
        public float HeightOffset { get; set; }

        /// <summary>
        /// Contains the height data for the cell in the form of a 65x65 pixel array. The
        /// height data is not absolute values but uses differences between adjacent pixels.
        /// Thus a pixel value of 0 means it has the same height as the last pixel.Note that
        /// the y-direction of the data is from the bottom up.
        /// row/cell 65 is height data from adjacent north-east cells
        /// </summary>
        public sbyte[,] HeightDelta { get; set; }

        public short Unknown1 { get; set; }

        public byte Unknown2 { get; set; }

        public VHGT()
        {
            Unknown1 = -22801;
            Unknown2 = 0;
        }

        public VHGT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            HeightOffset = reader.ReadBytes<float>(base.Data);
            HeightDelta = new sbyte[CELL_SIDE, CELL_SIDE];
            for (int y = 0; y < CELL_SIDE; y++)
            {
                for (int x = 0; x < CELL_SIDE; x++)
                {
                    HeightDelta[y,x] = reader.ReadBytes<sbyte>(base.Data);
                }
            }

            Unknown1 = reader.ReadBytes<short>(base.Data);
            Unknown2 = reader.ReadBytes<byte>(base.Data);
        }

        public override byte[] SerializeSubrecord()
        {
            List<byte> data = new List<byte>();

            data.AddRange(ByteWriter.ToBytes(HeightOffset, typeof(float)));
            for (int y = 0; y < HeightDelta.GetLength(0); y++)
            {
                for (int x = 0; x < HeightDelta.GetLength(1); x++)
                {
                    data.AddRange(ByteWriter.ToBytes(HeightDelta[y,x], typeof(sbyte)));
                }            
            }
            data.AddRange(ByteWriter.ToBytes(Unknown1, typeof(short)));
            data.AddRange(ByteWriter.ToBytes(Unknown2, typeof(byte)));

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }
    }
}