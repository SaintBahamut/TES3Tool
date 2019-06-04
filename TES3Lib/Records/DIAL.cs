using System.Collections.Generic;
using System.Diagnostics;
using TES3Lib.Base;
using TES3Lib.Subrecords.DIAL;
using TES3Lib.Subrecords.Shared;
using static Utility.Common;

namespace TES3Lib.Records
{
    /// <summary>
    /// Dialogue/journals topic Record
    /// </summary>
    [DebuggerDisplay("{NAME.EditorId}")]
    public class DIAL : Record
    {
        /// <summary>
        /// Dialogue ID string
        /// </summary>
        public NAME NAME { get; set; }

        /// <summary>
        ///  Dialogue Type
        /// </summary>
        public DATA DATA { get; set; }

        /// <summary>
        /// INFO records that follow this DIAL
        /// </summary>
        public List<INFO> INFO { get; set; }

        public DIAL()
        {
        }

        public DIAL(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        // TODO custom serializadion/deserialization
    }


}
