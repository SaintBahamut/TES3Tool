using System;
using System.Collections.Generic;
using System.Linq;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.SCPT
{
    public class SCHD : Subrecord
    {
        /// <summary>
        /// Script name (32 characters!)
        /// </summary>
        public string Name { get; set; }

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
    }
}
