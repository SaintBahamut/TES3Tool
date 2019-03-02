using static TES3Tool.TES4RecordConverter.Records.Helpers;
using static TES3Tool.TES4RecordConverter.Oblivion2Morrowind;
using System;
using System.Linq;
using System.Collections.Generic;
using TES4Lib.Records;

namespace TES3Tool.TES4RecordConverter.Records
{
    public static class Converters
    {
        /// <summary>
        /// Converts Oblivion Record to Morrowind Record
        /// </summary>
        /// <param name="obRecord">oblivion record to convert</param>
        /// <returns>Record Type:EditorID:Record</returns>
        internal static ConvertedRecordData ConvertRecord(TES4Lib.Base.Record obRecord)
        {
            var recordType = obRecord.Name;

            //STATIC
            if (recordType.Equals("STAT"))
            {
                var mwSTAT = ConvertSTAT((TES4Lib.Records.STAT)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwSTAT.GetType().Name, mwSTAT.NAME.EditorId, mwSTAT);
            }

            //FURNITURE (output is eighter static object or activator)
            if (recordType.Equals("FURN"))
            {
                var obFURN = (TES4Lib.Records.FURN)obRecord;
                ///BED
                if (obFURN.MNAM.ActiveMarkerFlags[0].Equals("8"))
                {
                    var mwACTI = ConvertFURN2ACTI(obFURN);
                    return new ConvertedRecordData(obRecord.FormId, mwACTI.GetType().Name, mwACTI.NAME.EditorId, mwACTI);
                }
                //CHAIRS AND OTHERS
                else
                {
                    var mwSTAT = ConvertFURN2STAT(obFURN);
                    return new ConvertedRecordData(obRecord.FormId, mwSTAT.GetType().Name, mwSTAT.NAME.EditorId, mwSTAT);
                }
            }

            //LIGHT
            if (recordType.Equals("LIGH"))
            {
                var mwLIGHT = ConvertLIGH((TES4Lib.Records.LIGH)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwLIGHT.GetType().Name, mwLIGHT.NAME.EditorId, mwLIGHT);
            }

            //ACTIVATOR
            if (recordType.Equals("ACTI"))
            {
                var mwACTI = ConvertACTI((TES4Lib.Records.ACTI)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwACTI.GetType().Name, mwACTI.NAME.EditorId, mwACTI);
            }

            //SOUND (ouptut is activator with hooked playback scipt)
            if (recordType.Equals("SOUN"))
            {
                var mwSOUN = ConvertSOUN2ACTI((TES4Lib.Records.SOUN)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwSOUN.GetType().Name, mwSOUN.NAME.EditorId, mwSOUN);
            }

            //MISC
            if (recordType.Equals("MISC"))
            {
                var mwMISC = ConvertMISC((TES4Lib.Records.MISC)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwMISC.GetType().Name, mwMISC.NAME.EditorId, mwMISC);
            }

            //KEYM (outputs MISC)
            if (recordType.Equals("KEYM"))
            {
                var mwMISC = ConvertKEYM((TES4Lib.Records.KEYM)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwMISC.GetType().Name, mwMISC.NAME.EditorId, mwMISC);
            }

            //CONT
            if (recordType.Equals("CONT"))
            {
                var mwCONT = ConvertCONT((TES4Lib.Records.CONT)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwCONT.GetType().Name, mwCONT.NAME.EditorId, mwCONT);
            }

            //CONT (outputs CONT for ingredient producting plant if not output is STAT
            if (recordType.Equals("FLOR"))
            {
                if (!IsNull((obRecord as TES4Lib.Records.FLOR).PFIG))
                {
                    var mwCONT = ConvertFLOR2CONT((TES4Lib.Records.FLOR)obRecord);
                    return new ConvertedRecordData(obRecord.FormId, mwCONT.GetType().Name, mwCONT.NAME.EditorId, mwCONT);
                }
                var mwSTAT = ConvertFLOR2STAT((TES4Lib.Records.FLOR)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwSTAT.GetType().Name, mwSTAT.NAME.EditorId, mwSTAT);
            }

            //CONT
            if (recordType.Equals("DOOR"))
            {
                var mwDOOR = ConvertDOOR((TES4Lib.Records.DOOR)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwDOOR.GetType().Name, mwDOOR.NAME.EditorId, mwDOOR);
            }


            return null;
        }

