using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.CELL
{
    public class DATA : Subrecord
    {
        /// <summary>
        /// 0x01 = Interior?
		///	0x02 = Has Water
		///	0x04 = Illegal to Sleep here
		///	0x80 = Behave like exterior(Tribunal)
        /// </summary>
        public int Flag { get; set; }

        public int GridX { get; set; }
        public int GridY { get; set; }

        public DATA()
        {
           
        }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flag = reader.ReadBytes<int>(base.Data);
            GridX = reader.ReadBytes<int>(base.Data);
            GridY = reader.ReadBytes<int>(base.Data);
        }
    }
}

