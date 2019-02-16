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
        static TES3Lib.TES3 ConvertInteriorCells(TES4Lib.TES4 tes4)
        {         
            var tes3 = new TES3Lib.TES3();

            //build header
            var header = new TES3Lib.Records.TES3();
            header.HEDR.CompanyName = "TES3Tool\0";
            header.HEDR.Description = "\0";
            header.HEDR.NumRecords = 666;
            header.HEDR.Unknown = 1;
            header.HEDR.Version = 1.2f;
            header.MAST.Filename = "Morrowind.esm\0";
            header.DATA.MasterDataSize = 6666; //should not break but fix that later

            //convert cells

           



            return tes3;
        }

        static TES3Lib.Records.LIGH ConvertLIGH(TES4Lib.Records.LIGH obLIGH)
        {
            return null;
        }

        static TES3Lib.Records.STAT ConvertREF(TES4Lib.Records.STAT obSTAT)
        {
            return new TES3Lib.Records.STAT()
            {
                MODL = new TES3Lib.Subrecords.STAT.MODL(obSTAT.MODL.ModelFileName),
                NAME = new TES3Lib.Subrecords.STAT.NAME(obSTAT.EDID.EditorId),
            };
        }

        //   0x01 = Interior?
        //0x02 = Has Water
        //   0x04 = Illegal to Sleep here
        //0x80 = Behave like exterior(Tribunal)
        //int

            //byte
        //   0x01 = Can't travel from here
        //   0x02 = Has water
        //   0x08 = Force hide land(exterior cell), Oblivion interior(interior cell)
        //   0x20 = Public place
        //   0x40 = Hand changed
        //   0x80 = Behave like exterior
        static TES3Lib.Records.CELL ConvertCELL(TES4Lib.Records.CELL obCell)
        {
            if (GetTES4DeletedRecordFlag(obCell.Flag) == 0x20) return null; //we dont need deleted records for conversion

            var cell = new TES3Lib.Records.CELL();
            cell.NAME = new TES3Lib.Subrecords.CELL.NAME();
            cell.NAME.CellName = obCell.FULL.CellName;

            cell.DATA = new TES3Lib.Subrecords.CELL.DATA();
            cell.DATA.Flag = cell.DATA.Flag;

            int interior = 0x01;
            int hasWater = 0x02 & obCell.Flag;
            int illegalToSleep = 0x20 & obCell.Flag;
            int behaveLikeEx = 0x80 & obCell.Flag;
            cell.DATA.Flag = 0 | interior | hasWater | (illegalToSleep == 0 ? 0 : 0x04) | behaveLikeEx;

            cell.DATA.GridX = (int)obCell.XCLC.GridX;
            cell.DATA.GridY = (int)obCell.XCLC.GridY;

            //cell.RGNN = new TES3Lib.Subrecords.CELL.RGNN(); need dehardcode regions on my on my own, idk if ill need that anyway

            // not needed? cell.NAM0 = new TES3Lib.Subrecords.CELL.NAM0();
            // exterior only cell.NAM5 = new TES3Lib.Subrecords.CELL.NAM5();

            cell.WHGT = new TES3Lib.Subrecords.CELL.WHGT();
            cell.WHGT.WaterHeight = obCell.XCLW.WaterHeight;

            cell.AMBI = new TES3Lib.Subrecords.CELL.AMBI();
            cell.AMBI.AmbientColor = obCell.XCLL.Ambient;
            cell.AMBI.SunlightColor = obCell.XCLL.Directional; //mabye not a good idea
            cell.AMBI.FogColor = obCell.XCLL.Fog; //missing
            //cell.AMBI.FogDensity

            //CONVERT REFERENCES



            return null;
        }

        static int GetTES4DeletedRecordFlag(int recordFlags)
        {
           return recordFlags & 0x20;
        }

        static int GetTES4CantWaitRecordFlag (int recordFlags)
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



        static TES3Lib.Records.REFR ConvertREFR(TES4Lib.Records.REFR obLIGH)
        {
            return null;
        }

      
    }
}