        private static TES3Lib.Records.DOOR ConvertDOOR(TES4Lib.Records.DOOR obDOOR)
        {
            var mwDOOR = new TES3Lib.Records.DOOR
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obDOOR.EDID.EditorId) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(!IsNull(obDOOR.FULL) ? obDOOR.FULL.DisplayName : string.Empty) },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obDOOR.MODL.ModelPath, Config.FLORPath) }
            };

            if (!IsNull(obDOOR.SNAM))//if has sound convert it as well
            {
                var BaseId = GetBaseIdFromFormId("s" + obDOOR.SNAM.SoundFormId);
                if (string.IsNullOrEmpty(BaseId))
                {

                    TES4Lib.Base.Record record;
                    TES4Lib.TES4.TES4RecordIndex.TryGetValue(obDOOR.SNAM.SoundFormId, out record);
                    if (!IsNull(record))
                    {
                        var mwSOUNDFromREFR = ConvertSOUN((TES4Lib.Records.SOUN)record);
                        if (!ConvertedRecords.ContainsKey(mwSOUNDFromREFR.GetType().Name)) ConvertedRecords.Add(mwSOUNDFromREFR.GetType().Name, new List<ConvertedRecordData>());
                        ConvertedRecords[mwSOUNDFromREFR.GetType().Name].Add(new ConvertedRecordData($"s{obDOOR.SNAM.SoundFormId}", mwSOUNDFromREFR.GetType().Name, mwSOUNDFromREFR.NAME.EditorId, mwSOUNDFromREFR));
                        BaseId = mwSOUNDFromREFR.NAME.EditorId;
                    }
                }

                mwDOOR.SNAM = new TES3Lib.Subrecords.Shared.SNAM { SoundName = BaseId };
            }

            if (!IsNull(obDOOR.ANAM))//if has sound convert it as well
            {
                var BaseId = GetBaseIdFromFormId("s" + obDOOR.ANAM.SoundFormId);
                if (string.IsNullOrEmpty(BaseId))
                {

                    TES4Lib.Base.Record record;
                    TES4Lib.TES4.TES4RecordIndex.TryGetValue(obDOOR.ANAM.SoundFormId, out record);
                    if (!IsNull(record))
                    {
                        var mwSOUNDFromREFR = ConvertSOUN((TES4Lib.Records.SOUN)record);
                        if (!ConvertedRecords.ContainsKey(mwSOUNDFromREFR.GetType().Name)) ConvertedRecords.Add(mwSOUNDFromREFR.GetType().Name, new List<ConvertedRecordData>());
                        ConvertedRecords[mwSOUNDFromREFR.GetType().Name].Add(new ConvertedRecordData($"s{obDOOR.ANAM.SoundFormId}", mwSOUNDFromREFR.GetType().Name, mwSOUNDFromREFR.NAME.EditorId, mwSOUNDFromREFR));
                        BaseId = mwSOUNDFromREFR.NAME.EditorId;
                    }
                }

                mwDOOR.ANAM = new TES3Lib.Subrecords.DOOR.ANAM { SoundNameClose = BaseId };
            }

            return mwDOOR;
        }

        private static TES3Lib.Records.STAT ConvertFLOR2STAT(TES4Lib.Records.FLOR obFLOR)
        {
            return new TES3Lib.Records.STAT()
            {
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obFLOR.MODL.ModelPath, TES3Tool.Config.FLORPath) },
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obFLOR.EDID.EditorId) },
            };
        }

        private static TES3Lib.Records.CONT ConvertFLOR2CONT(TES4Lib.Records.FLOR obFLOR)
        {
            var CONT = new TES3Lib.Records.CONT
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obFLOR.EDID.EditorId) },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obFLOR.MODL.ModelPath, TES3Tool.Config.FLORPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(obFLOR.FULL.DisplayName) },
                CNDT = new TES3Lib.Subrecords.CONT.CNDT { Weight = 0 },
                FLAG = new TES3Lib.Subrecords.CONT.FLAG { Flags = 0x0008 | 0x0001 | 0x0002 }
            };

            if (!IsNull(obFLOR.PFIG))
            {
                var BaseId = GetBaseIdFromFormId(obFLOR.PFIG.IngredientProduced);
                if (string.IsNullOrEmpty(BaseId))
                {
                    var rnd = new Random((int)DateTime.Now.Ticks); //calling gods
                    var fate = rnd.Next(0, 3); //pray for a sign
                    var qnt = 2;
                    switch (fate) //interpret gods words
                    {
                        case 0:
                            qnt = (int)obFLOR.PFPC.SpringProd;
                            break;
                        case 1:
                            qnt = (int)obFLOR.PFPC.SummerProd;
                            break;
                        case 2:
                            qnt = (int)obFLOR.PFPC.FallProd;
                            break;
                        case 4:
                            qnt = (int)obFLOR.PFPC.WinterProd;
                            break;
                    }

                    if (qnt == 0) //if gods are cruel, fuck them
                        qnt = 2;


                    TES4Lib.Base.Record record;
                    TES4Lib.TES4.TES4RecordIndex.TryGetValue(obFLOR.PFIG.IngredientProduced, out record);
                    if (!IsNull(record))
                    {
                        CONT.NPCO = new List<TES3Lib.Subrecords.Shared.NPCO>();
                        var mwRecordFromREFR = ConvertRecordFromREFR(BaseId);
                        if (!ConvertedRecords.ContainsKey(mwRecordFromREFR.Type)) ConvertedRecords.Add(mwRecordFromREFR.Type, new List<ConvertedRecordData>());
                        ConvertedRecords[mwRecordFromREFR.Type].Add(mwRecordFromREFR);
                        CONT.NPCO.Add(new TES3Lib.Subrecords.Shared.NPCO { ItemId = mwRecordFromREFR.EditorId, Count = qnt });
                    }
                }

            }

            return CONT;
        }

        private static TES3Lib.Records.CONT ConvertCONT(TES4Lib.Records.CONT obCONT)
        {
            var CONT = new TES3Lib.Records.CONT
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obCONT.EDID.EditorId) },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obCONT.MODL.ModelPath, TES3Tool.Config.CONTPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(!IsNull(obCONT.FULL) ? obCONT.FULL.DisplayName : "" ) },
                CNDT = new TES3Lib.Subrecords.CONT.CNDT { Weight = obCONT.DATA.Weight },
                FLAG = new TES3Lib.Subrecords.CONT.FLAG { Flags = 0x0008 }
            };

            if (obCONT.CNTO.Count > 0)
            {
                CONT.NPCO = new List<TES3Lib.Subrecords.Shared.NPCO>();

                foreach (var item in obCONT.CNTO)
                {
                    var BaseId = GetBaseIdFromFormId(item.ItemId);
                    if (string.IsNullOrEmpty(BaseId))
                    {
                        TES4Lib.Base.Record record;
                        TES4Lib.TES4.TES4RecordIndex.TryGetValue(item.ItemId, out record);
                        if (!IsNull(record))
                        {
                            var mwRecordFromREFR = ConvertRecordFromREFR(item.ItemId);
                            if (!ConvertedRecords.ContainsKey(mwRecordFromREFR.Type)) ConvertedRecords.Add(mwRecordFromREFR.Type, new List<ConvertedRecordData>());
                            ConvertedRecords[mwRecordFromREFR.Type].Add(mwRecordFromREFR);
                            CONT.NPCO.Add(new TES3Lib.Subrecords.Shared.NPCO { ItemId = mwRecordFromREFR.EditorId, Count = item.ItemCount });
                        }
                    }
                }
            }

            return CONT;
        }

        static TES3Lib.Records.MISC ConvertMISC(TES4Lib.Records.MISC obRecord)
        {
            return new TES3Lib.Records.MISC
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obRecord.EDID.EditorId) },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obRecord.MODL.ModelPath, TES3Tool.Config.MISCPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(obRecord.FULL.DisplayName) },
                MCDT = new TES3Lib.Subrecords.MISC.MCDT { Weight = obRecord.DATA.Weight, Value = obRecord.DATA.Value, Unknown = 0 },
                ITEX = new TES3Lib.Subrecords.Shared.ITEX { IconPath = PathFormater(obRecord.ICON.IconFilePath, TES3Tool.Config.MISCPath) },
                SCRI = null,
            };
        }

        static TES3Lib.Records.MISC ConvertKEYM(TES4Lib.Records.KEYM obRecord)
        {
            return new TES3Lib.Records.MISC
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obRecord.EDID.EditorId) },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obRecord.MODL.ModelPath, TES3Tool.Config.MISCPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(obRecord.FULL.DisplayName) },
                MCDT = new TES3Lib.Subrecords.MISC.MCDT { Weight = obRecord.DATA.Weight, Value = obRecord.DATA.Value, Unknown = 0 },
                ITEX = new TES3Lib.Subrecords.Shared.ITEX { IconPath = PathFormater(obRecord.ICON.IconFilePath, TES3Tool.Config.MISCPath) },
            };
        }

        static TES3Lib.Records.LIGH ConvertLIGH(TES4Lib.Records.LIGH obLIGH)
        {
            var LIGH = new TES3Lib.Records.LIGH
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME() { EditorId = EditorIdFormater(obLIGH.EDID.EditorId) },
                FNAM = !IsNull(obLIGH.FULL) ? new TES3Lib.Subrecords.Shared.FNAM() { Name = NameFormater(obLIGH.FULL.DisplayName) } : null,
                LHDT = new TES3Lib.Subrecords.LIGH.LHDT
                {
                    Weight = obLIGH.DATA.Weight,
                    Value = obLIGH.DATA.Value,
                    Color = obLIGH.DATA.Color,
                    Time = obLIGH.DATA.Time,
                    Radius = obLIGH.DATA.Radius,
                    Flags = ConvertLIGHFlags(obLIGH.DATA.Flags)
                },
                SCPT = null,
                ITEX = !IsNull(obLIGH.ICON) ? new TES3Lib.Subrecords.Shared.ITEX { IconPath = PathFormater(obLIGH.ICON.IconFilePath, Config.LIGHPath) } : null,
                MODL = !IsNull(obLIGH.MODL) ? new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obLIGH.MODL.ModelPath, Config.LIGHPath) } : null,
            };
           
            if (!IsNull(obLIGH.SNAM))//if has sound convert it as well
            {
                var BaseId = GetBaseIdFromFormId("s" + obLIGH.SNAM.SoundFormId);
                if (string.IsNullOrEmpty(BaseId))
                {

                    TES4Lib.Base.Record record;
                    TES4Lib.TES4.TES4RecordIndex.TryGetValue(obLIGH.SNAM.SoundFormId, out record);
                    if (!IsNull(record))
                    {
                        var mwSOUNDFromREFR = ConvertSOUN((TES4Lib.Records.SOUN)record);
                        if (!ConvertedRecords.ContainsKey(mwSOUNDFromREFR.GetType().Name)) ConvertedRecords.Add(mwSOUNDFromREFR.GetType().Name, new List<ConvertedRecordData>());
                        ConvertedRecords[mwSOUNDFromREFR.GetType().Name].Add(new ConvertedRecordData($"s{obLIGH.SNAM.SoundFormId}", mwSOUNDFromREFR.GetType().Name, mwSOUNDFromREFR.NAME.EditorId, mwSOUNDFromREFR));
                        BaseId = mwSOUNDFromREFR.NAME.EditorId;
                    }
                }

                LIGH.SNAM = new TES3Lib.Subrecords.Shared.SNAM { SoundName = BaseId };
            }

            return LIGH;
        }

        static int ConvertLIGHFlags(int flags)
        {
            int output = 0;
            output = output | (flags & 0x00000001);
            output = output | (flags & 0x00000002);
            output = output | (flags & 0x00000004);
            output = output | (flags & 0x00000008);
            output = output | (flags & 0x00000020);
            output = output | (flags & 0x00000040);
            output = output | (flags & 0x00000080);
            output = output | (flags & 0x00000100);
            return output;
        }

        static TES3Lib.Records.SOUN ConvertSOUN(TES4Lib.Records.SOUN obSOUND)
        {
            return new TES3Lib.Records.SOUN()
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME()
                {
                    EditorId = SoundIdFormater(obSOUND.EDID.EditorId),
                },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM()
                {
                    Name = PathFormater(obSOUND.FNAM.SoundFilename, TES3Tool.Config.SOUNPath)
                },
                DATA = new TES3Lib.Subrecords.SOUN.DATA()
                {
                    Volume = 1,
                    MinRange = obSOUND.SNDX.MinAttentuationDist,
                    MaxRange = obSOUND.SNDX.MinAttentuationDist,
                }
            };
        }

        static TES3Lib.Records.ACTI ConvertACTI(TES4Lib.Records.ACTI obACTI)
        {
            return new TES3Lib.Records.ACTI()
            {
                MODL = new TES3Lib.Subrecords.Shared.MODL() { ModelPath = PathFormater(obACTI.MODL.ModelPath, TES3Tool.Config.ACTIPath) },
                NAME = new TES3Lib.Subrecords.Shared.NAME() { EditorId = EditorIdFormater(obACTI.EDID.EditorId) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM() { Name = !IsNull(obACTI.FULL) ? NameFormater(obACTI.FULL.DisplayName) : "" },
            };
        }

        static TES3Lib.Records.STAT ConvertSTAT(TES4Lib.Records.STAT obSTAT)
        {
            return new TES3Lib.Records.STAT
            {
                MODL = new TES3Lib.Subrecords.Shared.MODL() { ModelPath = PathFormater(obSTAT.MODL.ModelFileName, Config.STATPath) },
                NAME = new TES3Lib.Subrecords.Shared.NAME() { EditorId = EditorIdFormater(obSTAT.EDID.EditorId) },
            };
        }

        static TES3Lib.Records.ACTI ConvertFURN2ACTI(TES4Lib.Records.FURN obFURN)
        {
            return new TES3Lib.Records.ACTI
            {
                MODL = new TES3Lib.Subrecords.Shared.MODL() { ModelPath = PathFormater(obFURN.MODL.ModelPath, Config.FURNPath) },
                NAME = new TES3Lib.Subrecords.Shared.NAME() { EditorId = EditorIdFormater(obFURN.EDID.EditorId) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM() { Name = NameFormater(obFURN.FULL.DisplayName) },
                SCRI = new TES3Lib.Subrecords.Shared.SCRI() { ScriptName = "Bed_Standard\0" }
            };
        }

        static TES3Lib.Records.STAT ConvertFURN2STAT(TES4Lib.Records.FURN obFURN)
        {
            return new TES3Lib.Records.STAT
            {
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obFURN.MODL.ModelPath, Config.FURNPath) },
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obFURN.EDID.EditorId) },
            };
        }

        static TES3Lib.Records.ACTI ConvertSOUN2ACTI(TES4Lib.Records.SOUN obSOUN)
        {
            //first sound
            var BaseId = GetBaseIdFromFormId("s" + obSOUN.FormId);
            if (string.IsNullOrEmpty(BaseId))
            {
                TES4Lib.Base.Record record;
                TES4Lib.TES4.TES4RecordIndex.TryGetValue(obSOUN.FormId, out record);
                if (!IsNull(record))
                {
                    var mwRecordFromREFR = ConvertSOUN((TES4Lib.Records.SOUN)record);
                    if (!ConvertedRecords.ContainsKey(mwRecordFromREFR.GetType().Name)) ConvertedRecords.Add(mwRecordFromREFR.GetType().Name, new List<ConvertedRecordData>());
                    ConvertedRecords[mwRecordFromREFR.GetType().Name].Add(new ConvertedRecordData($"s{obSOUN.FormId}", mwRecordFromREFR.GetType().Name, mwRecordFromREFR.NAME.EditorId, mwRecordFromREFR));
                    BaseId = mwRecordFromREFR.NAME.EditorId;
                }
            }

            //then script
            var SCPT = new TES3Lib.Records.SCPT
            {
                SCHD = new TES3Lib.Subrecords.SCPT.SCHD
                {
                    Name = $"Sound_{BaseId}",
                    LocalVarSize = 6,
                    NumFloats = 6,
                    NumLongs = 6,
                    NumShorts = 6,
                    ScriptDataSize = 6
                },
                SCVR = new TES3Lib.Subrecords.SCPT.SCVR
                {
                    LocalScriptVariables = "betterrecompilethis\0"
                },
                SCDT = new TES3Lib.Subrecords.SCPT.SCDT
                {
                    CompiledScript = new byte[6]
                },
                SCTX = new TES3Lib.Subrecords.SCPT.SCTX
                {
                    ScriptText = GenerateSoundScript(BaseId.Replace("\0", ""))
                }
            };


            if (!ConvertedRecords.ContainsKey(SCPT.GetType().Name)) ConvertedRecords.Add(SCPT.GetType().Name, new List<ConvertedRecordData>());
            ConvertedRecords[SCPT.GetType().Name].Add(new ConvertedRecordData("SOUNDSCPT", "SCPT", SCPT.SCHD.Name, SCPT));

            //then activator itself
            var ACTI = new TES3Lib.Records.ACTI
            {
                MODL = new TES3Lib.Subrecords.Shared.MODL() { ModelPath = $"{Config.convertedRootFolder}\\SoundEmitter.nif\0" },
                NAME = new TES3Lib.Subrecords.Shared.NAME() { EditorId = BaseId.TrimStart('s') },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM() { Name = BaseId },
                SCRI = new TES3Lib.Subrecords.Shared.SCRI() { ScriptName = $"Sound_{BaseId}" }
            };

            return ACTI;
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
                mwREFR.XSCL = new TES3Lib.Subrecords.REFR.XSCL
                {
                    Scale = obREFR.XSCL.Scale <= 0.5f ? 0.5f : obREFR.XSCL.Scale
                };
            }

            //if (false)
            //{
            //    //this data should be somewhere in record flag, not relevant, at lest for now
            //    mwREFR.DELE = new TES3Lib.Subrecords.REFR.DELE();
            //}

            // TODO:if palling exterior support this needs to be modified, because it expects only interiors to be present, so if
            // no cell found, door leads to exterior space
            if (!IsNull(obREFR.XTEL))
            {
                TES4Lib.Base.Record record;
                TES4Lib.TES4.TES4RecordIndex.TryGetValue(obREFR.XTEL.DestinationDoorReference, out record);
                if (IsNull(record)) //if record is null it means its exterior cell
                {
                    float shiftX = (Config.cellShiftX * Config.mwCellSize);
                    float shiftY = (Config.cellShiftY * Config.mwCellSize);
                    mwREFR.DODT = new TES3Lib.Subrecords.REFR.DODT
                    {                       
                        XPos = obREFR.XTEL.DestLocX + shiftX,
                        YPos = obREFR.XTEL.DestLocY + shiftY,
                        ZPos = obREFR.XTEL.DestLocZ,
                        XRotate = obREFR.XTEL.DestRotX,
                        YRotate = obREFR.XTEL.DestRotY,
                        ZRotate = obREFR.XTEL.DestRotZ
                    };
                }
                else
                {
                    mwREFR.DODT = new TES3Lib.Subrecords.REFR.DODT
                    {
                        XPos = obREFR.XTEL.DestLocX,
                        YPos = obREFR.XTEL.DestLocY,
                        ZPos = obREFR.XTEL.DestLocZ,
                        XRotate = obREFR.XTEL.DestRotX,
                        YRotate = obREFR.XTEL.DestRotY,
                        ZRotate = obREFR.XTEL.DestRotZ
                    };
                    mwREFR.DNAM = new TES3Lib.Subrecords.REFR.DNAM
                    {
                        DoorName = obREFR.XTEL.DestinationDoorReference //pass only formId, we will get cell names at end
                    };
                    DoorDestinations.Add(mwREFR.DNAM);
                }
            }

            if (!IsNull(obREFR.XLOC))
            {
                mwREFR.FLTV = new TES3Lib.Subrecords.REFR.FLTV { LockLevel = (int)obREFR.XLOC.LockLevel };

                if (!obREFR.XLOC.Key.Equals("00000000"))
                {
                    var BaseId = GetBaseIdFromFormId(obREFR.XLOC.Key);
                    if (string.IsNullOrEmpty(BaseId))
                    {
                        TES4Lib.Base.Record record;
                        TES4Lib.TES4.TES4RecordIndex.TryGetValue(obREFR.XLOC.Key, out record);
                        if (!IsNull(record))
                        {
                            var mwRecordFromREFR = ConvertRecordFromREFR(obREFR.XLOC.Key);
                            if (!ConvertedRecords.ContainsKey(mwRecordFromREFR.GetType().Name)) ConvertedRecords.Add(mwRecordFromREFR.GetType().Name, new List<ConvertedRecordData>());
                            ConvertedRecords[mwRecordFromREFR.GetType().Name].Add(mwRecordFromREFR);
                        }
                    }
                    mwREFR.KNAM = new TES3Lib.Subrecords.REFR.KNAM { DoorKeyId = BaseId };

                }
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


            if (!IsNull(obREFR.DATA))
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
