using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.SCPT
{
    public class SCHD : Subrecord
    {
        /// <summary>
        /// Script name (31 characters + null termnator)
        /// </summary>
        public new string Name { get; set; }

        public int NumShorts { get; set; }

        public int NumLongs { get; set; }

        public int NumFloats { get; set; }

        public int ScriptDataSize { get; set; }

        public int LocalVarSize { get; set; }

        public SCHD()
        {
        }

        public SCHD(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Name = reader.ReadBytes<string>(base.Data, 32);
            NumShorts = reader.ReadBytes<int>(base.Data);
            NumLongs = reader.ReadBytes<int>(base.Data);
            NumFloats = reader.ReadBytes<int>(base.Data);
            ScriptDataSize = reader.ReadBytes<int>(base.Data);
            LocalVarSize = reader.ReadBytes<int>(base.Data);
        }

        public override byte[] SerializeSubrecord()
        {
            List<byte> data = new List<byte>();
            byte[] nameBytes = ASCIIEncoding.ASCII.GetBytes(Name);
            Array.Resize(ref nameBytes, 32);

            data.AddRange(nameBytes);
            data.AddRange(ByteWriter.ToBytes(NumShorts, NumShorts.GetType()));
            data.AddRange(ByteWriter.ToBytes(NumLongs, NumLongs.GetType()));
            data.AddRange(ByteWriter.ToBytes(NumFloats, NumFloats.GetType()));
            data.AddRange(ByteWriter.ToBytes(ScriptDataSize, ScriptDataSize.GetType()));
            data.AddRange(ByteWriter.ToBytes(LocalVarSize, LocalVarSize.GetType()));

            var serialized = Encoding.ASCII.GetBytes("SCHD")
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
        }
    }
}
