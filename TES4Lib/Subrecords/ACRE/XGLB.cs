using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.ACRE
{
    public class XGLB : Subrecord
    {
        public string GlobalVariableFormId { get; set; }

        public XGLB(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            GlobalVariableFormId = BitConverter.ToString(reader.ReadBytes<byte[]>(base.Data, FORMID_LENGTH).Reverse().ToArray()).Replace("-", "");
        }
    }
}
