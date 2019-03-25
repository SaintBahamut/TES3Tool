using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TES3Lib.Base;
using TES3Lib.Subrecords.REFR;
using TES3Lib.Subrecords.Shared;
using Utility;

namespace TES3Lib.Records
{
    /// <summary>
    /// Cell reference
    /// </summary>
    public class REFR
    {
        public FRMR FRMR { get; set; }
        public NAME NAME { get; set; }
        public CNAM CNAM { get; set; }
        public INDX INDX { get; set; }
        public XSCL XSCL { get; set; }
        public DELE DELE { get; set; }
        public DODT DODT { get; set; }
        public DNAM DNAM { get; set; }
        public FLTV FLTV { get; set; }
        public KNAM KNAM { get; set; }
        public TNAM TNAM { get; set; }
        public UNAM UNAM { get; set; }
        public ANAM ANAM { get; set; }
        public BNAM BNAM { get; set; }
        public INTV INTV { get; set; }
        public NAM9 NAM9 { get; set; }
        public XSOL XSOL { get; set; }
        public DATA DATA { get; set; }

        public REFR()
        {
        }

        public REFR(byte[] data, ByteReader reader)
        {
            do
            {
                var subrecordName = GetRecordName(reader, data);
                var subrecordSize = GetRecordSize(reader, data);
                try
                {
                    var subrecordProp = this.GetType().GetProperty(subrecordName);
                    object subrecord = Activator.CreateInstance(subrecordProp.PropertyType, new object[] { reader.ReadBytes<byte[]>(data, subrecordSize) });
                    subrecordProp.SetValue(this, subrecord);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error in building {this.GetType().ToString()} on {subrecordName} eighter not implemented or borked {e}");
                    break;
                }

            } while (data.Length != reader.offset && !GetRecordName(reader, data).Equals("FRMR") && !GetRecordName(reader, data).Equals("NAM0"));


        }

        public byte[] SerializeRecord()
        {
            var properties = this.GetType()
                .GetProperties(BindingFlags.Public |
                               BindingFlags.Instance |
                               BindingFlags.DeclaredOnly).OrderBy(x => x.MetadataToken).ToList();


            List<byte> data = new List<byte>();
            foreach (PropertyInfo property in properties)
            {
                var subrecord = (Subrecord)property.GetValue(this);
                if (subrecord == null) continue;

                data.AddRange(subrecord.SerializeSubrecord());
            }

            return data.ToArray();
        }

        private string GetRecordName(ByteReader reader, byte[] data)
        {
            var name = reader.ReadBytes<string>(data, 4);
            reader.ShiftBackBy(4);
            return name;
        }

        private int GetRecordSize(ByteReader reader, byte[] data)
        {
            reader.ShiftForwardBy(4);
            var size = reader.ReadBytes<int>(data) + 8;
            reader.ShiftBackBy(8);
            return size;
        }
    }
}
