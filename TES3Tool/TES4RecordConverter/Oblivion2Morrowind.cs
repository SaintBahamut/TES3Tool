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

                            var cellReferences = cellSubBlock.Groups.FirstOrDefault(x => x.Label == cellRecord.FormId);
                            foreach (var childrenType in cellReferences.Groups) //can have 3 with labels: persistent 8; temporaty 9; distant 10;
                            {
                                int refrNumber = 1;
                                foreach (TES4Lib.Records.REFR objectReference in childrenType.Records)
                                {
                                    if (objectReference.NAME == null) continue;
                                    var convertedRecordData = ConvertRecord(TES4Lib.Base.Group.FormIdIndex[objectReference.NAME.BaseFormId]);

                                    if (!ConvertedRecords.ContainsKey(convertedRecordData.Item1))
                                    {
                                        ConvertedRecords.Add(convertedRecordData.Item1, new List<TES3Lib.Base.Record>());
                                    }

                                    ConvertedRecords[convertedRecordData.Item1].Add(convertedRecordData.Item3);
                                    var convertedReference = ConvertREFR(objectReference, convertedRecordData.Item2, refrNumber);
                                    refrNumber++;
                                    convertedCell.REFR.Add(convertedReference);
                                }
                            }

                            tes3.Records.Add(convertedCell);
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, List<TES3Lib.Base.Record>> recordType in ConvertedRecords)
            {
                foreach (var record in recordType.Value)
                {
                    tes3.Records.Add(record);
                }
            }

            return tes3;
        }
    }
}
