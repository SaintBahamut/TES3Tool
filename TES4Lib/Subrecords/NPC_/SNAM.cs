using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.NPC_
{
    /// <summary>
    /// NPC Faction data
    /// </summary>
    public class SNAM : Subrecord
    {
        /// <summary>
        /// FormId of referenced faction
        /// </summary>
        public string FormId { get; set; }

        public byte Rank { get; set; }

        /// <summary>
        /// ?Always '0x0DB'? Some sort of faction flag?
        /// </summary>
        public byte[] Flag { get; set; }

        public SNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            var formIdBytes = reader.ReadBytes<byte[]>(base.Data, FORMID_LENGTH);
            FormId = BitConverter.ToString(formIdBytes.Reverse().ToArray()).Replace("-", "");
            Rank = reader.ReadBytes<byte>(base.Data);
            Flag = reader.ReadBytes<byte[]>(base.Data);
        }
    }
}