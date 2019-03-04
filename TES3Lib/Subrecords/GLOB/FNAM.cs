using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;

namespace TES3Lib.Subrecords.GLOB
{
    /// <summary>
    /// Type of global variable
    /// </summary>
    public class FNAM : Subrecord
    {
        public GlobalVariableType VariableType { get; set; }

        public FNAM()
        {
        }

        public FNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            VariableType = (GlobalVariableType)reader.ReadBytes<byte>(base.Data);
        }
    }
}