using System;
using System.Collections.Generic;
using System.Linq;
using static Utility.Common;
using static TES3Tool.TES4RecordConverter.Records.Helpers;
using static TES3Tool.TES4RecordConverter.Records.Converters;
using TES3Tool.TES4RecordConverter.Records;
using TES4Lib.Enums;
using TES4Lib.Base;

namespace TES3Tool.TES4RecordConverter
{
    public static class Oblivion2Morrowind
    {
        public static TES3Lib.TES3 ConvertInteriorsAndExteriors(TES4Lib.TES4 tes4)
        {
            ConvertedRecords.Add("CELL", new List<ConvertedRecordData>());

            ConvertInteriorCells(tes4);
            ConvertExteriorCells(tes4);
                   
            UpdateDoorReferences();

            var tes3 = new TES3Lib.TES3();
            TES3Lib.Records.TES3 header = createTES3HEader();
            tes3.Records.Add(header);

            foreach (var record in Enum.GetNames(typeof(TES3Lib.RecordTypes)))
            {
                if (!ConvertedRecords.ContainsKey(record)) continue;
                tes3.Records.InsertRange(tes3.Records.Count, ConvertedRecords[record].Select(x => x.Record));
            }

            //dispose helper structures
            ConvertedRecords = new Dictionary<string, List<ConvertedRecordData>>();
            CellReferences = new List<ConvertedCellReference>();
            DoorReferences = new List<TES3Lib.Records.REFR>();

            return tes3;
        }

        public static TES3Lib.TES3 ConvertInteriors(TES4Lib.TES4 tes4)
        {
            ConvertedRecords.Add("CELL", new List<ConvertedRecordData>());

            ConvertInteriorCells(tes4);

            UpdateDoorReferences();

            Console.WriteLine($"INTERIOR CELL AND REFERENCED RECORDS CONVERSION DONE \n BUILDING TES3 PLUGIN/MASTER INSTANCE");

            var tes3 = new TES3Lib.TES3();
            TES3Lib.Records.TES3 header = createTES3HEader();
            tes3.Records.Add(header);

            foreach (var record in Enum.GetNames(typeof(TES3Lib.RecordTypes)))
            {
                if (!ConvertedRecords.ContainsKey(record)) continue;
                tes3.Records.InsertRange(tes3.Records.Count, ConvertedRecords[record].Select(x => x.Record));
            }

            //dispose helper structures
            ConvertedRecords = new Dictionary<string, List<ConvertedRecordData>>();
            CellReferences = new List<ConvertedCellReference>();
            DoorReferences = new List<TES3Lib.Records.REFR>();

            return tes3;
        }

        public static TES3Lib.TES3 ConvertExteriors(TES4Lib.TES4 tes4)
        {
            ConvertedRecords.Add("CELL", new List<ConvertedRecordData>());

            ConvertExteriorCells(tes4);

            Console.WriteLine($"EXTERIOR CELL AND REFERENCED RECORDS CONVERSION DONE \n BUILDING TES3 PLUGIN/MASTER INSTANCE");

            var tes3 = new TES3Lib.TES3();
            TES3Lib.Records.TES3 header = createTES3HEader();
            tes3.Records.Add(header);

            foreach (var record in Enum.GetNames(typeof(TES3Lib.RecordTypes)))
            {
                if (!ConvertedRecords.ContainsKey(record)) continue;
                tes3.Records.InsertRange(tes3.Records.Count, ConvertedRecords[record].Select(x => x.Record));
            }

            //dispose helper structures
            ConvertedRecords = new Dictionary<string, List<ConvertedRecordData>>();
            CellReferences = new List<ConvertedCellReference>();
            DoorReferences = new List<TES3Lib.Records.REFR>();

            return tes3;
        }

