using System.Collections.Generic;
using TES4Lib.Base;
using TES4Lib.Subrecords.ENCH;
using TES4Lib.Subrecords.Shared;

namespace TES4Lib.Records
{
    public class ENCH : Record
    {
        public EDID EDID { get; set; }

        /// <summary>
        /// Might be not even needed
        /// </summary>
        public FULL FULL { get; set; }

        public ENIT ENIT { get; set; }

        public List<EFFC> EFFC { get; set; }

        public ENCH(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        protected override void BuildSubrecords()
        {
            base.BuildSubrecords();
        }
    }

    /// <summary>
    /// Made up class for grouping enchantment effect
    /// </summary>
    public class EFFC
    {
        public EFID EFID { get; set; }

        public EFIT EFIT { get; set; }

        public SCIT SCIT { get; set; }

        /// <summary>
        /// Holds the custom script effect name
        /// </summary>
        public FULL FULL { get; set; }
    }
}