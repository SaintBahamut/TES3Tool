using System;
using System.Collections.Generic;
using System.Linq;
using static TES3Tool.TES4RecordConverter.Records.Helpers;
using static TES3Tool.TES4RecordConverter.Records.Converters;


namespace TES3Tool.TES4RecordConverter
{
    public static class Oblivion2Morrowind
    {
        public static Dictionary<string, List<TES3Lib.Base.Record>> ConvertedRecords = new Dictionary<string, List<TES3Lib.Base.Record>>(); 

        public static TES3Lib.TES3 ConvertInteriorCells(TES4Lib.TES4 tes4)
        {
            var tes3 = new TES3Lib.TES3();

            //build header
            var header = new TES3Lib.Records.TES3();
            header.HEDR.CompanyName = "TES3Tool\0";
            header.HEDR.Description = "\0";
            header.HEDR.NumRecords = 666;
            header.HEDR.ESMFlag = 0;
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

                        if (GetTES4DeletedRecordFlag(cellRecord.Flag) != 0) continue;

                        //hack for now to get SI only
                        if ((cellRecord.EDID.CellEditorId.Contains("SE") || cellRecord.EDID.CellEditorId.Contains("XP")) && cellRecord.FULL != null)
                        {
                            var convertedCell = ConvertCELL(cellRecord);
                            if (convertedCell == null) throw new Exception("Output cell was null");

                          

                            var cellReferences = cellSubBlock.Groups.FirstOrDefault(x => x.Label == cellRecord.FormId);
                            if (cellReferences == null) continue;

                            foreach (var childrenType in cellReferences.Groups) //can have 3 with labels: persistent 8; temporaty 9; distant 10;
                            {
                                int refrNumber = 1;
                                foreach (var obRef in childrenType.Records)
                                {
                                    if (GetTES4DeletedRecordFlag(obRef.Flag) != 0) continue;

                                    TES3Lib.Records.REFR mwREFR;
                                    switch (obRef.GetType().Name)
                                    {
                                        case "REFR":
                                            var obREFR = (TES4Lib.Records.REFR)obRef;
                                            if (obREFR.NAME == null) continue;

                                            var obRecordFromREFR = TES4Lib.Base.Group.FormIdIndex.FirstOrDefault(x => x.Key == obREFR.NAME.BaseFormId);
                                            if (obRecordFromREFR.Value == null) continue;

                                            //var obRecordFromREFR = TES4Lib.Base.Group.FormIdIndex[obREFR.NAME.BaseFormId];
                                            var mwRecordFromREFR = ConvertRecord(obRecordFromREFR.Value);

                                            if (!ConvertedRecords.ContainsKey(mwRecordFromREFR.Item1)) ConvertedRecords.Add(mwRecordFromREFR.Item1, new List<TES3Lib.Base.Record>());

                                            ConvertedRecords[mwRecordFromREFR.Item1].Add(mwRecordFromREFR.Item3);

                                            mwREFR = ConvertREFR(obREFR, mwRecordFromREFR.Item2, refrNumber);
                                            convertedCell.REFR.Add(mwREFR);
                                            refrNumber++;
                                            break;
                                        case "ACHR":
                                            continue;
                                        case "ACRE":
                                            continue;   
                                    } 
                                }
                            }

                            tes3.Records.Add(convertedCell);
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, List<TES3Lib.Base.Record>> recordType in ConvertedRecords)
            {
                tes3.Records.InsertRange(1, recordType.Value);
            }

            return tes3;
        }

      

    }
}
