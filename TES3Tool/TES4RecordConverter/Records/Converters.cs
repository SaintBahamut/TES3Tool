using static TES3Tool.TES4RecordConverter.Records.Helpers;
using static TES3Tool.TES4RecordConverter.Records.Converters;
using System;

namespace TES3Tool.TES4RecordConverter.Records
{
    public static class Converters
    {
        /// <summary>
        /// Converts Oblivion Record to Morrowind Record
        /// </summary>
        /// <param name="obRecord">oblivion record to convert</param>
        /// <returns>Record Type:EditorID:Record</returns>
        internal static (string,string, TES3Lib.Base.Record) ConvertRecord(TES4Lib.Base.Record obRecord)
        {
            var recordType = obRecord.GetType().Name;

            switch (recordType)
            {
                case "STAT":
                    var mwRecord = ConvertSTAT((TES4Lib.Records.STAT)obRecord);
                    return (mwRecord.GetType().Name, mwRecord.NAME.EditorId, mwRecord);
                default:
                    return ("","",null);
            }
        }



        static TES3Lib.Records.LIGH ConvertLIGH(TES4Lib.Records.LIGH obLIGH)
        {
            return null;
        }

        static TES3Lib.Records.STAT ConvertSTAT(TES4Lib.Records.STAT obSTAT)
        {
            return new TES3Lib.Records.STAT()
            {
                MODL = new TES3Lib.Subrecords.Shared.MODL() { ModelPath = obSTAT.MODL.ModelFileName },
                NAME = new TES3Lib.Subrecords.Shared.NAME() { EditorId = obSTAT.EDID.EditorId },
            };
        }

        internal static TES3Lib.Records.CELL ConvertCELL(TES4Lib.Records.CELL obCELL)
        {
            if (GetTES4DeletedRecordFlag(obCELL.Flag) == 0x20) return null; //we dont need deleted records for conversion

            var mwCELL = new TES3Lib.Records.CELL();
            mwCELL.NAME = new TES3Lib.Subrecords.CELL.NAME();
            mwCELL.NAME.CellName = obCELL.FULL.CellName;

            mwCELL.DATA = new TES3Lib.Subrecords.CELL.DATA();

            int interior = 0x01;
            int hasWater = 0x02 & obCELL.Flag;
            int illegalToSleep = 0x20 & obCELL.Flag;
            int behaveLikeEx = 0x80 & obCELL.Flag;
            mwCELL.DATA.Flag = 0 | interior | hasWater | (illegalToSleep == 0 ? 0 : 0x04) | behaveLikeEx;


            if (obCELL.XCLC != null) //exterior only
            {
                mwCELL.DATA.GridX = obCELL.XCLC.GridX;
                mwCELL.DATA.GridY = obCELL.XCLC.GridY;
            }


            //cell.RGNN = new TES3Lib.Subrecords.CELL.RGNN(); need dehardcode regions on my on my own, idk if ill need that anyway

            // not needed? cell.NAM0 = new TES3Lib.Subrecords.CELL.NAM0();
            // exterior only cell.NAM5 = new TES3Lib.Subrecords.CELL.NAM5();

            mwCELL.WHGT = new TES3Lib.Subrecords.CELL.WHGT();
            if (obCELL.XCLW != null) //exterior only
            {
                mwCELL.WHGT.WaterHeight = obCELL.XCLW.WaterHeight;
            }

            mwCELL.AMBI = new TES3Lib.Subrecords.CELL.AMBI();
            if (obCELL.XCLL != null)
            {
                mwCELL.AMBI.AmbientColor = obCELL.XCLL.Ambient;
                mwCELL.AMBI.SunlightColor = obCELL.XCLL.Directional; //mabye not a good idea
                mwCELL.AMBI.FogColor = obCELL.XCLL.Fog; //missing
                mwCELL.AMBI.FogDensity = 1.0f;
            }
            else
            {
                mwCELL.AMBI.AmbientColor = 0;
                mwCELL.AMBI.SunlightColor = 0;
                mwCELL.AMBI.FogColor = 0;
                mwCELL.AMBI.FogDensity = 1.0f;
            }

            return mwCELL;
        }

