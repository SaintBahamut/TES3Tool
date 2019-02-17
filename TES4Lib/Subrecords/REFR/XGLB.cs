using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.REFR
{
    public class XGLB : Subrecord
    {
        /// <summary>
        /// idk, but we dont need this
        /// </summary>
        public string GlobalVariable { get; set; }

        public XGLB(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            var globalVariableBytes = reader.ReadBytes<byte[]>(base.Data, base.Size);
            GlobalVariable = BitConverter.ToString(globalVariableBytes.Reverse().ToArray()).Replace("-", "");

        }
    }
}