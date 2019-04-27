using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.NPC_
{
    /// <summary>
    /// NPC Race
    /// </summary>
    public class RNAM : Subrecord
    {
        /// <summary>
        /// FormId of referenced race
        /// </summary>
        public string RaceFormId { get; set; }

        public RNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            RaceFormId = reader.ReadFormId(base.Data);
        }
    }
}