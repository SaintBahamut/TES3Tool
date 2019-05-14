using System;
using TES4Lib.Base;
using TES4Lib.Subrecords.ACHR;
using TES4Lib.Subrecords.Shared;
using Utility;

namespace TES4Lib.Records
{
    public class ACHR : Record
    {
        public EDID EDID { get; set; }

        public NAME NAME { get; set; }

        public XRGD XRGD { get; set; }

        public XESP XESP { get; set; }

        public XHRS XHRS { get; set; }

        public XMRC XMRC { get; set; }

        public XSCL XSCL { get; set; }

        public DATA DATA { get; set; }

        public ACHR(byte[] rawData) : base(rawData)
        {
            BuildSubrecords();
        }

        /// <summary>
        /// Builds SubRecords
        /// </summary>
        protected virtual void BuildSubrecords()
        {
            if (!IsImplemented) return;

            var readerData = new ByteReader();
            while (Data.Length != readerData.offset)
            {
                string subrecordName = GetSubrecordName(readerData);
                int subrecordSize = GetSubrecordSize(readerData);

                try
                {

                    ReadSubrecords(readerData, subrecordName, subrecordSize);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error in building {this.GetType().ToString()} on {subrecordName} eighter not implemented or borked {e}");
                    break;
                }
            }
        }
    }
}