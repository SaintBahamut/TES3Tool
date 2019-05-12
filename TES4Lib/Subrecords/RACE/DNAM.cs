using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.RACE
{
    /// <summary>
    /// Default Hair for race
    /// </summary>
    public class DNAM : Subrecord
    {
        public string MaleHair { get; set; }

        public string FemaleHair { get; set; }

        public DNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MaleHair = reader.ReadFormId(base.Data);
            FemaleHair = reader.ReadFormId(base.Data);
        }
    }
}
