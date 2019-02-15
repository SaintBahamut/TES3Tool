using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMLab.RecordConverter
{
    public static class Oblivion2Morrowind
    {
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
            var cell = new TES3Lib.Records.CELL();
            cell.NAME = new TES3Lib.Subrecords.CELL.NAME();
            cell.NAME.CellName = obCell.FULL.CellName;

            cell.DATA = new TES3Lib.Subrecords.CELL.DATA();
            //cell.DATA.Flag
            //cell.DATA.GridX
            //cell.DATA.GridY

            cell.RGNN = new TES3Lib.Subrecords.CELL.RGNN();

            // not needed? cell.NAM0 = new TES3Lib.Subrecords.CELL.NAM0();
            // exterior only cell.NAM5 = new TES3Lib.Subrecords.CELL.NAM5();

            cell.WHGT = new TES3Lib.Subrecords.CELL.WHGT();

            cell.AMBI = new TES3Lib.Subrecords.CELL.AMBI();

            //CONVERT REFERENCES



            return null;
        }

        static TES3Lib.Records.REFR ConvertREFR(TES4Lib.Records.REFR obLIGH)
        {
            return null;
        }

      
    }
}
