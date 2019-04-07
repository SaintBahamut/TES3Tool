using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.WRLD
{
    public class NAM2 : Subrecord
    {
        /// <summary>
        /// Water type FormId
        /// </summary>
        public string WaterType { get; set; }

        public NAM2(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            WaterType = reader.ReadFormId(base.Data);
        }
    }
}
