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
            if(recordType.Equals("STAT"))
            {
                var mwSTAT = ConvertSTAT((TES4Lib.Records.STAT)obRecord);
                return new ConvertedRecordData(obRecord.FormId ,mwSTAT.GetType().Name, mwSTAT.NAME.EditorId, mwSTAT);
            }

            //FURNITURE (output is eighter static object or activator)
            if(recordType.Equals("FURN"))
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
            if(recordType.Equals("LIGH"))
            {
                var mwLIGHT = ConvertLIGH((TES4Lib.Records.LIGH)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwLIGHT.GetType().Name, mwLIGHT.NAME.EditorId, mwLIGHT);
            }

            //ACTIVATOR
            if(recordType.Equals("ACTI"))
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
            if(recordType.Equals("MISC"))
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

            return null;
        }

        static TES3Lib.Records.MISC ConvertMISC(TES4Lib.Records.MISC obRecord)
        {
            return new TES3Lib.Records.MISC
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = obRecord.EDID.EditorId },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = ModelPathFormater(obRecord.MODL.ModelPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = obRecord.FULL.Name },
                MCDT = new TES3Lib.Subrecords.MISC.MCDT { Weight = obRecord.DATA.Weight, Value = obRecord.DATA.Value, Unknown = 0 },
                ITEX = new TES3Lib.Subrecords.Shared.ITEX { IconPath = ModelPathFormater(obRecord.ICON.IconFileName) },
                SCRI = null,
            };
        }

        static TES3Lib.Records.MISC ConvertKEYM(TES4Lib.Records.KEYM obRecord)
        {
            return new TES3Lib.Records.MISC
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = obRecord.EDID.EditorId },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = ModelPathFormater(obRecord.MODL.ModelPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = obRecord.FULL.Name },
                MCDT = new TES3Lib.Subrecords.MISC.MCDT { Weight = obRecord.DATA.Weight, Value = obRecord.DATA.Value, Unknown = 0 },
                ITEX = new TES3Lib.Subrecords.Shared.ITEX { IconPath = ModelPathFormater(obRecord.ICON.IconFileName) },
                SCRI = null,
            };
        }

        static TES3Lib.Records.LIGH ConvertLIGH(TES4Lib.Records.LIGH obLIGH)
        {
            var LIGH = new TES3Lib.Records.LIGH();
            LIGH.NAME = new TES3Lib.Subrecords.Shared.NAME() { EditorId = obLIGH.EDID.EditorId };
            LIGH.FNAM = !IsNull(obLIGH.FULL) ? new TES3Lib.Subrecords.Shared.FNAM() { Name = obLIGH.FULL.Name } : null;

            LIGH.LHDT = new TES3Lib.Subrecords.LIGH.LHDT
            {
                Weight = obLIGH.DATA.Weight,
                Value = obLIGH.DATA.Value,
                Color = obLIGH.DATA.Color,
                Time = obLIGH.DATA.Time,
                Radius = obLIGH.DATA.Radius,
                Flags = ConvertLIGHFlags(obLIGH.DATA.Flags)
            };

            LIGH.SCPT = null;
            LIGH.ITEX = !IsNull(obLIGH.ICON) ? new TES3Lib.Subrecords.Shared.ITEX() { IconPath = obLIGH.ICON.IconFileName } : null;
            LIGH.MODL = !IsNull(obLIGH.MODL) ? new TES3Lib.Subrecords.Shared.MODL() { ModelPath = obLIGH.MODL.ModelPath } : null;

            if(!IsNull(obLIGH.SNAM))//if has sound convert it as well
            {
                var BaseId = GetBaseIdFromFormId(obLIGH.SNAM.SoundFormId);
                if (string.IsNullOrEmpty(BaseId))
                {
                    var mwRecordFromREFR = ConvertRecordFromREFR(obLIGH.SNAM.SoundFormId);
                    if (!ConvertedRecords.ContainsKey(mwRecordFromREFR.Type)) ConvertedRecords.Add(mwRecordFromREFR.Type, new List<ConvertedRecordData>());
                    ConvertedRecords[mwRecordFromREFR.Type].Add(mwRecordFromREFR);
                    BaseId = mwRecordFromREFR.EditorId;
                }

                LIGH.SNAM = new TES3Lib.Subrecords.Shared.SNAM() { SoundName = BaseId };
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
                    EditorId = $"s{obSOUND.EDID.EditorId}",
                },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM()
                {
                    Name = obSOUND.FNAM.SoundFilename
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
                MODL = new TES3Lib.Subrecords.Shared.MODL() { ModelPath = obACTI.MODL.ModelPath },
                NAME = new TES3Lib.Subrecords.Shared.NAME() { EditorId = obACTI.EDID.EditorId },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM() { Name = obACTI.FULL.FullName },
            };
        }

        static TES3Lib.Records.STAT ConvertSTAT(TES4Lib.Records.STAT obSTAT)
        {
            return new TES3Lib.Records.STAT()
            {
                MODL = new TES3Lib.Subrecords.Shared.MODL() { ModelPath = obSTAT.MODL.ModelFileName },
                NAME = new TES3Lib.Subrecords.Shared.NAME() { EditorId = obSTAT.EDID.EditorId },
            };
        }

        static TES3Lib.Records.ACTI ConvertFURN2ACTI(TES4Lib.Records.FURN obFURN)
        {
            return new TES3Lib.Records.ACTI()
            {
                MODL = new TES3Lib.Subrecords.Shared.MODL() { ModelPath = obFURN.MODL.ModelPath },
                NAME = new TES3Lib.Subrecords.Shared.NAME() { EditorId = obFURN.EDID.EditorId },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM() { Name = obFURN.FULL.FullName},
                SCRI = new TES3Lib.Subrecords.Shared.SCRI() { ScriptName = "Bed_Standard\0"}
            };
        }

        static TES3Lib.Records.STAT ConvertFURN2STAT(TES4Lib.Records.FURN obFURN)
        {
            return new TES3Lib.Records.STAT()
            {
                MODL = new TES3Lib.Subrecords.Shared.MODL() { ModelPath = obFURN.MODL.ModelPath },
                NAME = new TES3Lib.Subrecords.Shared.NAME() { EditorId = obFURN.EDID.EditorId },
            };
        }

        static TES3Lib.Records.ACTI ConvertSOUN2ACTI(TES4Lib.Records.SOUN obSOUN)
        {
            //first sound
            var SOUN = ConvertSOUN(obSOUN);
            SOUN.NAME.EditorId = $"s{SOUN.NAME.EditorId}"; //sound record id will be prefixed with s
            var SOUNData = new ConvertedRecordData($"s{obSOUN.FormId}", SOUN.GetType().Name, SOUN.NAME.EditorId, SOUN);
            if (!ConvertedRecords.ContainsKey(SOUN.Name)) ConvertedRecords.Add(SOUN.Name, new List<ConvertedRecordData>());
                ConvertedRecords[SOUN.Name].Add(SOUNData);

            //then script
            var SCPT = new TES3Lib.Records.SCPT();
            SCPT.SCHD.Name = $"Sound_{SOUN.NAME.EditorId}";
            SCPT.SCHD.LocalVarSize = 6;
            SCPT.SCHD.NumFloats = 6;
            SCPT.SCHD.NumLongs = 6;
            SCPT.SCHD.NumShorts = 6;
            SCPT.SCHD.ScriptDataSize = 6;

            SCPT.SCVR = new TES3Lib.Subrecords.SCPT.SCVR();
            SCPT.SCVR.LocalScriptVariables = "betterrecompilethis\0";

            SCPT.SCDT = new TES3Lib.Subrecords.SCPT.SCDT();
            SCPT.SCDT.CompiledScript = new byte[6];

            SCPT.SCTX = new TES3Lib.Subrecords.SCPT.SCTX();
            SCPT.SCTX.ScriptText = GenerateSoundScript(SOUN.NAME.EditorId);

            if (!ConvertedRecords.ContainsKey(SCPT.GetType().Name)) ConvertedRecords.Add(SCPT.GetType().Name, new List<ConvertedRecordData>());
            ConvertedRecords[SCPT.GetType().Name].Add(new ConvertedRecordData("SOUNDSCPT","SCPT", SCPT.SCHD.Name, SCPT));

            //then activator itself
            var ACTI = new TES3Lib.Records.ACTI();
            ACTI.MODL = new TES3Lib.Subrecords.Shared.MODL() { ModelPath="SI/SoundEmitter.nif"};
            ACTI.NAME = new TES3Lib.Subrecords.Shared.NAME() { EditorId = SOUN.NAME.EditorId };
            ACTI.FNAM = new TES3Lib.Subrecords.Shared.FNAM() { Name = SOUN.NAME.EditorId };
            ACTI.SCRI = new TES3Lib.Subrecords.Shared.SCRI() { ScriptName=""};

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
