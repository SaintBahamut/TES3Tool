using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.INFO
{
    /// <summary>
    /// String that represents function and variable choice
    /// </summary>
    public class SCVR : Subrecord
    {
        public string FunctionVariables { get; set; }

        // TODO: decode this string into more friendly variables
        public byte Index { get; set; }

        public byte Type { get; set; }

        public short Function { get; set; }

        public byte CompareOperator { get; set; }

        public byte[] VariableName { get; set; }

        public SCVR()
        {
        }

        public SCVR(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            FunctionVariables = reader.ReadBytes<string>(base.Data, base.Size);
        }
    }
}
