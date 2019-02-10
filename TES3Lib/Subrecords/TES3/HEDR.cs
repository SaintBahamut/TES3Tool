using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Structures.Base;
using Utility;

namespace TES3Lib.Subrecords.TES3
{
    internal class HEDR : Subrecord
    {

        public float Version { get; set; }

        public int Unknown { get; set; }

        //32 bytes
        public string CompanyName { get; set; }

        //256 bytes
        public string Description { get; set; }

        public int NumRecords { get; set; }

        public HEDR(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Version = reader.ReadBytes<float>(base.Data);
            Unknown = reader.ReadBytes<int>(base.Data);
            CompanyName = reader.ReadBytes<string>(base.Data, 32);
            Description = reader.ReadBytes<string>(base.Data, 256);
            NumRecords = reader.ReadBytes<int>(base.Data);
        }

        public override byte[] SerializeSubrecord()
        { 
            //being lazy here...
            byte[] nameBytes = new byte[32];
            byte[] descBytes = new byte[256];
            Encoding.ASCII.GetBytes(CompanyName).CopyTo(nameBytes, 0);
            Encoding.ASCII.GetBytes(Description).CopyTo(descBytes, 0);


            List<byte> data = new List<byte>();
            data.AddRange(BitConverter.GetBytes(Version));
            data.AddRange(BitConverter.GetBytes(Unknown));
            data.AddRange(nameBytes);
            data.AddRange(descBytes);
            data.AddRange(BitConverter.GetBytes(NumRecords));

            var serialized = Encoding.ASCII.GetBytes(Name)
               .Concat(BitConverter.GetBytes(data.Count()))
               .Concat(data).ToArray();
            return serialized;
    }
    }
}