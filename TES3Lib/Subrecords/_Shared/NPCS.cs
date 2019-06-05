using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using Utility;
using Utility.Attributes;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// Spell/Ability/Power Subrecord
    /// </summary>
    [DebuggerDisplay("{SpellId}")]
    public class NPCS : Subrecord
    {
        //private object[] itemIdBytes;

        /// <summary>
        /// Spell/Ability/PowerId
        /// </summary>
        [SizeInBytes(32)]
        public string SpellId { get; set; }

        public NPCS()
        {
        }

        public NPCS(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SpellId = reader.ReadBytes<string>(base.Data, 32);
        }

        //public override byte[] SerializeSubrecord()
        //{
        //    List<byte> data = new List<byte>();

        //    byte[] spellIdBytes = ASCIIEncoding.ASCII.GetBytes(SpellId);
        //    Array.Resize(ref itemIdBytes, 32);
        //    data.AddRange(spellIdBytes);

        //    var serialized = Encoding.ASCII.GetBytes("NPCS")
        //       .Concat(BitConverter.GetBytes(data.Count))
        //       .Concat(data).ToArray();
        //    return serialized;
        //}
    }
}
