using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.FACT
{
    /// <summary>
    /// Crime gold multiplayer
    /// </summary>
    public class CNAM : Subrecord
    {
        public float CrimeGoldMultiplier { get; set; }

        public CNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            CrimeGoldMultiplier = reader.ReadBytes<float>(base.Data);        
        }
    }
}
