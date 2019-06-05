using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;
using TES3Lib.Subrecords.SPEL;
using Utility;
using static Utility.Common;

namespace TES3Lib.Records
{
    [DebuggerDisplay("{NAME.EditorId}")]
    public class SPEL : Record
    {
        public NAME NAME { get; set; }

        public FNAM FNAM { get; set; }

        public SPDT SPDT { get; set; }

        public List<ENAM> ENAM { get; set; }

        public SPEL()
        {
        }

        public SPEL(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
