using TES3Lib.Base;
using TES3Lib.Subrecords.Shared;

namespace TES3Lib.Records
{
    /// <summary>
    /// Activator Record
    /// </summary>
    public class ACTI : Record
    {
        /// <summary>
        /// Activators EditorId
        /// </summary>
        public NAME NAME { get; set; }

        /// <summary>
        /// Activaors model path
        /// </summary>
        public MODL MODL { get; set; }

        /// <summary>
        /// Activators display name
        /// </summary>
        public FNAM FNAM { get; set; }

        /// <summary>
        /// Activators attached script
        /// </summary>
        public SCRI SCRI { get; set; }

        public ACTI()
        {

        }

        public ACTI(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }
    }
}