        private static void ConvertInteriorCells(TES4Lib.TES4 tes4)
        {
            //convert cells
            var cellGroupsTop = tes4.Groups.FirstOrDefault(x => x.Label == "CELL");
            if (IsNull(cellGroupsTop))
            {
                Console.WriteLine("no CELL records");
                return;
            }
            
            foreach (var cellBlock in cellGroupsTop.Groups)
            {
                ProcessInteriorSubBlocks(cellBlock);
            }

            
        }

        private static void ConvertExteriorCells(TES4Lib.TES4 tes4)
        {

            //convert cells
            var wrldGroupsTop = tes4.Groups.FirstOrDefault(x => x.Label == "WRLD");
            if (IsNull(wrldGroupsTop))
            {
                Console.WriteLine("no WRLD records");
                return;
            }

            foreach (TES4Lib.Records.WRLD wrld in wrldGroupsTop.Records)
            {
                Console.WriteLine($"Converting worldspace {wrld.FULL.DisplayName}");

                var wrldFormId = wrld.FormId;
                var worldChildren = wrldGroupsTop.Groups.FirstOrDefault(x => x.Label == wrldFormId);

                if (IsNull(worldChildren))
                {
                    Console.WriteLine($"{wrld.FULL.DisplayName} has no WorldChildren");
                    continue;
                }

                //Here are records ROAD

                foreach (var exteriorCellBlock in worldChildren.Groups)
                {
                    if (exteriorCellBlock.Type.Equals(GroupLabel.CellChildren)) continue;

                    ProcessExteriorSubBlocks(exteriorCellBlock);
                }

                //process extra CELL record
                var wrldCell = worldChildren.Records.FirstOrDefault(x => x.Name.Equals("CELL")) as TES4Lib.Records.CELL;

                if (!IsNull(wrldCell))
                {
                    var convertedCell = ConvertCELL(wrldCell);
                    var wrldCellChildren = worldChildren.Groups.FirstOrDefault(x => x.Type.Equals(GroupLabel.CellChildren));
                    ConvertCellChildren(ref convertedCell, wrldCellChildren, wrld.FormId);

                    DistributeWorldSpaceReferecnes(convertedCell);

                }
       
            }
        }

        private static void DistributeWorldSpaceReferecnes(TES3Lib.Records.CELL convertedCell)
        {
            foreach (var cellReference in convertedCell.REFR)
            {
                int cellGrindX = (int)(cellReference.DATA.XPos / Config.mwCellSize);
                int cellGrindY = (int)(cellReference.DATA.YPos / Config.mwCellSize);

                ConvertedRecordData targetConvertedCell = ConvertedRecords["CELL"].FirstOrDefault(x => 
                    (x.Record as TES3Lib.Records.CELL).DATA.GridX.Equals(cellGrindX) && (x.Record as TES3Lib.Records.CELL).DATA.GridY.Equals(cellGrindY));

                if (!IsNull(targetConvertedCell))
                {
                    var cellRecord = targetConvertedCell.Record as TES3Lib.Records.CELL;
                    cellRecord.NAM0.ReferenceCount++;
                    cellReference.FRMR.ObjectIndex = cellRecord.NAM0.ReferenceCount;
                    cellRecord.REFR.Add(cellReference);

                    //need update parent formId
                    var convertedReference = CellReferences.FirstOrDefault(x => x.Reference.Equals(cellReference));
                    convertedReference.ParentCellFormId = targetConvertedCell.OriginFormId;

                }
                else
                {
                    Console.WriteLine($"target cell at coordinates {cellGrindX}.{cellGrindY} not found");
                }
            }
        }

