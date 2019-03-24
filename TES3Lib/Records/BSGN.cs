using System.Collections.Generic;
using TES3Lib.Base;
using TES3Lib.Subrecords.BSGN;
using TES3Lib.Subrecords.Shared;

namespace TES3Lib.Records
{
    public class BSGN : Record
    {
        public NAME NAME { get; set; }

        public FNAM FNAM { get; set; }

        public TNAM TNAM { get; set; }

        public DESC DESC { get; set; }

        public List<NPCS> NPCS { get; set; }

        public BSGN()
        {
        }

        public BSGN(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        //public override byte[] SerializeRecord()
        //{
        //    var properties = this.GetType()
        //        .GetProperties(System.Reflection.BindingFlags.Public |
        //                       System.Reflection.BindingFlags.Instance |
        //                       System.Reflection.BindingFlags.DeclaredOnly).OrderBy(x => x.MetadataToken).ToList();

        //    List<byte> data = new List<byte>();
        //    foreach (PropertyInfo property in properties)
        //    {

        //        if (property.Name == "NPCS")
        //        {
        //            List<byte> ranks = new List<byte>();
        //            foreach (var npcs in NPCS)
        //            {
        //                ranks.AddRange(npcs.SerializeSubrecord());
        //            }
        //            data.AddRange(ranks.ToArray());
        //            continue;
        //        }

        //        var subrecord = (Subrecord)property.GetValue(this);
        //        if (subrecord == null) continue;

        //        data.AddRange(subrecord.SerializeSubrecord());
        //    }

        //    return Encoding.ASCII.GetBytes(this.GetType().Name)
        //        .Concat(BitConverter.GetBytes(data.Count()))
        //        .Concat(BitConverter.GetBytes(Header))
        //        .Concat(BitConverter.GetBytes(Flags))
        //        .Concat(data).ToArray();
        //}
    }
}
