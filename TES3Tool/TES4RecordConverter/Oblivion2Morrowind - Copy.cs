//using System;
//using System.Collections.Generic;
//using System.Linq;
//using static TES3Tool.TES4RecordConverter.Records.Helpers;
//using static TES3Tool.TES4RecordConverter.Records.Converters;
//using TES3Tool.TES4RecordConverter.Records;
//using System.Threading.Tasks;

//namespace TES3Tool.TES4RecordConverterx
//{
//    public static class Oblivion2Morrowind
//    {

//        public static TES3Lib.TES3 ConvertInteriorCells(TES4Lib.TES4 tes4)
//        {
//            //convert cells
//            var cellGroupsTop = tes4.Groups.FirstOrDefault(x => x.Label == "CELL");
//            if (cellGroupsTop == null)
//            {
//                Console.WriteLine("no CELL records");
//                return null;
//            }
//            ConvertedRecords.Add("CELL", new List<ConvertedRecordData>());

//            //this is soooo bad
//            foreach (var cellBlock in cellGroupsTop.Groups)
//            {
//                foreach (var cellSubBlock in cellBlock.Groups)
//                {
//                    foreach (TES4Lib.Records.CELL cellRecord in cellSubBlock.Records)
//                    {

//                        if (GetTES4DeletedRecordFlag(cellRecord.Flag) != 0) continue;

//                        //hack for now to get SI only
//                        if ((cellRecord.EDID.CellEditorId.Contains("SE") || cellRecord.EDID.CellEditorId.Contains("XP")) && cellRecord.FULL != null)
//                        {
//                            var convertedCell = ConvertCELL(cellRecord);
//                            if (convertedCell == null) throw new Exception("Output cell was null");

//                            var cellReferences = cellSubBlock.Groups.FirstOrDefault(x => x.Label == cellRecord.FormId);
//                            if (cellReferences == null) continue;

//                            Console.WriteLine($"BEGIN CONVERTING \"{convertedCell.NAME.CellName}\" CELL");
//                            foreach (var childrenType in cellReferences.Groups) //can have 3 with labels: persistent 8; temporaty 9; distant 10;
//                            {
//                                int refrNumber = 1;
//                                foreach (var obRef in childrenType.Records)
//                                {
//                                    if (GetTES4DeletedRecordFlag(obRef.Flag) != 0) continue;

//                                    TES3Lib.Records.REFR mwREFR;
//                                    switch (obRef.GetType().Name)
//                                    {
//                                        case "REFR":
//                                            var obREFR = (TES4Lib.Records.REFR)obRef;

//                                            //walkaround for SOUND references that cant be placed in Morrowind, marking FormId with a prefix
//                                            bool sounRefFlag = false;
//                                            {
//                                                TES4Lib.Base.Record record;
//                                                TES4Lib.TES4.TES4RecordIndex.TryGetValue(obREFR.NAME.BaseFormId, out record);
//                                                if (!IsNull(record) && record.Name.Equals("SOUN"))
//                                                    sounRefFlag = true;
//                                            }
                                          
//                                            if (IsNull(obREFR.NAME)) continue;

//                                            var BaseId = GetBaseIdFromFormId(!sounRefFlag ? obREFR.NAME.BaseFormId : $"a{obREFR.NAME.BaseFormId}");
//                                            if(string.IsNullOrEmpty(BaseId))
//                                            {
//                                                var mwRecordFromREFR = !sounRefFlag ? ConvertRecordFromREFR(obREFR.NAME.BaseFormId) : ConvertSOUNRecordFromREFR(obREFR.NAME.BaseFormId);
//                                                if (IsNull(mwRecordFromREFR)) continue;

//                                                if (!ConvertedRecords.ContainsKey(mwRecordFromREFR.Type)) ConvertedRecords.Add(mwRecordFromREFR.Type, new List<ConvertedRecordData>());
//                                                ConvertedRecords[mwRecordFromREFR.Type].Add(mwRecordFromREFR);

//                                                BaseId = mwRecordFromREFR.EditorId;
//                                            }
                       
//                                            mwREFR = ConvertREFR(obREFR, BaseId, refrNumber);

//                                            convertedCell.REFR.Add(mwREFR);
//                                            refrNumber++;
//                                            break;
//                                        case "ACHR":
//                                            continue;
//                                        case "ACRE":
//                                            continue;
//                                    }
//                                }
//                            }
//                            ConvertedRecords["CELL"].Add(new ConvertedRecordData(cellRecord.FormId,"CELL",convertedCell.NAME.CellName, convertedCell));

//                            Console.WriteLine($"DONE CONVERTING \"{convertedCell.NAME.CellName}\" CELL");
//                        }
//                    }
//                }
//            }

//            Console.WriteLine($"INTERIOR CELL AND REFERENCED RECORDS CONVERSION DONE \n BUILDING TES3 PLUGIN/MASTER INSTANCE");

//            var tes3 = new TES3Lib.TES3();
//            TES3Lib.Records.TES3 header = createTES3HEader();
//            tes3.Records.Add(header);

//            foreach (var record in Enum.GetNames(typeof(TES3Lib.RecordTypes)))
//            {
//                if (!ConvertedRecords.ContainsKey(record)) continue;
//                tes3.Records.InsertRange(tes3.Records.Count, ConvertedRecords[record].Select(x => x.Record));
//            }

//            //dispose references
//            ConvertedRecords = new Dictionary<string, List<ConvertedRecordData>>();

//            return tes3;
//        }

//        private static TES3Lib.Records.TES3 createTES3HEader()
//        {
//            var header = new TES3Lib.Records.TES3();
//            header.HEDR.CompanyName = "TES3Tool\0";
//            header.HEDR.Description = "\0";
//            header.HEDR.NumRecords = 666;
//            header.HEDR.ESMFlag = 0;
//            header.HEDR.Version = 1.3f;
//            header.MAST.Filename = "Morrowind.esm\0";
//            header.DATA.MasterDataSize = 6666; //should not break but fix that later
//            return header;
//        }

//        public static ConvertedRecordData ConvertRecordFromREFR(string BaseFormId)
//        {
//            TES4Lib.Base.Record record;
//            TES4Lib.TES4.TES4RecordIndex.TryGetValue(BaseFormId, out record);
//            if (IsNull(record)) return null ;

//            var mwRecordFromREFR = ConvertRecord(record);

//            return mwRecordFromREFR;
//        }

//        /// <summary>
//        /// Need special routine
//        /// </summary>
//        /// <param name="BaseFormId"></param>
//        /// <returns></returns>
//        public static ConvertedRecordData ConvertSOUNRecordFromREFR(string BaseFormId)
//        {
//            TES4Lib.Base.Record record;
//            TES4Lib.TES4.TES4RecordIndex.TryGetValue(BaseFormId, out record);
//            if (IsNull(record)) return null;

//            var mwACTI = ConvertSOUN2ACTI((TES4Lib.Records.SOUN)record);
//            return new ConvertedRecordData(record.FormId, mwACTI.GetType().Name, mwACTI.NAME.EditorId, mwACTI);
//        }
//    }
//}
