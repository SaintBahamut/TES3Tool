using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TES3Lib.Base;
using TES3Lib.Enums.Flags;
using TES3Lib.Subrecords.CELL;
using static Utility.Common;
using Utility;
using TES3Lib.Subrecords.Shared;

namespace TES3Lib.Records
{
    /// <summary>
    /// Cell record
    /// </summary>
    public class CELL : Record
    {
        /// <summary>
        /// EditorId (Cell name)
        /// </summary>
        public NAME NAME { get; set; }

        /// <summary>
        /// Cell grid data and flags
        /// </summary>
        public DATA DATA { get; set; }

        /// <summary>
        /// Cell region
        /// </summary>
        public RGNN RGNN { get; set; }

        /// <summary>
        /// Water level (?)
        /// </summary>
        public INTV INTV { get; set; }

        /// <summary>
        /// Number of references in cell
        /// </summary>
        public NAM0 NAM0 { get; set; }

        /// <summary>
        /// Map color
        /// Dont really know what this does
        /// </summary>
        public NAM5 NAM5 { get; set; }

        /// <summary>
        /// Interior water level
        /// </summary>
        public WHGT WHGT { get; set; }

        /// <summary>
        /// Light setting
        /// </summary>
        public AMBI AMBI { get; set; }

        /// <summary>
        /// Cell references
        /// </summary>
        public List<REFR> REFR { get; set; }

        public CELL()
        {
            REFR = new List<REFR>();
        }

        public CELL(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        public override void BuildSubrecords()
        {
            var readerData = new ByteReader();
            REFR = new List<REFR>();
            while (Data.Length != readerData.offset)
            {
                var subrecordName = GetRecordName(readerData);
                var subrecordSize = GetRecordSize(readerData);
                try
                {
                    if (subrecordName.Equals("FRMR"))
                    {
                        var refrListType = this.GetType().GetProperty("REFR");
                        var reflist = (List<REFR>)refrListType.GetValue(this);
                        reflist.Add(new REFR(Data, readerData));
                    }
                    else
                    {
                        var subrecordProp = this.GetType().GetProperty(subrecordName);
                        var subrecord = Activator.CreateInstance(subrecordProp.PropertyType, new object[] { readerData.ReadBytes<byte[]>(Data, subrecordSize) });
                        subrecordProp.SetValue(this, subrecord);
                    }
                }
                catch (Exception e)
                {
                    if (!IsNull(NAME))
                    {
                        Console.WriteLine(NAME.EditorId);
                    }
                    Console.WriteLine($"error in building {this.GetType().ToString()} on {subrecordName} either not implemented or borked {e}");
                    break;
                }
            }
        }

        public override byte[] SerializeRecord()
        {
            var properties = this.GetType()
                .GetProperties(BindingFlags.Public |
                               BindingFlags.Instance |
                               BindingFlags.DeclaredOnly).OrderBy(x => x.MetadataToken).ToList();
    
            List<byte> data = new List<byte>();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "REFR") continue;
                var subrecord = (Subrecord)property.GetValue(this);
                if (IsNull(subrecord)) continue;

                data.AddRange(subrecord.SerializeSubrecord());
            }

            if(REFR.Count() > 0)
            {
                List<byte> cellReferences = new List<byte>();
                foreach (var refr in REFR)
                {
                    cellReferences.AddRange(refr.SerializeRecord());
                }
                data.AddRange(cellReferences.ToArray());
            }

            return Encoding.ASCII.GetBytes(this.GetType().Name)
                .Concat(BitConverter.GetBytes(data.Count()))
                .Concat(BitConverter.GetBytes(Header))
                .Concat(BitConverter.GetBytes(SerializeFlag()))
                .Concat(data).ToArray();
        }

        public override bool Equals(object obj)
        {
            var cell = obj as CELL;
            if (cell.DATA.Flags.Contains(CellFlag.IsInteriorCell))
            {
                return this.NAME.EditorId.Equals(cell.NAME.EditorId);
            }
            return this.DATA.GridX.Equals(cell.DATA.GridX) && this.DATA.GridY.Equals(cell.DATA.GridY);
        }

        public override string GetEditorId()
        {
            if (DATA.Flags.Contains(CellFlag.IsInteriorCell))
            {
                return !IsNull(NAME) ? NAME.EditorId : null;
            }
            else
            {
                return $"({DATA.GridX},{DATA.GridY})";
            }
        }
    }
}
