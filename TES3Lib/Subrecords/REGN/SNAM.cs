using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.REGN
{
    public class SNAM : Subrecord
    {
        /// <summary>
        /// Id of random sound played in region (alwasy 32 chars)
        /// </summary>
        public string SoundId { get; set; }

        public byte Chance { get; set; }

        public SNAM()
        {

        }

        public SNAM(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            SoundId = reader.ReadBytes<string>(base.Data, 32);
            Chance = reader.ReadBytes<byte>(base.Data);
        }

        public override byte[] SerializeSubrecord()
        {
            List<byte> data = new List<byte>();

            byte[] soundIdbytes = ASCIIEncoding.ASCII.GetBytes(SoundId);
            Array.Resize(ref soundIdbytes, 32);
            data.AddRange(soundIdbytes);
            data.Add(Chance);

            var serialized = Encoding.ASCII.GetBytes(this.GetType().Name)
               .Concat(BitConverter.GetBytes(data.Count))
               .Concat(data).ToArray();
            return serialized;
        }
    }
}
