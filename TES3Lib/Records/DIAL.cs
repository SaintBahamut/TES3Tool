using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Subrecords.DIAL;
using TES3Lib.Subrecords.Shared;

namespace TES3Lib.Records
{
    /// <summary>
    /// Dialogue/journals topic 
    /// </summary>
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
