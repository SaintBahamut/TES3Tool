using System;
using System.Reflection;
using TES3Lib.Subrecords.REFR;
using Utility;

namespace TES3Lib.Records
{
    /// <summary>
    /// Not real TES3 data type, just bag for cell object references
    /// </summary>
    public class REFR
    {
        public FRMR FRMR { get; set; }
        public NAME NAME { get; set; }
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


        public REFR(byte[] data, ByteReader reader)
        {       
            try
            {
                do
                {
                    var subrecordName = GetRecordName(reader, data);
                    var subrecordSize = GetRecordSize(reader, data);
                    var subrecordProp = this.GetType().GetProperty(subrecordName);
                   //Console.WriteLine($"{subrecordName} offset:{reader.offset} subsize:{subrecordSize} datasize:{data.Length}");
                
                    object subrecord = Activator.CreateInstance(subrecordProp.PropertyType, new object[] { reader.ReadBytes<byte[]>(data, subrecordSize) });
                    subrecordProp.SetValue(this, subrecord);
 
                } while (data.Length != reader.offset && !GetRecordName(reader, data).Equals("FRMR") && !GetRecordName(reader, data).Equals("NAM0"));
            }
            catch (Exception e)
            {
                Console.WriteLine($"REFR parse failure! {e}");
                throw;
            }

            //int size;
            //string name;

            //name = GetRecordName(reader, data);
            //size = GetRecordSize(reader, data);
            //FRMR = new FRMR(reader.ReadBytes<byte[]>(data, size));

            //name = GetRecordName(reader, data);
            //size = GetRecordSize(reader, data);
            //NAME = new NAME(reader.ReadBytes<byte[]>(data, size));

            //name = GetRecordName(reader, data);
            //size = GetRecordSize(reader, data);
            //XSCL = new XSCL(reader.ReadBytes<byte[]>(data, size));

            //WHGT = name.Equals("WHGT") ? new WHGT(reader.ReadBytes<byte[]>(base.Data, size)) : null;
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