        internal static TES3Lib.Records.REFR ConvertREFR(TES4Lib.Records.REFR obREFR, string baseId, int refrNumber)
        {
            var mwREFR = new TES3Lib.Records.REFR();

            mwREFR.FRMR = new TES3Lib.Subrecords.REFR.FRMR();
            mwREFR.FRMR.ObjectIndex = refrNumber;

            mwREFR.NAME = new TES3Lib.Subrecords.REFR.NAME();
            mwREFR.NAME.ObjectId = baseId;

            if (!IsNull(obREFR.XSCL))
            {
                mwREFR.XSCL = new TES3Lib.Subrecords.REFR.XSCL();
                mwREFR.XSCL.Scale = obREFR.XSCL.Scale;
            }

            //if (false)
            //{
            //    //this data should be somewhere in record flag, not relevant, at lest for now
            //    mwREFR.DELE = new TES3Lib.Subrecords.REFR.DELE();
            //}

            if (!IsNull(obREFR.XTEL))
            {
                mwREFR.DODT = new TES3Lib.Subrecords.REFR.DODT();
                mwREFR.DODT.XPos = obREFR.XTEL.DestLocX;
                mwREFR.DODT.YPos = obREFR.XTEL.DestLocY;
                mwREFR.DODT.ZPos = obREFR.XTEL.DestLocZ;
                mwREFR.DODT.XRotate = obREFR.XTEL.DestRotX;
                mwREFR.DODT.YRotate = obREFR.XTEL.DestRotY;
                mwREFR.DODT.ZRotate = obREFR.XTEL.DestRotX;

                mwREFR.DNAM = new TES3Lib.Subrecords.REFR.DNAM();
                //mwREFR.DNAM.DoorName = obREFR.XTEL.DestinationDoorReference; //hold right there criminal scum TODO: need dig on this
            }

            if(!IsNull(obREFR.XLOC))
            {
                mwREFR.FLTV = new TES3Lib.Subrecords.REFR.FLTV(); //lock level
                mwREFR.KNAM = new TES3Lib.Subrecords.REFR.KNAM(); //key data
                //locks data

              mwREFR.TNAM = new TES3Lib.Subrecords.REFR.TNAM(); //traps, oblvion doesent have them
            }

            // uneeded?
            ///mwREFR.UNAM = new TES3Lib.Subrecords.REFR.UNAM();
            
            //owner data laaateeer
            //mwREFR.ANAM = new TES3Lib.Subrecords.REFR.ANAM();
            //mwREFR.BNAM = new TES3Lib.Subrecords.REFR.BNAM();

            mwREFR.INTV = new TES3Lib.Subrecords.REFR.INTV();
            mwREFR.INTV.NumberOfUses = 1;

            //no one knows what this is
            //mwREFR.NAM9 = new TES3Lib.Subrecords.REFR.NAM9();

            //soul data, obREFR dont have it?
            //mwREFR.XSOL = new TES3Lib.Subrecords.REFR.XSOL();

            
            if(!IsNull(obREFR.DATA))
            {
                mwREFR.DATA = new TES3Lib.Subrecords.REFR.DATA();
                mwREFR.DATA.XPos = obREFR.DATA.LocX;
                mwREFR.DATA.YPos = obREFR.DATA.LocY;
                mwREFR.DATA.ZPos = obREFR.DATA.LocZ;
                mwREFR.DATA.XRotate = obREFR.DATA.RotX;
                mwREFR.DATA.YRotate = obREFR.DATA.RotY;
                mwREFR.DATA.ZRotate = obREFR.DATA.RotZ;
            }

            return mwREFR;
        }  

        //internal static TES3Lib.Records.REFR ConvertACHR(TES4Lib.Records.REFR obREFR, string baseId, int refrNumber)
        //{

        //}

        //internal static TES3Lib.Records.REFR ConvertACRE(TES4Lib.Records.REFR obREFR, string baseId, int refrNumber)
        //{

        //}
    }
}
