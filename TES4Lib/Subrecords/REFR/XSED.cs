using System;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.REFR
{
    public class XSED : Subrecord
    {
        /// <summary>
        /// Target Reference (REFR, ACHR or ACRE)
        /// </summary>
        public string TargetRefFormId { get; set; }

        public XSED(byte[] rawData) : base(rawData)
        {
         
        }
    }
}
