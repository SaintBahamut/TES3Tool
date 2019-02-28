using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.REFR
{
    public class XLOC : Subrecord
    {

        /// <summary>
        /// Lock information (only present if object is a DOOR or CONT, and if object is locked). Only partially understood:
        /// First byte is base lock level for lock (0-100; 100 means key required)
        /// Bytes 5-8 are the formid of the KEYM that opens this lock (00000000 if there is no key)
        /// Last 4 bytes(i.e., bytes 9-12 if 12 bytes long, or bytes 13-16 if 16 bytes long) appear to be flags
        /// 0x00000004 = Is lock leveled
        /// </summary>

        public byte LockLevel { get; set; }
        public byte[] Unknown1 { get; set; }
        public string Key { get; set; }
        public byte[] Unknown2 { get; set; }
        public byte[] Flags { get; set; }


        public XLOC(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            LockLevel = reader.ReadBytes<byte>(base.Data);
            Unknown1 = reader.ReadBytes<byte[]>(base.Data, 3);
            Key = BitConverter.ToString(reader.ReadBytes<byte[]>(base.Data, 4).Reverse().ToArray()).Replace("-", "");

            if (base.Size.Equals(12))
            {
               Flags = reader.ReadBytes<byte[]>(base.Data, 4);
            }
            else
	        {
                Unknown2 = Flags = reader.ReadBytes<byte[]>(base.Data, 4);
                Flags = reader.ReadBytes<byte[]>(base.Data, 4);
            }
        }
    }
}
