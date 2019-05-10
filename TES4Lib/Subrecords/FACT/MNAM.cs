using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.FACT
{
    /// <summary>
    /// Male rank title
    /// </summary>
    public class MNAM : Subrecord
    {
        public string MaleRankTitle { get; set; }

        public MNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();

            MaleRankTitle = reader.ReadBytes<string>(base.Data, base.Size);        
        }
    }
}
