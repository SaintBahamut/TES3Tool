using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.ALCH
{
    /// <summary>
    /// Item weight
    /// </summary>
    public class DATA : Subrecord
    {
        public float Weight { get; set; }
     
        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Weight = reader.ReadBytes<float>(base.Data);          
        }
    }
}

