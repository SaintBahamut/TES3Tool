using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.SCPT
{
    /// <summary>
    /// List of all the local script variables seperated
    /// by '\0' NULL characters.
    /// </summary>
    public class SCVR : Subrecord
    {
        public string LocalScriptVariables { get; set; }

        public SCVR()
        {
        }

        public SCVR(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            LocalScriptVariables = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
