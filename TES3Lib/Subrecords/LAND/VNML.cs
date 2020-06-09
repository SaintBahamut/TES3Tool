using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.LAND
{
    /// <summary>
    /// Vertex normals
    /// A RGB color map 65x65 pixels in size representing the land normal vectors.
    /// The signed value of the 'color' represents the vector's component. Blue
	/// is vertical(Z), Red the X direction and Green the Y direction.Note that
    /// the y-direction of the data is from the bottom up.
    /// </summary>
    public class VNML : Subrecord
    {
        const int size = 65;

        public normal[,] normals {get;set;}

        public VNML()
        {
        }

        public VNML(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            normals = new normal[size,size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    var bytes = reader.ReadBytes<byte[]>(base.Data, 3);
                    normals[y, x].x = bytes[0];
                    normals[y, x].y = bytes[1];
                    normals[y, x].z = bytes[2];
                }
            }
        }
    }

    public struct normal
    {
        public byte x;
        public byte y;
        public byte z;
    }
}