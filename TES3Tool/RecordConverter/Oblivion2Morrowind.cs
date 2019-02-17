using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TES3Lib;


namespace ESMLab.RecordConverter
{
    public static class Oblivion2Morrowind
    {
        public static TES3Lib.TES3 ConvertInteriorCells(TES4Lib.TES4 tes4)
        {
            var tes3 = new TES3Lib.TES3();

            //build header
            var header = new TES3Lib.Records.TES3();
            header.HEDR.CompanyName = "TES3Tool\0";
            header.HEDR.Description = "\0";
            header.HEDR.NumRecords = 666;
            header.HEDR.Unknown = 1;
            header.HEDR.Version = 1.3f;
            header.MAST.Filename = "Morrowind.esm\0";
            header.DATA.MasterDataSize = 6666; //should not break but fix that later

            tes3.Records.Add(header);

            //convert cells
            var cellGroupsTop = tes4.Groups.FirstOrDefault(x => x.Label == "CELL");
            if (cellGroupsTop == null)
            {
                Console.WriteLine("no CELL records");
                return null;
            }

            //this is soooo bad
            foreach (var cellBlock in cellGroupsTop.Groups)
            {
                foreach (var cellSubBlock in cellBlock.Groups)
                {
                    foreach (TES4Lib.Records.CELL cellRecord in cellSubBlock.Records)
                    {
                        //hack for now to get SI only
                        if ((cellRecord.EDID.CellEditorId.Contains("SE") || cellRecord.EDID.CellEditorId.Contains("XP")) && cellRecord.FULL != null)
                        {
                            var convertedCell = ConvertCELL(cellRecord);
                            if (convertedCell == null) throw new Exception("Output cell was null");

                            //MAKE SUM REFERENCES BRO
                            var cellReferences = cellSubBlock.Groups.FirstOrDefault(x => x.Label == cellRecord.FormId);
                            foreach (var childrenType in cellReferences.Groups) //can have 3 with labels: persistent 8; temporaty 9; distant 10;
                            {
                                foreach (TES4Lib.Records.REFR objectReference in childrenType.Records)
                                {
                                    var baseReference = TES4Lib.Base.Group.FormIdIndex[objectReference.NAME.BaseFormId];
                                    var convertedReference = ConvertREFR(objectReference, baseReference);

                                    convertedCell.REFR.Add(convertedReference);

                                }
                            }

                            tes3.Records.Add(convertedCell);
                        }
                    }
                }
            }

            return tes3;
        }

        #region records converter
        ///move this to separate files?
        static TES3Lib.Records.LIGH ConvertLIGH(TES4Lib.Records.LIGH obLIGH)
        {
            return null;
        }

        static TES3Lib.Records.STAT ConvertSTAT(TES4Lib.Records.STAT obSTAT)
        {
            return new TES3Lib.Records.STAT()
            {
                MODL = new TES3Lib.Subrecords.STAT.MODL(obSTAT.MODL.ModelFileName),
                NAME = new TES3Lib.Subrecords.STAT.NAME(obSTAT.EDID.EditorId),
            };
        }

        static TES3Lib.Records.CELL ConvertCELL(TES4Lib.Records.CELL obCELL)
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

        static TES3Lib.Records.REFR ConvertREFR(TES4Lib.Records.REFR obREFR, TES4Lib.Base.Record baseObject)
        {
            var mwREFR = new TES3Lib.Records.REFR();

            //FRMR = Object Index(starts at 1)(4 bytes, long)

            //This is used to uniquely identify objects in the cell.  For new files the
            //index starts at 1 and is incremented for each new object added.For modified

            //objects the index is kept the same. TODO SOLVE THIS
            mwREFR.FRMR = new TES3Lib.Subrecords.REFR.FRMR();

            mwREFR.NAME = new TES3Lib.Subrecords.REFR.NAME();
            var cast = (TES4Lib.Records.STAT)baseObject; //dynamic casting this was overlooked!!!!
            mwREFR.NAME.ObjectId = cast.EDID.EditorId;

            if(!IsNull(obREFR.XSCL))
            {
                mwREFR.XSCL = new TES3Lib.Subrecords.REFR.XSCL();
                mwREFR.XSCL.Scale = obREFR.XSCL.Scale;
            }

            if (false)
            {
                //this data should be somewhere in record flag, lateeeer
                mwREFR.DELE = new TES3Lib.Subrecords.REFR.DELE();
            }

            if(!IsNull(obREFR.XTEL))
            {
                mwREFR.DODT = new TES3Lib.Subrecords.REFR.DODT();
            }

        
          
            mwREFR.DNAM = new TES3Lib.Subrecords.REFR.DNAM();
            mwREFR.FLTV = new TES3Lib.Subrecords.REFR.FLTV();
            mwREFR.KNAM = new TES3Lib.Subrecords.REFR.KNAM();
            mwREFR.TNAM = new TES3Lib.Subrecords.REFR.TNAM();
            mwREFR.UNAM = new TES3Lib.Subrecords.REFR.UNAM();
            mwREFR.ANAM = new TES3Lib.Subrecords.REFR.ANAM();
            mwREFR.BNAM = new TES3Lib.Subrecords.REFR.BNAM();
            mwREFR.INTV = new TES3Lib.Subrecords.REFR.INTV();
            mwREFR.NAM9 = new TES3Lib.Subrecords.REFR.NAM9();
            mwREFR.XSOL = new TES3Lib.Subrecords.REFR.XSOL();
            mwREFR.DATA = new TES3Lib.Subrecords.REFR.DATA();

            return mwREFR;
        }

        #endregion



        static int GetTES4DeletedRecordFlag(int recordFlags)
        {
            return recordFlags & 0x20;
        }

        static int GetTES4CantWaitRecordFlag(int recordFlags)
        {
            return recordFlags & 0x080000;
        }

        static int GetTES4IgnoredRecordFlag(int recordFlags)
        {
            return recordFlags & 0x01000;
        }

        /// <summary>
        /// For TES4.Records.CELL.Flag
        /// </summary>
        /// <param name="recordFlags"></param>
        /// <returns></returns>
        static int GetTES4HasWaterCellFlag(int recordFlags)
        {
            return recordFlags & 0x02;
        }

        /// <summary>
        /// For TES4.Records.CELL.Flag
        /// </summary>
        /// <param name="recordFlags"></param>
        /// <returns></returns>
        static int GetTES4BehavesLikeExteriorCellFlag(int recordFlags)
        {
            return recordFlags & 0x80;
        }

        static bool IsNull(object tested) => tested == null ? true : false;

    }
}
