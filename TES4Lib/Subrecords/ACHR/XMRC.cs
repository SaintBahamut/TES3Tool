using System;
using System.Collections.Generic;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.ACHR
{
    /// <summary>
    /// Merchant container
    /// </summary>
    public class XMRC : Subrecord
    {
        public string MerchantContainerFormId { get; set; }

        public uint Flags { get; set; }

        public XMRC(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            MerchantContainerFormId = BitConverter.ToString(reader.ReadBytes<byte[]>(base.Data, FORMID_LENGTH).Reverse().ToArray()).Replace("-", "");
        }
    }
}