        private static void ProcessInteriorSubBlocks(Group interiorSubBlock)
        {
            foreach (var cellSubBlock in interiorSubBlock.Groups)
            {
                foreach (TES4Lib.Records.CELL interiorCell in cellSubBlock.Records)
                {
                    string cellFormId = interiorCell.FormId;

                    if (interiorCell.Flag.Contains(TES4Lib.Enums.Flags.RecordFlag.Deleted)) continue;

                    //hack for now to get SI only
                    if ((interiorCell.EDID.EditorId.Contains("SE") || interiorCell.EDID.EditorId.Contains("XP")) && !IsNull(interiorCell.FULL))
                    {
                        var convertedCell = ConvertCELL(interiorCell);
                        if (IsNull(convertedCell)) throw new Exception("Output cell was null");

                        var cellChildren = cellSubBlock.Groups.FirstOrDefault(x => x.Label == interiorCell.FormId);
                        if (IsNull(cellChildren)) continue;

                        Console.WriteLine($"BEGIN CONVERTING \"{convertedCell.NAME.EditorId}\" CELL");

                        ConvertCellChildren(ref convertedCell, cellChildren, cellFormId);                      

                        foreach (var item in ConvertedRecords["CELL"])
                        {
                            bool cellWithSameNameExists = (item.Record as TES3Lib.Records.CELL).NAME.EditorId.Equals(convertedCell.NAME.EditorId);
                            if (cellWithSameNameExists)
                            {
                                convertedCell.NAME.EditorId = CellNameFormatter($"{convertedCell.NAME.EditorId.Replace("\0", " ")}{interiorCell.EDID.EditorId}");
                                break;
                            }
                        }

                        ConvertedRecords["CELL"].Add(new ConvertedRecordData(interiorCell.FormId, "CELL", interiorCell.EDID.EditorId, convertedCell));

                        Console.WriteLine($"DONE CONVERTING \"{convertedCell.NAME.EditorId}\" CELL");
                    }
                }
            }
        }

        private static void ProcessExteriorSubBlocks(Group exteriorCellBlock)
        {
            foreach (var subBlocks in exteriorCellBlock.Groups)
            {
                foreach (TES4Lib.Records.CELL exteriorCell in subBlocks.Records)
                {
                    bool cellMerge = false;
                    string cellFormId = exteriorCell.FormId;
                    var convertedCell = ConvertCELL(exteriorCell);             

                    // resolve if this cell at this grid already exist
                    foreach (var alreadyConvertedCell in ConvertedRecords["CELL"])
                    {
                        if (convertedCell.Equals(alreadyConvertedCell.Record as TES3Lib.Records.CELL))
                        {
                            cellMerge = true;
                            cellFormId = alreadyConvertedCell.OriginFormId;

                            alreadyConvertedCell.OriginFormId += exteriorCell.FormId;  //store all source cells formid as one string

                            convertedCell = mergeExteriorCells(alreadyConvertedCell.Record as TES3Lib.Records.CELL, convertedCell);

                            Console.WriteLine("merging subcells...");
                            break;
                        }
                    }

                    var cellChildren = subBlocks.Groups.FirstOrDefault(x => x.Label == exteriorCell.FormId);

                    if (IsNull(cellChildren))
                    {
                        Console.WriteLine("cell has no objects");
                        continue;
                    }

                    ConvertCellChildren(ref convertedCell, cellChildren, cellFormId);

                    if (!cellMerge)
                    {
                        //if (IsNull(convertedCell.RGNN) && convertedCell.REFR.Count.Equals(0)) return;

                        var editorId = !IsNull(exteriorCell.EDID) ? exteriorCell.EDID.EditorId : $"{exteriorCell.XCLC.GridX},{exteriorCell.XCLC.GridY}";
                        ConvertedRecords["CELL"].Add(new ConvertedRecordData(exteriorCell.FormId, "CELL", editorId, convertedCell));
                    }
                    Console.WriteLine($"DONE CONVERTING \"{convertedCell.DATA.GridX},{convertedCell.DATA.GridY}\" CELL");
                }
            }
        }

