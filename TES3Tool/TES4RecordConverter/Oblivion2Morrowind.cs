using System;
using System.Collections.Generic;
using System.Linq;
using static TES3Tool.TES4RecordConverter.Records.Helpers;
using static TES3Tool.TES4RecordConverter.Records.Converters;
using TES3Tool.TES4RecordConverter.Records;

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

                            Console.WriteLine($"BEGIN CONVERTING \"{convertedCell.NAME.CellName}\" CELL");
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
                                            if (IsNull(obREFR.NAME)) continue;

                                            var mwRecordFromREFR = ConvertRecordFromREFR(obREFR.NAME.BaseFormId);
                                            if (IsNull(mwRecordFromREFR)) continue;

                                            mwREFR = ConvertREFR(obREFR, mwRecordFromREFR.EditorId, refrNumber);

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
                            Console.WriteLine($"DONE CONVERTING \"{convertedCell.NAME.CellName}\" CELL");
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

        public static ConvertedRecordResult ConvertRecordFromREFR(string BaseFormId)
        {
            var obRecordFromREFR = TES4Lib.Base.Group.FormIdIndex.FirstOrDefault(x => x.Key.Equals(BaseFormId));
            if (IsNull(obRecordFromREFR.Value)) return null;

            //convert ob2mw
            var mwRecordFromREFR = ConvertRecord(obRecordFromREFR.Value);

            //check if key like this exist
            if (!ConvertedRecords.ContainsKey(mwRecordFromREFR.Type)) ConvertedRecords.Add(mwRecordFromREFR.Type, new List<TES3Lib.Base.Record>());

            //check if record like this already added (i know order is fucked, but not really have idea how to make it better atm)
            if (!ConvertedRecords[mwRecordFromREFR.Type].Any(x => !IsNull(x.NAME) & x.NAME.Equals(mwRecordFromREFR.EditorId)))
            {
                ConvertedRecords[mwRecordFromREFR.Type].Add(mwRecordFromREFR.Record);
            }

            return mwRecordFromREFR;
        }


    }
}