        private static void ConvertCellChildren(ref TES3Lib.Records.CELL mwCELL, Group cellChildren, string originalCellFormId)
        {
            foreach (var childrenType in cellChildren.Groups)
            {
                if(IsNull(mwCELL.NAM0))
                {
                    mwCELL.NAM0 = new TES3Lib.Subrecords.CELL.NAM0 { ReferenceCount = 1 };
                }

                foreach (var obRef in childrenType.Records)
                {
                    if (obRef.Flag.Contains(TES4Lib.Enums.Flags.RecordFlag.Deleted)) continue;

                    TES3Lib.Records.REFR mwREFR;

                    var referenceTypeName = obRef.GetType().Name;

                    if (referenceTypeName.Equals("REFR"))
                    {
                        var obREFR = obRef as TES4Lib.Records.REFR;
                        if (IsNull(obREFR.NAME)) continue;
                        var ReferenceBaseFormId = obREFR.NAME.BaseFormId;

                        var BaseId = GetBaseId(ReferenceBaseFormId);
                        if (string.IsNullOrEmpty(BaseId)) continue;

                        mwREFR = ConvertREFR(obREFR, BaseId, mwCELL.NAM0.ReferenceCount, mwCELL.DATA.Flags.Contains(TES3Lib.Enums.Flags.CellFlag.IsInteriorCell));
                        CellReferences.Add(new ConvertedCellReference(originalCellFormId, obREFR.FormId, mwREFR)); //for tracking

                        mwCELL.REFR.Add(mwREFR);
                        mwCELL.NAM0.ReferenceCount++;
                        continue;
                    }

                    if (referenceTypeName.Equals("ACRE"))
                    {
                        var obACRE = obRef as TES4Lib.Records.ACRE;
                        if (IsNull(obACRE.NAME)) continue;
                        var ReferenceBaseFormId = obACRE.NAME.BaseFormId;

                        var BaseId = GetBaseIdFromFormId(ReferenceBaseFormId);
                        if (string.IsNullOrEmpty(BaseId)) continue;

                        mwREFR = ConvertACRE(obACRE, BaseId, mwCELL.NAM0.ReferenceCount, true);
                        CellReferences.Add(new ConvertedCellReference(originalCellFormId, obACRE.FormId, mwREFR)); //for tracking

                        mwCELL.REFR.Add(mwREFR);
                        mwCELL.NAM0.ReferenceCount++;
                        continue;
                    }

                    if (referenceTypeName.Equals("ACHR"))
                    {
                        continue;
                    }

                    if (referenceTypeName.Equals("LAND"))
                    {
                        continue;
                    }

                    if (referenceTypeName.Equals("PGRD"))
                    {
                        continue;
                    }
                }
            }
        }

        private static TES3Lib.Records.CELL mergeExteriorCells(TES3Lib.Records.CELL cellBase, TES3Lib.Records.CELL cellToMerge)
        {
            cellBase.NAME = cellBase.NAME.EditorId.Equals("\0") ? cellToMerge.NAME : cellBase.NAME;
            cellBase.RGNN = IsNull(cellBase.RGNN) ? cellToMerge.RGNN : cellBase.RGNN;

            return cellBase;
        }

        private static TES3Lib.Records.TES3 createTES3HEader()
        {
            var header = new TES3Lib.Records.TES3
            {
                HEDR = new TES3Lib.Subrecords.TES3.HEDR
                {
                    CompanyName = "TES3Tool\0",
                    Description = "\0",
                    NumRecords = 666,
                    ESMFlag = 0,
                    Version = 1.3f,
                },
                MAST = new TES3Lib.Subrecords.TES3.MAST
                {
                    Filename = "Morrowind.esm\0",
                },
                DATA = new TES3Lib.Subrecords.TES3.DATA
                {
                    MasterDataSize = 6666 //should not break but fix that later
                }
            };
            return header;
        }

        public static ConvertedRecordData ConvertRecordFromFormId(string BaseFormId)
        {
            TES4Lib.Base.Record record;
            TES4Lib.TES4.TES4RecordIndex.TryGetValue(BaseFormId, out record);
            if (IsNull(record)) return null;

            var mwRecordFromREFR = ConvertRecord(record);

            return mwRecordFromREFR;
        }
    }
}
