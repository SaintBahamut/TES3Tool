using static TES3Tool.TES4RecordConverter.Records.Helpers;
using static Utility.Common;
using static TES3Tool.TES4RecordConverter.Oblivion2Morrowind;
using System;
using System.Linq;
using System.Collections.Generic;
using TES4Lib.Records;
using TES3Lib.Enums;
using TES4Lib.Enums;
using TES3Lib.Enums.Flags;
using TES4Lib.Enums.Flags;

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

            //WEAPONS
            if (recordType.Equals("WEAP"))
            {
                var mwWEAP = ConvertWEAP((TES4Lib.Records.WEAP)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwWEAP.GetType().Name, mwWEAP.NAME.EditorId, mwWEAP);
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

            //KEYS (outputs MISC)
            if (recordType.Equals("KEYM"))
            {
                var mwMISC = ConvertKEYM((TES4Lib.Records.KEYM)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwMISC.GetType().Name, mwMISC.NAME.EditorId, mwMISC);
            }

            //CONTAINERS
            if (recordType.Equals("CONT"))
            {
                var mwCONT = ConvertCONT((TES4Lib.Records.CONT)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwCONT.GetType().Name, mwCONT.NAME.EditorId, mwCONT);
            }

            //FLORA (outputs CONT for ingredient producting plant if not output is STAT
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

            //DOORS
            if (recordType.Equals("DOOR"))
            {
                var mwDOOR = ConvertDOOR((TES4Lib.Records.DOOR)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwDOOR.GetType().Name, mwDOOR.NAME.EditorId, mwDOOR);
            }

            //INGREDIENT
            if (recordType.Equals("INGR"))
            {
                var mwINGR = ConvertINGR((TES4Lib.Records.INGR)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwINGR.GetType().Name, mwINGR.NAME.EditorId, mwINGR);
            }

            //BOOKS
            if (recordType.Equals("BOOK"))
            {
                var mwINGR = ConvertBOOK((TES4Lib.Records.BOOK)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwINGR.GetType().Name, mwINGR.NAME.EditorId, mwINGR);
            }

            //ENCHANTMENTS
            if (recordType.Equals("ENCH"))
            {
                var mwENCH = ConvertENCH((TES4Lib.Records.ENCH)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwENCH.GetType().Name, mwENCH.NAME.EditorId, mwENCH);
            }

            //ALCHEMY
            if (recordType.Equals("ALCH"))
            {
                var mwALCH = ConvertALCH((TES4Lib.Records.ALCH)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwALCH.GetType().Name, mwALCH.NAME.EditorId, mwALCH);
            }

            //AMMO
            if (recordType.Equals("AMMO"))
            {
                var mwWEAP = ConvertAMMO((TES4Lib.Records.AMMO)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwWEAP.GetType().Name, mwWEAP.NAME.EditorId, mwWEAP);
            }

            //APPARATUS
            if (recordType.Equals("APPA"))
            {
                var mwAPPA = ConvertAPPA((TES4Lib.Records.APPA)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwAPPA.GetType().Name, mwAPPA.NAME.EditorId, mwAPPA);
            }

            //ARMOR
            if (recordType.Equals("ARMO"))
            {
                var mwARMO = ConvertARMO((TES4Lib.Records.ARMO)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwARMO.GetType().Name, mwARMO.NAME.EditorId, mwARMO);
            }

            //CLOTHING
            if (recordType.Equals("CLOT"))
            {
                var mwCLOT = ConvertCLOT((TES4Lib.Records.CLOT)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwCLOT.GetType().Name, mwCLOT.NAME.EditorId, mwCLOT);
            }

            //LEVELED ITEM
            if (recordType.Equals("LVLI"))
            {
                var mwLEVI = ConvertLVLI((TES4Lib.Records.LVLI)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwLEVI.GetType().Name, mwLEVI.NAME.EditorId, mwLEVI);
            }

            //LEVELED CREATURE
            if (recordType.Equals("LVLC"))
            {
                var mwLEVC = ConvertLVLC((TES4Lib.Records.LVLC)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwLEVC.GetType().Name, mwLEVC.NAME.EditorId, mwLEVC);
            }

            //CREATURE
            if (recordType.Equals("CREA"))
            {
                var mwCREA = ConvertCREA((TES4Lib.Records.CREA)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwCREA.GetType().Name, mwCREA.NAME.EditorId, mwCREA);
            }

            return null;
        }

        private static TES3Lib.Records.CREA ConvertCREA(TES4Lib.Records.CREA obCREA)
        {
            var mwCREA = new TES3Lib.Records.CREA
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = CreatureIdFormater(obCREA.EDID.EditorId) },
                MODL = new TES3Lib.Subrecords.Shared.MODL(),
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(!IsNull(obCREA.FULL) ? obCREA.FULL.DisplayName : string.Empty) },
                NPDT = new TES3Lib.Subrecords.CREA.NPDT
                {
                    CreatureType = CastCreatureTypeToMW(obCREA.DATA.CreatureType),
                    Level = obCREA.ACBS.Flags.Contains(TES4Lib.Enums.Flags.CreatureFlag.PCLevelOffset) ? (int)Math.Max(obCREA.ACBS.CalcMax, obCREA.ACBS.CalcMin) : obCREA.ACBS.LevelOffset,
                    Strength = obCREA.DATA.Strength,
                    Intelligence = obCREA.DATA.Intelligence,
                    Willpower = obCREA.DATA.Willpower,
                    Agility = obCREA.DATA.Agility,
                    Speed = obCREA.DATA.Speed,
                    Endurance = obCREA.DATA.Endurance,
                    Personality = obCREA.DATA.Personality,
                    Luck = obCREA.DATA.Luck,
                    Health = obCREA.ACBS.Flags.Contains(TES4Lib.Enums.Flags.CreatureFlag.PCLevelOffset) ? (int)Math.Max(obCREA.ACBS.CalcMax, obCREA.ACBS.CalcMin) * obCREA.DATA.Health/2 : obCREA.DATA.Health,                  
                    SpellPts = obCREA.ACBS.Flags.Contains(TES4Lib.Enums.Flags.CreatureFlag.PCLevelOffset) ? (int)Math.Max(obCREA.ACBS.CalcMax, obCREA.ACBS.CalcMin) * obCREA.ACBS.BaseSpellPoints / 2 : obCREA.ACBS.BaseSpellPoints,
                    Fatigue = obCREA.ACBS.Flags.Contains(TES4Lib.Enums.Flags.CreatureFlag.PCLevelOffset) ? (int)Math.Max(obCREA.ACBS.CalcMax, obCREA.ACBS.CalcMin) * obCREA.ACBS.Fatigue / 2 : obCREA.ACBS.Fatigue,
                    Soul = GetSoulValue(obCREA.DATA.Soul),
                    Combat = obCREA.DATA.CombatSkill,
                    Magic = obCREA.DATA.MagicSkill,
                    Stealth = obCREA.DATA.StealthSkill,
                    AttackMin1 = (int)(0.90 * obCREA.DATA.AttackDamage),
                    AttackMax1 = obCREA.DATA.AttackDamage,
                    AttackMin2 = (int)(0.90 * obCREA.DATA.AttackDamage),
                    AttackMax2 = obCREA.DATA.AttackDamage,
                    AttackMin3 = (int)(0.90 * obCREA.DATA.AttackDamage),
                    AttackMax3 = obCREA.DATA.AttackDamage,
                    Gold = obCREA.ACBS.BarterGold,
                },
                FLAG = new TES3Lib.Subrecords.CREA.FLAG
                {
                    Flags = CastCreatureFlagsToMW(obCREA.ACBS.Flags),
                },
                NPCO = new List<TES3Lib.Subrecords.Shared.NPCO>(),
                NPCS = new List<TES3Lib.Subrecords.Shared.NPCS>(),
                AIDT = new TES3Lib.Subrecords.Shared.AIDT
                {
                    Hello = obCREA.AIDT.EnergyLevel,
                    Fight = obCREA.AIDT.Aggression,
                    Flee = obCREA.AIDT.Confidence,
                    Alarm = obCREA.AIDT.Responsibility,
                    Flags = CastServicesToMW(obCREA.AIDT.Services)
                },
                XSCL = !IsNull(obCREA.BNAM) && !obCREA.BNAM.BaseScale.Equals(1.0f) ? new TES3Lib.Subrecords.Shared.XSCL { Scale = obCREA.BNAM.BaseScale } : null
            };

            if (mwCREA.FLAG.Flags.Contains(TES3Lib.Enums.Flags.CreatureFlag.Biped))
            {
                mwCREA.MODL.ModelPath = Config.CREABiped;
            }
            else if (mwCREA.FLAG.Flags.Contains(TES3Lib.Enums.Flags.CreatureFlag.WeaponAndShield))
            {
                mwCREA.MODL.ModelPath = Config.CREAWeapon;
            }
            else if(mwCREA.FLAG.Flags.Contains(TES3Lib.Enums.Flags.CreatureFlag.Walks) && mwCREA.FLAG.Flags.Contains(TES3Lib.Enums.Flags.CreatureFlag.Swims))
            {
                mwCREA.MODL.ModelPath = Config.CREAamphibious;
            }
            else
            {
                mwCREA.MODL.ModelPath = Config.CREADefault;
            }

            if (!IsNull(obCREA.INAM))
            {
                var BaseId = GetBaseId(obCREA.INAM.ItemFormId);
                if (!string.IsNullOrEmpty(BaseId))
                {
                    mwCREA.NPCO.Add(new TES3Lib.Subrecords.Shared.NPCO { ItemId = BaseId, Count = 1 });
                }
            }

            if (!IsNull(obCREA.CNTO))
            {
                foreach (var item in obCREA.CNTO)
                {
                    var BaseId = GetBaseId(item.ItemId);
                    if (!string.IsNullOrEmpty(BaseId))
                    {
                        mwCREA.NPCO.Add(new TES3Lib.Subrecords.Shared.NPCO { ItemId = BaseId, Count = item.ItemCount });
                    }
                }
            }

            if (!IsNull(obCREA.SPLO))
            {
                foreach (var spell in obCREA.SPLO)// leveled spells not supported, might add
                {
                    var BaseId = GetBaseId(spell.SpellFormId);
                    if (!string.IsNullOrEmpty(BaseId))
                    {
                        mwCREA.NPCS.Add(new TES3Lib.Subrecords.Shared.NPCS { SpellId = BaseId });
                    }
                }
            }


            return mwCREA;
        }


        static TES3Lib.Records.LEVI ConvertLVLI(TES4Lib.Records.LVLI obLVLI)
        {
            var mwLEVI = new TES3Lib.Records.LEVI
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obLVLI.EDID.EditorId) },
                NNAM = new TES3Lib.Subrecords.Shared.NNAM { ChanceNone = obLVLI.LVLD.ChanceNone },
                DATA = new TES3Lib.Subrecords.LEVI.DATA { Flag = new HashSet<TES3Lib.Enums.Flags.LeveledItemFlag>() },
                ITEM = new List<(TES3Lib.Subrecords.LEVI.INAM INAM, TES3Lib.Subrecords.LEVI.INTV INTV)>()
            };

            if (obLVLI.LVLF.Flags.Contains(TES4Lib.Enums.Flags.LeveledItemFlag.CalcForEachItem))
                mwLEVI.DATA.Flag.Add(TES3Lib.Enums.Flags.LeveledItemFlag.CalcForEachItem);

            if (obLVLI.LVLF.Flags.Contains(TES4Lib.Enums.Flags.LeveledItemFlag.CalcFromAllLevels))
                mwLEVI.DATA.Flag.Add(TES3Lib.Enums.Flags.LeveledItemFlag.CalcFromAllLevels);

            if (IsNull(obLVLI.LVLO)) return mwLEVI;
            foreach (var leveledItem in obLVLI.LVLO)
            {
                var BaseId = GetBaseId(leveledItem.ItemFormId);
                if (!string.IsNullOrEmpty(BaseId))
                {
                    mwLEVI.ITEM.Add((new TES3Lib.Subrecords.LEVI.INAM { ItemEditorId = BaseId },
                            new TES3Lib.Subrecords.LEVI.INTV { PCLevelOfPrevious = leveledItem.Level }));
                }
            }
            mwLEVI.INDX = new TES3Lib.Subrecords.LEVI.INDX { ItemCount = mwLEVI.ITEM.Count() };

            return mwLEVI;
        }

        static TES3Lib.Records.LEVC ConvertLVLC(TES4Lib.Records.LVLC obLVLC)
        {
            var mwLEVI = new TES3Lib.Records.LEVC
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = CreatureIdFormater(obLVLC.EDID.EditorId) },
                NNAM = new TES3Lib.Subrecords.Shared.NNAM { ChanceNone = obLVLC.LVLD.ChanceNone },
                DATA = new TES3Lib.Subrecords.LEVC.DATA { Flag = new HashSet<TES3Lib.Enums.Flags.LeveledCreatureFlag>() },
                CRIT = new List<(TES3Lib.Subrecords.LEVC.CNAM CNAM, TES3Lib.Subrecords.LEVC.INTV INTV)>()
            };


            if (!IsNull(obLVLC.LVLF) && obLVLC.LVLF.Flags.Contains(TES4Lib.Enums.Flags.LeveledItemFlag.CalcFromAllLevels))
                mwLEVI.DATA.Flag.Add(TES3Lib.Enums.Flags.LeveledCreatureFlag.CalcFromAllLevels);

            if (IsNull(obLVLC.LVLO)) return mwLEVI;
            foreach (var leveledCreature in obLVLC.LVLO)
            {
                var BaseId = GetBaseId(leveledCreature.ItemFormId);
                if (!string.IsNullOrEmpty(BaseId))
                {
                    mwLEVI.CRIT.Add((new TES3Lib.Subrecords.LEVC.CNAM { CreatureEditorId = BaseId },
                            new TES3Lib.Subrecords.LEVC.INTV { PCLevelOfPrevious = leveledCreature.Level }));
                }
            }
            mwLEVI.INDX = new TES3Lib.Subrecords.LEVC.INDX { CreatureCount = mwLEVI.CRIT.Count() };

            return mwLEVI;
        }

        static TES3Lib.Records.CLOT ConvertCLOT(TES4Lib.Records.CLOT obCLOT)
        {
            var mwCLOT = new TES3Lib.Records.CLOT
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obCLOT.EDID.EditorId) },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obCLOT.MODL.ModelPath, Config.CLOTPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(!IsNull(obCLOT.FULL) ? obCLOT.FULL.DisplayName : string.Empty) },
                ITEX = new TES3Lib.Subrecords.Shared.ITEX { IconPath = PathFormater(obCLOT.ICON.IconFilePath, Config.CLOTPath) },
                CTDT = new TES3Lib.Subrecords.CLOT.CTDT
                {
                    Type = CastClothingTypeToMW(obCLOT.BMDT.BodySlots),
                    Weight = obCLOT.DATA.Weight,
                    Value = (short)obCLOT.DATA.Value,
                    EnchancmentPoints = 10,
                }
            };

            if (!IsNull(obCLOT.ENAM))
            {
                var BaseId = GetBaseId(obCLOT.ENAM.EnchantmentFormId);
                if (!string.IsNullOrEmpty(BaseId))
                {
                    mwCLOT.ENAM = new TES3Lib.Subrecords.CLOT.ENAM { EnchantmentId = BaseId };
                }
            }

            return mwCLOT;
        }

        static TES3Lib.Records.ARMO ConvertARMO(TES4Lib.Records.ARMO obARMO)
        {
            var mwARMO = new TES3Lib.Records.ARMO
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obARMO.EDID.EditorId) },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obARMO.MODL.ModelPath, Config.ARMOPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(!IsNull(obARMO.FULL) ? obARMO.FULL.DisplayName : string.Empty) },
                ITEX = new TES3Lib.Subrecords.Shared.ITEX { IconPath = PathFormater(obARMO.ICON.IconFilePath, Config.ARMOPath) },
                AODT = new TES3Lib.Subrecords.ARMO.AODT
                {
                    Type = CastArmorTypeToMW(obARMO.BMDT.BodySlots),
                    Weight = obARMO.DATA.Weight,
                    Value = (int)obARMO.DATA.Weight,
                    Health = obARMO.DATA.Health,
                    EnchancmentPoints = 10,
                    ArmorRating = obARMO.DATA.ArmorRating / 100,
                }
            };

            if (!IsNull(obARMO.ENAM))
            {
                var BaseId = GetBaseId(obARMO.ENAM.EnchantmentFormId);
                if (!string.IsNullOrEmpty(BaseId))
                {
                    mwARMO.ENAM = new TES3Lib.Subrecords.ARMO.ENAM { EnchantmentId = BaseId };
                }
            }

            return mwARMO;
        }

        static TES3Lib.Records.APPA ConvertAPPA(TES4Lib.Records.APPA obAPPA)
        {
            var mwAPPA = new TES3Lib.Records.APPA
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obAPPA.EDID.EditorId) },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obAPPA.MODL.ModelPath, Config.APPAPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(!IsNull(obAPPA.FULL) ? obAPPA.FULL.DisplayName : string.Empty) },
                ITEX = new TES3Lib.Subrecords.Shared.ITEX { IconPath = PathFormater(obAPPA.ICON.IconFilePath, Config.APPAPath) },
                AADT = new TES3Lib.Subrecords.APPA.AADT
                {
                    Type = (TES3Lib.Enums.ApparatusType)(int)obAPPA.DATA.Type,
                    Quality = obAPPA.DATA.Quality,
                    Value = obAPPA.DATA.Value,
                    Weight = obAPPA.DATA.Weight
                }
            };

            return mwAPPA;
        }

        static TES3Lib.Records.WEAP ConvertAMMO(TES4Lib.Records.AMMO obAMMO)
        {
            var mwWEAP = new TES3Lib.Records.WEAP
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obAMMO.EDID.EditorId) },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obAMMO.MODL.ModelPath, Config.WEAPPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(!IsNull(obAMMO.FULL) ? obAMMO.FULL.DisplayName : string.Empty) },
                ITEX = new TES3Lib.Subrecords.Shared.ITEX { IconPath = PathFormater(obAMMO.ICON.IconFilePath, Config.MISCPath) },
                WPDT = new TES3Lib.Subrecords.WEAP.WPDT
                {
                    Weight = obAMMO.DATA.Weight,
                    Value = obAMMO.DATA.Value,
                    Type = TES3Lib.Enums.WeaponType.Arrow,
                    Health = 10,
                    Speed = obAMMO.DATA.Speed,
                    Reach = 1,
                    EnchantmentPoints = !IsNull(obAMMO.ANAM) ? obAMMO.ANAM.EnchantmentPoints : (short)0,
                    ChopMin = (byte)(0.5f * obAMMO.DATA.Damage),
                    ChopMax = (byte)obAMMO.DATA.Damage,
                    SlashMin = 0,
                    SlashMax = 0,
                    ThrustMin = 0,
                    ThrustMax = 0,
                },
            };

            if (!IsNull(obAMMO.ENAM))
            {
                var BaseId = GetBaseId(obAMMO.ENAM.EnchantmentFormId);
                if (!string.IsNullOrEmpty(BaseId))
                {
                    mwWEAP.ENAM = new TES3Lib.Subrecords.WEAP.ENAM { EnchantmentId = BaseId };
                }
            }

            return mwWEAP;
        }

        static TES3Lib.Records.ALCH ConvertALCH(TES4Lib.Records.ALCH obALCH)
        {
            var mwALCH = new TES3Lib.Records.ALCH
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obALCH.EDID.EditorId) },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obALCH.MODL.ModelPath, Config.ALCHPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(obALCH.FULL.DisplayName) },
                TEXT = new TES3Lib.Subrecords.ALCH.TEXT { IconPath = PathFormater(obALCH.ICON.IconFilePath, Config.ALCHPath) },
                ALDT = new TES3Lib.Subrecords.ALCH.ALDT
                {
                    Flags = obALCH.ENIT.Flags.Equals(TES4Lib.Enums.Flags.AlchemyFlag.NoAutoCalculate) ? TES3Lib.Enums.Flags.AlchemyFlag.AutoCalculate : TES3Lib.Enums.Flags.AlchemyFlag.AutoCalculate,
                    Weight = obALCH.DATA.Weight,
                    Value = obALCH.ENIT.Value
                }
            };

            mwALCH.ENAM = new List<TES3Lib.Subrecords.ALCH.ENAM>();
            foreach (var effect in obALCH.EFCT)
            {
                var enam = new TES3Lib.Subrecords.ALCH.ENAM
                {
                    MagicEffect = CastMagicEffectToMW(effect.EFIT.MagicEffect),
                    Duration = effect.EFIT.Duration,
                    Magnitude = effect.EFIT.Magnitude,
                    Unknown1 = 1,
                    Unknown2 = 2,
                    Unknown3 = 3,
                };
                enam.Skill = CastActorValueToSkillMW(effect.EFIT.ActorValue, enam.MagicEffect);
                enam.Attribute = CastActorValueToAttributeMW(effect.EFIT.ActorValue, enam.MagicEffect);
                mwALCH.ENAM.Add(enam);
            }
            return mwALCH;
        }

        static TES3Lib.Records.ENCH ConvertENCH(TES4Lib.Records.ENCH obENCH)
        {
            var mwENCH = new TES3Lib.Records.ENCH
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obENCH.EDID.EditorId) },
                ENDT = new TES3Lib.Subrecords.ENCH.ENDT
                {
                    Type = CastEnchantmentTypeToMW(obENCH.ENIT.EnchantmentType),
                    AutoCalc = (int)obENCH.ENIT.Flags,
                    Charge = obENCH.ENIT.Charge,
                    EnchantCost = obENCH.ENIT.EnchantCost,
                }
            };

            mwENCH.ENAM = new List<TES3Lib.Subrecords.ENCH.ENAM>();
            foreach (var effect in obENCH.EFCT)
            {
                var enam = new TES3Lib.Subrecords.ENCH.ENAM
                {
                    MagicEffect = CastMagicEffectToMW(effect.EFIT.MagicEffect),
                    Area = effect.EFIT.Area,
                    Duration = effect.EFIT.Duration,
                    SpellRange = (TES3Lib.Enums.SpellRange)((int)effect.EFIT.SpellRange),
                    MinMagnitude = effect.EFIT.Magnitude / 2,
                    MaxMagnitude = effect.EFIT.Magnitude
                };
                enam.Skill = CastActorValueToSkillMW(effect.EFIT.ActorValue, enam.MagicEffect);
                enam.Attribute = CastActorValueToAttributeMW(effect.EFIT.ActorValue, enam.MagicEffect);
                mwENCH.ENAM.Add(enam);
            }

            return mwENCH;
        }

        static TES3Lib.Records.BOOK ConvertBOOK(TES4Lib.Records.BOOK obBOOK)
        {
            var mwBOOK = new TES3Lib.Records.BOOK
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obBOOK.EDID.EditorId) },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obBOOK.MODL.ModelPath, Config.BOOKPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(obBOOK.FULL.DisplayName) },
                ITEX = new TES3Lib.Subrecords.Shared.ITEX { IconPath = PathFormater(obBOOK.ICON.IconFilePath, Config.BOOKPath) },
                TEXT = new TES3Lib.Subrecords.BOOK.TEXT { BookText = obBOOK.DESC.Text },
                BKDT = new TES3Lib.Subrecords.BOOK.BKDT
                {
                    Weight = obBOOK.DATA.Weight,
                    Value = obBOOK.DATA.Value,
                    Skill = CastSkillToMW(obBOOK.DATA.Skill),
                    EnchantPoints = !IsNull(obBOOK.ANAM) ? obBOOK.ANAM.Points : 0,
                    Flag = obBOOK.DATA.Flags.Contains(TES4Lib.Enums.Flags.BookFlag.Scroll) ? TES3Lib.Enums.Flags.BookFlag.Scroll : TES3Lib.Enums.Flags.BookFlag.Book
                }
            };

            if (!IsNull(obBOOK.ENAM)) //convert enchamtments here
            {
                var BaseId = GetBaseId(obBOOK.ENAM.EnchantmentFormId);

                if (!string.IsNullOrEmpty(BaseId))
                {
                    mwBOOK.ENAM = new TES3Lib.Subrecords.BOOK.ENAM { EnchantmentId = BaseId };
                }
            }

            return mwBOOK;
        }

        static TES3Lib.Records.INGR ConvertINGR(TES4Lib.Records.INGR obINGR)
        {
            var mwINGR = new TES3Lib.Records.INGR
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obINGR.EDID.EditorId) },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obINGR.MODL.ModelPath, Config.INGRPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(obINGR.FULL.DisplayName) },
                ITEX = new TES3Lib.Subrecords.Shared.ITEX { IconPath = PathFormater(obINGR.ICON.IconFilePath, Config.INGRPath) },
                IRDT = new TES3Lib.Subrecords.INGR.IRDT()
            };

            //EFFECTS
            var IRDT = mwINGR.IRDT;
            IRDT.Value = obINGR.ENIT.Value;
            IRDT.Weight = obINGR.DATA.Weight;

            IRDT.EffectIds = new TES3Lib.Enums.MagicEffect[4];
            IRDT.SkillIds = new TES3Lib.Enums.Skill[4];
            IRDT.AttributeIds = new TES3Lib.Enums.Attribute[4];
            for (int i = 0; i < IRDT.EffectIds.Length; i++)
            {
                if (obINGR.EFFECT.Count > i)
                {
                    IRDT.EffectIds[i] = CastMagicEffectToMW(obINGR.EFFECT[i].EFIT.MagicEffect);
                    IRDT.SkillIds[i] = CastActorValueToSkillMW(obINGR.EFFECT[i].EFIT.ActorValue, IRDT.EffectIds[i]);
                    IRDT.AttributeIds[i] = CastActorValueToAttributeMW(obINGR.EFFECT[i].EFIT.ActorValue, IRDT.EffectIds[i]);
                }
                else
                {
                    IRDT.EffectIds[i] = TES3Lib.Enums.MagicEffect.None;
                    IRDT.SkillIds[i] = TES3Lib.Enums.Skill.Unused;
                    IRDT.AttributeIds[i] = TES3Lib.Enums.Attribute.Unused;
                }
            }
            return mwINGR;
        }

        static TES3Lib.Records.WEAP ConvertWEAP(TES4Lib.Records.WEAP obWEAP)
        {
            var mwWEAP = new TES3Lib.Records.WEAP
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obWEAP.EDID.EditorId) },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obWEAP.MODL.ModelPath, Config.WEAPPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(!IsNull(obWEAP.FULL) ? obWEAP.FULL.DisplayName : string.Empty) },
                ITEX = new TES3Lib.Subrecords.Shared.ITEX { IconPath = PathFormater(obWEAP.ICON.IconFilePath, Config.MISCPath) },
                WPDT = new TES3Lib.Subrecords.WEAP.WPDT
                {
                    Weight = obWEAP.DATA.Weight,
                    Value = obWEAP.DATA.Value,
                    Type = CastWeaponTypeToMw(obWEAP),
                    Health = (short)obWEAP.DATA.Health,
                    Speed = obWEAP.DATA.Speed,
                    Reach = obWEAP.DATA.Reach,
                    EnchantmentPoints = !IsNull(obWEAP.ANAM) ? obWEAP.ANAM.EnchantmentPoints : (short)0,
                },

            };

            if (!IsNull(obWEAP.ENAM)) //convert enchamtments here
            {
                var BaseId = GetBaseId(obWEAP.ENAM.EnchantmentFormId);

                if (!string.IsNullOrEmpty(BaseId))
                {
                    mwWEAP.ENAM = new TES3Lib.Subrecords.WEAP.ENAM { EnchantmentId = BaseId };
                }
            }

            mwWEAP.WPDT.ChopMin = (byte)(0.5f * obWEAP.DATA.Damage);
            mwWEAP.WPDT.ChopMax = (byte)obWEAP.DATA.Damage;
            mwWEAP.WPDT.SlashMin = mwWEAP.WPDT.Type.Equals(TES3Lib.Enums.WeaponType.MarksmanBow) ? (byte)0 : (byte)(0.5f * obWEAP.DATA.Damage);
            mwWEAP.WPDT.SlashMax = mwWEAP.WPDT.Type.Equals(TES3Lib.Enums.WeaponType.MarksmanBow) ? (byte)0 : (byte)(obWEAP.DATA.Damage);
            mwWEAP.WPDT.ThrustMin = (byte)(0.5f * CalcThrust(mwWEAP.WPDT.Type, obWEAP.DATA.Damage));
            mwWEAP.WPDT.ThrustMax = CalcThrust(mwWEAP.WPDT.Type, obWEAP.DATA.Damage);

            return mwWEAP;
        }

        static TES3Lib.Records.DOOR ConvertDOOR(TES4Lib.Records.DOOR obDOOR)
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

        static TES3Lib.Records.STAT ConvertFLOR2STAT(TES4Lib.Records.FLOR obFLOR)
        {
            return new TES3Lib.Records.STAT()
            {
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obFLOR.MODL.ModelPath, TES3Tool.Config.FLORPath) },
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obFLOR.EDID.EditorId) },
            };
        }

        static TES3Lib.Records.CONT ConvertFLOR2CONT(TES4Lib.Records.FLOR obFLOR)
        {
            var CONT = new TES3Lib.Records.CONT
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obFLOR.EDID.EditorId) },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obFLOR.MODL.ModelPath, TES3Tool.Config.FLORPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(obFLOR.FULL.DisplayName) },
                CNDT = new TES3Lib.Subrecords.CONT.CNDT { Weight = 0 },
                FLAG = new TES3Lib.Subrecords.CONT.FLAG { Flags = 0x0008 | 0x0001 | 0x0002 },
                NPCO = new List<TES3Lib.Subrecords.Shared.NPCO>()
        };

            if (!IsNull(obFLOR.PFIG))
            {

                var rnd = new Random((int)DateTime.Now.Ticks); //calling gods
                var fate = rnd.Next(0, 3); //pray for a sign
                var qnt = 2;
                switch (fate) //interpret gods words
                {
                    case 0:
                        qnt = (int)obFLOR.PFPC.SpringProd/5;
                        break;
                    case 1:
                        qnt = (int)obFLOR.PFPC.SummerProd/5;
                        break;
                    case 2:
                        qnt = (int)obFLOR.PFPC.FallProd/5;
                        break;
                    case 4:
                        qnt = (int)obFLOR.PFPC.WinterProd/5;
                        break;
                }

                if (qnt == 0) //if gods are cruel, fuck them
                    qnt = 2;

                var BaseId = GetBaseIdFromFormId(obFLOR.PFIG.IngredientProduced);
                if (string.IsNullOrEmpty(BaseId))
                {
                    TES4Lib.Base.Record record;
                    TES4Lib.TES4.TES4RecordIndex.TryGetValue(obFLOR.PFIG.IngredientProduced, out record);
                    if (!IsNull(record))
                    {
                        var mwRecordFromREFR = ConvertRecordFromFormId(obFLOR.PFIG.IngredientProduced);
                        if (!ConvertedRecords.ContainsKey(mwRecordFromREFR.Type)) ConvertedRecords.Add(mwRecordFromREFR.Type, new List<ConvertedRecordData>());
                        ConvertedRecords[mwRecordFromREFR.Type].Add(mwRecordFromREFR);
                        BaseId = mwRecordFromREFR.EditorId;
                    }
                }

                if (!string.IsNullOrEmpty(BaseId))
                {
                    CONT.NPCO.Add(new TES3Lib.Subrecords.Shared.NPCO { ItemId = BaseId, Count = qnt });
                }
            }

            return CONT;
        }

        static TES3Lib.Records.CONT ConvertCONT(TES4Lib.Records.CONT obCONT)
        {
            var CONT = new TES3Lib.Records.CONT
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obCONT.EDID.EditorId) },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obCONT.MODL.ModelPath, Config.CONTPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(!IsNull(obCONT.FULL) ? obCONT.FULL.DisplayName : "") },
                CNDT = new TES3Lib.Subrecords.CONT.CNDT { Weight = obCONT.DATA.Weight },
                FLAG = new TES3Lib.Subrecords.CONT.FLAG { Flags = 8 }
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
                            var mwRecordFromREFR = ConvertRecordFromFormId(item.ItemId);
                            if (!ConvertedRecords.ContainsKey(mwRecordFromREFR.Type)) ConvertedRecords.Add(mwRecordFromREFR.Type, new List<ConvertedRecordData>());
                            ConvertedRecords[mwRecordFromREFR.Type].Add(mwRecordFromREFR);
                            BaseId = mwRecordFromREFR.EditorId;
                        }
                    }
                    if (!string.IsNullOrEmpty(BaseId))
                    {
                        CONT.NPCO.Add(new TES3Lib.Subrecords.Shared.NPCO { ItemId = BaseId, Count = item.ItemCount });
                    }
                }
            }

            return CONT;
        }

        static TES3Lib.Records.MISC ConvertMISC(TES4Lib.Records.MISC obMISC)
        {
            return new TES3Lib.Records.MISC
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obMISC.EDID.EditorId) },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obMISC.MODL.ModelPath, TES3Tool.Config.MISCPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(obMISC.FULL.DisplayName) },
                MCDT = new TES3Lib.Subrecords.MISC.MCDT { Weight = obMISC.DATA.Weight, Value = obMISC.DATA.Value, Unknown = 0 },
                ITEX = new TES3Lib.Subrecords.Shared.ITEX { IconPath = PathFormater(obMISC.ICON.IconFilePath, TES3Tool.Config.MISCPath) },
                SCRI = null,
            };
        }

        static TES3Lib.Records.MISC ConvertKEYM(TES4Lib.Records.KEYM obKEYM)
        {
            return new TES3Lib.Records.MISC
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obKEYM.EDID.EditorId) },
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = PathFormater(obKEYM.MODL.ModelPath, TES3Tool.Config.KEYMPath) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(obKEYM.FULL.DisplayName) },
                MCDT = new TES3Lib.Subrecords.MISC.MCDT { Weight = obKEYM.DATA.Weight, Value = obKEYM.DATA.Value, Unknown = 0 },
                ITEX = new TES3Lib.Subrecords.Shared.ITEX { IconPath = PathFormater(obKEYM.ICON.IconFilePath, TES3Tool.Config.KEYMPath) },
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
                    Flags = CastLightFlagsToMW(obLIGH.DATA.Flags)
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
                    Volume = 255,
                    MinRange = obSOUND.SNDX.MaxAttentuationDist,// this is on purpose
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
                MODL = new TES3Lib.Subrecords.Shared.MODL() { ModelPath = PathFormater(obSTAT.MODL.ModelPath, Config.STATPath) },
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

        public static TES3Lib.Records.CELL ConvertCELL(TES4Lib.Records.CELL obCELL)
        {
            if (obCELL.Flag.Contains(TES4Lib.Enums.Flags.RecordFlag.Deleted)) return null; //we dont need deleted records for conversion

            var mwCELL = new TES3Lib.Records.CELL();
            mwCELL.NAME = new TES3Lib.Subrecords.Shared.NAME();
            mwCELL.NAME.EditorId = obCELL.FULL.DisplayName;

            mwCELL.DATA = new TES3Lib.Subrecords.CELL.DATA();

            mwCELL.DATA.Flags = new HashSet<TES3Lib.Enums.Flags.CellFlag>();
            foreach (var flag in obCELL.DATA.Flags)
            {
                switch (flag)
                {
                    case TES4Lib.Enums.Flags.CellFlag.IsInteriorCell:
                        mwCELL.DATA.Flags.Add(TES3Lib.Enums.Flags.CellFlag.IsInteriorCell);
                        break;
                    case TES4Lib.Enums.Flags.CellFlag.HasWater:
                        mwCELL.DATA.Flags.Add(TES3Lib.Enums.Flags.CellFlag.HasWater);
                        break;
                    case TES4Lib.Enums.Flags.CellFlag.PublicPlace:
                        mwCELL.DATA.Flags.Add(TES3Lib.Enums.Flags.CellFlag.IllegalToSleep);
                        break;
                    case TES4Lib.Enums.Flags.CellFlag.BehaveLikeExterior:
                        mwCELL.DATA.Flags.Add(TES3Lib.Enums.Flags.CellFlag.BehaveLikeExterior);
                        break;
                }
            }

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

        public static TES3Lib.Records.REFR ConvertREFR(TES4Lib.Records.REFR obREFR, string baseId, int refrNumber)
        {
            var mwREFR = new TES3Lib.Records.REFR();

            mwREFR.FRMR = new TES3Lib.Subrecords.REFR.FRMR();
            mwREFR.FRMR.ObjectIndex = refrNumber;

            mwREFR.NAME = new TES3Lib.Subrecords.Shared.NAME();
            mwREFR.NAME.EditorId = baseId;

            if (!IsNull(obREFR.XSCL))
            {
                mwREFR.XSCL = new TES3Lib.Subrecords.Shared.XSCL
                {
                    Scale = obREFR.XSCL.Scale <= 0.5f ? 0.5f : obREFR.XSCL.Scale
                };
            }

            //if (false)
            //{
            //    //this data should be somewhere in record flag, not relevant, at lest for now
            //    mwREFR.DELE = new TES3Lib.Subrecords.REFR.DELE();
            //}

            // TODO:if planning exterior support this needs to be modified, because it expects only interiors to be present, so if
            // no cell found, door leads to exterior space
            if (!IsNull(obREFR.XTEL))
            {
                TES4Lib.Base.Record record;
                TES4Lib.TES4.TES4RecordIndex.TryGetValue(obREFR.XTEL.DestinationDoorReference, out record);
                if (IsNull(record)) //if record is null it means its exterior cell
                {
                    float shiftX = (Config.cellShiftX * Config.mwCellSize);
                    float shiftY = (Config.cellShiftY * Config.mwCellSize);
                    mwREFR.DODT = new TES3Lib.Subrecords.Shared.DODT
                    {
                        PositionX = obREFR.XTEL.DestLocX + shiftX,
                        PositionY = obREFR.XTEL.DestLocY + shiftY,
                        PositionZ = obREFR.XTEL.DestLocZ,
                        RotationX = obREFR.XTEL.DestRotX,
                        RotationY = obREFR.XTEL.DestRotY,
                        RotationZ = obREFR.XTEL.DestRotZ
                    };
                }
                else
                {
                    mwREFR.DODT = new TES3Lib.Subrecords.Shared.DODT
                    {
                        PositionX = obREFR.XTEL.DestLocX,
                        PositionY = obREFR.XTEL.DestLocY,
                        PositionZ = obREFR.XTEL.DestLocZ,
                        RotationX = obREFR.XTEL.DestRotX,
                        RotationY = obREFR.XTEL.DestRotY,
                        RotationZ = obREFR.XTEL.DestRotZ
                    };
                    mwREFR.DNAM = new TES3Lib.Subrecords.Shared.DNAM
                    {
                        InteriorCellName = obREFR.XTEL.DestinationDoorReference //pass only formId, we will get cell names at later stages of conversion
                    };
                    DoorDestinations.Add(mwREFR.DNAM);
                }
            }

            if (!IsNull(obREFR.XLOC))
            {
                if (obREFR.XLOC.LockLevel > 0)
                {
                    mwREFR.FLTV = new TES3Lib.Subrecords.REFR.FLTV { LockLevel = obREFR.XLOC.LockLevel };
                }

                if (!obREFR.XLOC.Key.Equals("00000000"))
                {
                    var BaseId = GetBaseIdFromFormId(obREFR.XLOC.Key);
                    if (string.IsNullOrEmpty(BaseId))
                    {
                        TES4Lib.Base.Record record;
                        TES4Lib.TES4.TES4RecordIndex.TryGetValue(obREFR.XLOC.Key, out record);
                        if (!IsNull(record))
                        {
                            var mwRecordFromREFR = ConvertRecordFromFormId(obREFR.XLOC.Key);
                            if (!ConvertedRecords.ContainsKey(mwRecordFromREFR.Type)) ConvertedRecords.Add(mwRecordFromREFR.Type, new List<ConvertedRecordData>());
                            ConvertedRecords[mwRecordFromREFR.Type].Add(mwRecordFromREFR);
                            BaseId = mwRecordFromREFR.EditorId;
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
            mwREFR.NAM9 = new TES3Lib.Subrecords.REFR.NAM9 { Unknown = 0x00000001 };

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

        public static TES3Lib.Records.REFR ConvertACRE(TES4Lib.Records.ACRE obACRE, string baseId, int refrNumber)
        {
            var mwREFR = new TES3Lib.Records.REFR();

            mwREFR.FRMR = new TES3Lib.Subrecords.REFR.FRMR();
            mwREFR.FRMR.ObjectIndex = refrNumber;

            mwREFR.NAME = new TES3Lib.Subrecords.Shared.NAME();
            mwREFR.NAME.EditorId = baseId;

            //TODO: after you got all return here
            //XESP
            //XOWN
            //XGLB
            //XRNK


            if (!IsNull(obACRE.XSCL))
            {
                mwREFR.XSCL = new TES3Lib.Subrecords.Shared.XSCL
                {
                    Scale = obACRE.XSCL.Scale <= 0.5f ? 0.5f : obACRE.XSCL.Scale
                };
            }

            if (!IsNull(obACRE.DATA))
            {
                mwREFR.DATA = new TES3Lib.Subrecords.REFR.DATA();
                mwREFR.DATA.XPos = obACRE.DATA.LocX;
                mwREFR.DATA.YPos = obACRE.DATA.LocY;
                mwREFR.DATA.ZPos = obACRE.DATA.LocZ;
                mwREFR.DATA.XRotate = obACRE.DATA.RotX;
                mwREFR.DATA.YRotate = obACRE.DATA.RotY;
                mwREFR.DATA.ZRotate = obACRE.DATA.RotZ;
            }

            return mwREFR;
        }

        static byte CalcThrust(TES3Lib.Enums.WeaponType type, short damage)
        {
            float penalty = 0.75f;
            switch (type)
            {
                case TES3Lib.Enums.WeaponType.BluntTwoClose:
                case TES3Lib.Enums.WeaponType.BluntOneHand:
                case TES3Lib.Enums.WeaponType.AxeOneHand:
                case TES3Lib.Enums.WeaponType.AxeTwoHand:
                    return (byte)(penalty * damage);
                case TES3Lib.Enums.WeaponType.MarksmanBow:
                    return 0;

                default:
                    return (byte)damage;
            }
        }

        static TES3Lib.Enums.ArmorType CastArmorTypeToMW(HashSet<TES4Lib.Enums.BodySlot> bipedObjectFlags)
        {
            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.UpperBody)) return TES3Lib.Enums.ArmorType.Cuirass;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Foot)) return TES3Lib.Enums.ArmorType.Boots;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Hand)) return TES3Lib.Enums.ArmorType.LGauntlet;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Head) || bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Hair))
                return TES3Lib.Enums.ArmorType.Helmet;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.LowerBody)) return TES3Lib.Enums.ArmorType.Greaves;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Shield)) return TES3Lib.Enums.ArmorType.Shield;

            return TES3Lib.Enums.ArmorType.Cuirass;
        }

        static TES3Lib.Enums.ClothingType CastClothingTypeToMW(HashSet<TES4Lib.Enums.BodySlot> bipedObjectFlags)
        {
            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Amulet))
                return TES3Lib.Enums.ClothingType.Amulet;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.RightRing) || bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.LeftRing))
                return TES3Lib.Enums.ClothingType.Ring;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.UpperBody))
                return TES3Lib.Enums.ClothingType.Skirt;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Foot))
                return TES3Lib.Enums.ClothingType.Shoes;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Hand))
                return TES3Lib.Enums.ClothingType.LeftGlove;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Head) || bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Hair))
                return TES3Lib.Enums.ClothingType.Belt;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.LowerBody))
                return TES3Lib.Enums.ClothingType.Pants;

            return TES3Lib.Enums.ClothingType.Robe;
        }

        static TES3Lib.Enums.WeaponType CastWeaponTypeToMw(TES4Lib.Records.WEAP obWEAP)
        {
            if (obWEAP.DATA.Type.Equals(TES4Lib.Enums.WeaponType.BladeOneHand))//blade one hand
            {
                if (obWEAP.EDID.EditorId.ToLower().Contains("dagger") || obWEAP.FULL.DisplayName.ToLower().Contains("dagger"))
                    return TES3Lib.Enums.WeaponType.ShortBladeOneHand;

                return TES3Lib.Enums.WeaponType.LongBladeOneHand;
            }
            if (obWEAP.DATA.Type.Equals(TES4Lib.Enums.WeaponType.BladeTwoHand))//blade two hand
            {
                return TES3Lib.Enums.WeaponType.LongBladeTwoClose;
            }
            if (obWEAP.DATA.Type.Equals(TES4Lib.Enums.WeaponType.BluntOneHand))//blunt one hand
            {
                if (obWEAP.EDID.EditorId.ToLower().Contains("axe") || obWEAP.FULL.DisplayName.ToLower().Contains("axe"))
                    return TES3Lib.Enums.WeaponType.AxeOneHand;

                return TES3Lib.Enums.WeaponType.BluntOneHand;
            }
            if (obWEAP.DATA.Type.Equals(TES4Lib.Enums.WeaponType.BluntTwoHand))//blunt two hand
            {
                if (obWEAP.EDID.EditorId.ToLower().Contains("axe") || obWEAP.FULL.DisplayName.ToLower().Contains("axe"))
                    return TES3Lib.Enums.WeaponType.AxeTwoHand;

                return TES3Lib.Enums.WeaponType.BluntTwoClose;
            }
            if (obWEAP.DATA.Type.Equals(TES4Lib.Enums.WeaponType.Staff))//staff
            {
                return TES3Lib.Enums.WeaponType.BluntTwoWide;
            }
            else
            {
                return TES3Lib.Enums.WeaponType.MarksmanBow;
            }
        }

        static TES3Lib.Enums.MagicEffect CastMagicEffectToMW(TES4Lib.Enums.MagicEffect magicEffect)
        {
            switch (magicEffect)
            {
                case TES4Lib.Enums.MagicEffect.BoundPriestDagger:
                    return TES3Lib.Enums.MagicEffect.BoundDagger;
                case TES4Lib.Enums.MagicEffect.SummonSewnFleshAtronach:
                    return TES3Lib.Enums.MagicEffect.SummonLeastBonewalker;
                case TES4Lib.Enums.MagicEffect.SummonStitchedFleshAtronach:
                    return TES3Lib.Enums.MagicEffect.SummonLeastBonewalker;
                case TES4Lib.Enums.MagicEffect.SummonTornFleshAtronach:
                    return TES3Lib.Enums.MagicEffect.SummonGreaterBonewalker;
                case TES4Lib.Enums.MagicEffect.SummonMangledFleshAtronach:
                    return TES3Lib.Enums.MagicEffect.SummonGreaterBonewalker;
                case TES4Lib.Enums.MagicEffect.SummonHunger:
                    return TES3Lib.Enums.MagicEffect.SummonHunger;
                case TES4Lib.Enums.MagicEffect.BoundOrderWeapon6:
                    return TES3Lib.Enums.MagicEffect.BoundLongsword;
                case TES4Lib.Enums.MagicEffect.BoundOrderWeapon5:
                    return TES3Lib.Enums.MagicEffect.BoundLongsword;
                case TES4Lib.Enums.MagicEffect.BoundOrderWeapon4:
                    return TES3Lib.Enums.MagicEffect.BoundLongsword;
                case TES4Lib.Enums.MagicEffect.BoundOrderWeapon3:
                    return TES3Lib.Enums.MagicEffect.BoundLongsword;
                case TES4Lib.Enums.MagicEffect.BoundOrderWeapon2:
                    return TES3Lib.Enums.MagicEffect.BoundLongsword;
                case TES4Lib.Enums.MagicEffect.BoundOrderWeapon1:
                    return TES3Lib.Enums.MagicEffect.BoundLongsword;
                case TES4Lib.Enums.MagicEffect.SummonStaffofSheogorath:
                    return TES3Lib.Enums.MagicEffect.BoundSpear;
                case TES4Lib.Enums.MagicEffect.WabbaSummon:
                    return TES3Lib.Enums.MagicEffect.BoundMace;
                case TES4Lib.Enums.MagicEffect.SummonGoldenSaint:
                    return TES3Lib.Enums.MagicEffect.SummonGoldenSaint;
                case TES4Lib.Enums.MagicEffect.SummonVoraciousHunger:
                    return TES3Lib.Enums.MagicEffect.SummonHunger;
                case TES4Lib.Enums.MagicEffect.SummonRavenousHunger:
                    return TES3Lib.Enums.MagicEffect.SummonHunger;
                case TES4Lib.Enums.MagicEffect.SummonDarkSeducer:
                    return TES3Lib.Enums.MagicEffect.SummonGoldenSaint;
                case TES4Lib.Enums.MagicEffect.SummonGluttonousHunger:
                    return TES3Lib.Enums.MagicEffect.SummonHunger;
                case TES4Lib.Enums.MagicEffect.SummonRepleteShambles:
                    return TES3Lib.Enums.MagicEffect.SummonSkeleton;
                case TES4Lib.Enums.MagicEffect.SummonShambles:
                    return TES3Lib.Enums.MagicEffect.SummonSkeleton;
                case TES4Lib.Enums.MagicEffect.SummonDecrepitShambles:
                    return TES3Lib.Enums.MagicEffect.SummonSkeleton;
                case TES4Lib.Enums.MagicEffect.DONOTUSEDarkness:
                    return TES3Lib.Enums.MagicEffect.Blind;
                case TES4Lib.Enums.MagicEffect.Rally:
                    return TES3Lib.Enums.MagicEffect.RallyHumanoid;
                case TES4Lib.Enums.MagicEffect.Frenzy:
                    return TES3Lib.Enums.MagicEffect.FrenzyHumanoid;
                case TES4Lib.Enums.MagicEffect.Charm:
                    return TES3Lib.Enums.MagicEffect.Charm;
                case TES4Lib.Enums.MagicEffect.Calm:
                    return TES3Lib.Enums.MagicEffect.CalmCreature;
                case TES4Lib.Enums.MagicEffect.Demoralize:
                    return TES3Lib.Enums.MagicEffect.DemoralizeHumanoid;
                case TES4Lib.Enums.MagicEffect.SummonMythicDawnHelm:
                    return TES3Lib.Enums.MagicEffect.BoundHelm;
                case TES4Lib.Enums.MagicEffect.PoisonInfo:
                    return TES3Lib.Enums.MagicEffect.Poison;
                case TES4Lib.Enums.MagicEffect.SummonBear:
                    return TES3Lib.Enums.MagicEffect.SummonBear;
                case TES4Lib.Enums.MagicEffect.SummonFleshAtronach:
                    return TES3Lib.Enums.MagicEffect.SummonGreaterBonewalker;
                case TES4Lib.Enums.MagicEffect.SummonSpiderling:
                    return TES3Lib.Enums.MagicEffect.SummonScamp;
                case TES4Lib.Enums.MagicEffect.SummonAncestorGuardian:
                    return TES3Lib.Enums.MagicEffect.SummonGhost;
                case TES4Lib.Enums.MagicEffect.SummonRufiosGhost:
                    return TES3Lib.Enums.MagicEffect.SummonGhost;
                case TES4Lib.Enums.MagicEffect.Reanimate:
                    return TES3Lib.Enums.MagicEffect.SummonGhost;
                case TES4Lib.Enums.MagicEffect.SummonXivilai:
                    return TES3Lib.Enums.MagicEffect.SummonDremora;
                case TES4Lib.Enums.MagicEffect.SummonGloomWraith:
                    return TES3Lib.Enums.MagicEffect.SummonBonelord;
                case TES4Lib.Enums.MagicEffect.SummonSpiderDaedra:
                    return TES3Lib.Enums.MagicEffect.SummonTwilight;
                case TES4Lib.Enums.MagicEffect.SummonSkeletonHero:
                    return TES3Lib.Enums.MagicEffect.SummonSkeleton;
                case TES4Lib.Enums.MagicEffect.SummonSkeletonChampion:
                    return TES3Lib.Enums.MagicEffect.SummonSkeleton;
                case TES4Lib.Enums.MagicEffect.SummonSkeletonGuardian:
                    return TES3Lib.Enums.MagicEffect.SummonSkeleton;
                case TES4Lib.Enums.MagicEffect.SummonHeadlessZombie:
                    return TES3Lib.Enums.MagicEffect.SummonLeastBonewalker;
                case TES4Lib.Enums.MagicEffect.SummonDremoraLord:
                    return TES3Lib.Enums.MagicEffect.SummonDremora;
                case TES4Lib.Enums.MagicEffect.SummonMythicDawnArmor:
                    return TES3Lib.Enums.MagicEffect.BoundCuirass;
                case TES4Lib.Enums.MagicEffect.AbsorbHealth:
                    return TES3Lib.Enums.MagicEffect.AbsorbHealth;
                case TES4Lib.Enums.MagicEffect.AbsorbSkill:
                    return TES3Lib.Enums.MagicEffect.AbsorbSkill;
                case TES4Lib.Enums.MagicEffect.AbsorbMagicka:
                    return TES3Lib.Enums.MagicEffect.AbsorbMagicka;
                case TES4Lib.Enums.MagicEffect.AbsorbAttribute:
                    return TES3Lib.Enums.MagicEffect.AbsorbAttribute;
                case TES4Lib.Enums.MagicEffect.BoundBoots:
                    return TES3Lib.Enums.MagicEffect.BoundBoots;
                case TES4Lib.Enums.MagicEffect.AbsorbFatigue:
                    return TES3Lib.Enums.MagicEffect.AbsorbFatigue;
                case TES4Lib.Enums.MagicEffect.BoundCuirass:
                    return TES3Lib.Enums.MagicEffect.BoundCuirass;
                case TES4Lib.Enums.MagicEffect.BoundGauntlets:
                    return TES3Lib.Enums.MagicEffect.BoundGloves;
                case TES4Lib.Enums.MagicEffect.BoundGreaves:
                    return TES3Lib.Enums.MagicEffect.BoundSpear;
                case TES4Lib.Enums.MagicEffect.BoundHelmet:
                    return TES3Lib.Enums.MagicEffect.BoundHelm;
                case TES4Lib.Enums.MagicEffect.BoundShield:
                    return TES3Lib.Enums.MagicEffect.BoundShield;
                case TES4Lib.Enums.MagicEffect.Burden:
                    return TES3Lib.Enums.MagicEffect.Burden;
                case TES4Lib.Enums.MagicEffect.BoundAxe:
                    return TES3Lib.Enums.MagicEffect.BoundBattleAxe;
                case TES4Lib.Enums.MagicEffect.BoundBow:
                    return TES3Lib.Enums.MagicEffect.BoundLongbow;
                case TES4Lib.Enums.MagicEffect.BoundDagger:
                    return TES3Lib.Enums.MagicEffect.BoundDagger;
                case TES4Lib.Enums.MagicEffect.BoundMace:
                    return TES3Lib.Enums.MagicEffect.BoundMace;
                case TES4Lib.Enums.MagicEffect.BoundSword:
                    return TES3Lib.Enums.MagicEffect.BoundLongsword;
                case TES4Lib.Enums.MagicEffect.Chameleon:
                    return TES3Lib.Enums.MagicEffect.Chameleon;
                case TES4Lib.Enums.MagicEffect.CommandCreature:
                    return TES3Lib.Enums.MagicEffect.CommandCreature;
                case TES4Lib.Enums.MagicEffect.CommandHumanoid:
                    return TES3Lib.Enums.MagicEffect.CommandHumanoid;
                case TES4Lib.Enums.MagicEffect.CureDisease:
                    return TES3Lib.Enums.MagicEffect.CureCommonDisease;
                case TES4Lib.Enums.MagicEffect.CureParalysis:
                    return TES3Lib.Enums.MagicEffect.CureParalyzation;
                case TES4Lib.Enums.MagicEffect.CurePoison:
                    return TES3Lib.Enums.MagicEffect.CurePoison;
                case TES4Lib.Enums.MagicEffect.DamageAttribute:
                    return TES3Lib.Enums.MagicEffect.DamageAttribute;
                case TES4Lib.Enums.MagicEffect.DamageFatigue:
                    return TES3Lib.Enums.MagicEffect.DamageFatigue;
                case TES4Lib.Enums.MagicEffect.DamageHealth:
                    return TES3Lib.Enums.MagicEffect.DamageHealth;
                case TES4Lib.Enums.MagicEffect.DamageMagicka:
                    return TES3Lib.Enums.MagicEffect.DamageMagicka;
                case TES4Lib.Enums.MagicEffect.DisintegrateArmor:
                    return TES3Lib.Enums.MagicEffect.DisintegrateArmor;
                case TES4Lib.Enums.MagicEffect.DisintegrateWeapon:
                    return TES3Lib.Enums.MagicEffect.DisintegrateWeapon;
                case TES4Lib.Enums.MagicEffect.DrainAttribute:
                    return TES3Lib.Enums.MagicEffect.DrainAttribute;
                case TES4Lib.Enums.MagicEffect.DrainFatigue:
                    return TES3Lib.Enums.MagicEffect.DrainFatigue;
                case TES4Lib.Enums.MagicEffect.DrainHealth:
                    return TES3Lib.Enums.MagicEffect.DrainHealth;
                case TES4Lib.Enums.MagicEffect.DrainSkill:
                    return TES3Lib.Enums.MagicEffect.DrainSkill;
                case TES4Lib.Enums.MagicEffect.DrainMagicka:
                    return TES3Lib.Enums.MagicEffect.DrainMagicka;
                case TES4Lib.Enums.MagicEffect.Dispel:
                    return TES3Lib.Enums.MagicEffect.Dispel;
                case TES4Lib.Enums.MagicEffect.DetectLife:
                    return TES3Lib.Enums.MagicEffect.DetectAnimal;
                case TES4Lib.Enums.MagicEffect.FireDamage:
                    return TES3Lib.Enums.MagicEffect.FireDamage;
                case TES4Lib.Enums.MagicEffect.FireShield:
                    return TES3Lib.Enums.MagicEffect.FireShield;
                case TES4Lib.Enums.MagicEffect.FortifyAttribute:
                    return TES3Lib.Enums.MagicEffect.FortifyAttribute;
                case TES4Lib.Enums.MagicEffect.FortifyFatigue:
                    return TES3Lib.Enums.MagicEffect.FortifyFatigue;
                case TES4Lib.Enums.MagicEffect.FortifyHealth:
                    return TES3Lib.Enums.MagicEffect.FortifyHealth;
                case TES4Lib.Enums.MagicEffect.FortifyMagickaMultiplier:
                    return TES3Lib.Enums.MagicEffect.FortifyMagickaMultiplier;
                case TES4Lib.Enums.MagicEffect.FortifySkill:
                    return TES3Lib.Enums.MagicEffect.FortifySkill;
                case TES4Lib.Enums.MagicEffect.FortifyMagicka:
                    return TES3Lib.Enums.MagicEffect.FortifyMagicka;
                case TES4Lib.Enums.MagicEffect.FrostDamage:
                    return TES3Lib.Enums.MagicEffect.FortifyAttackBonus;
                case TES4Lib.Enums.MagicEffect.FrostShield:
                    return TES3Lib.Enums.MagicEffect.FrostShield;
                case TES4Lib.Enums.MagicEffect.Feather:
                    return TES3Lib.Enums.MagicEffect.Feather;
                case TES4Lib.Enums.MagicEffect.Invisibility:
                    return TES3Lib.Enums.MagicEffect.Invisibility;
                case TES4Lib.Enums.MagicEffect.Light:
                    return TES3Lib.Enums.MagicEffect.Light;
                case TES4Lib.Enums.MagicEffect.ShockShield:
                    return TES3Lib.Enums.MagicEffect.LightningShield;
                case TES4Lib.Enums.MagicEffect.DONOTUSELock:
                    return TES3Lib.Enums.MagicEffect.Lock;
                case TES4Lib.Enums.MagicEffect.NightEye:
                    return TES3Lib.Enums.MagicEffect.NightEye;
                case TES4Lib.Enums.MagicEffect.Open:
                    return TES3Lib.Enums.MagicEffect.Open;
                case TES4Lib.Enums.MagicEffect.Paralyze:
                    return TES3Lib.Enums.MagicEffect.Paralyze;
                case TES4Lib.Enums.MagicEffect.RestoreAttribute:
                    return TES3Lib.Enums.MagicEffect.RestoreAttribute;
                case TES4Lib.Enums.MagicEffect.ReflectDamage:
                    return TES3Lib.Enums.MagicEffect.Reflect;
                case TES4Lib.Enums.MagicEffect.RestoreFatigue:
                    return TES3Lib.Enums.MagicEffect.RestoreFatigue;
                case TES4Lib.Enums.MagicEffect.RestoreHealth:
                    return TES3Lib.Enums.MagicEffect.RestoreHealth;
                case TES4Lib.Enums.MagicEffect.RestoreMagicka:
                    return TES3Lib.Enums.MagicEffect.RestoreMagicka;
                case TES4Lib.Enums.MagicEffect.ReflectSpell:
                    return TES3Lib.Enums.MagicEffect.Reflect;
                case TES4Lib.Enums.MagicEffect.ResistDisease:
                    return TES3Lib.Enums.MagicEffect.ResistCommonDisease;
                case TES4Lib.Enums.MagicEffect.ResistFire:
                    return TES3Lib.Enums.MagicEffect.ResistFire;
                case TES4Lib.Enums.MagicEffect.ResistFrost:
                    return TES3Lib.Enums.MagicEffect.ResistFrost;
                case TES4Lib.Enums.MagicEffect.ResistMagic:
                    return TES3Lib.Enums.MagicEffect.ResistMagicka;
                case TES4Lib.Enums.MagicEffect.ResistNormalWeapons:
                    return TES3Lib.Enums.MagicEffect.ResistNormalWeapons;
                case TES4Lib.Enums.MagicEffect.ResistParalysis:
                    return TES3Lib.Enums.MagicEffect.ResistParalysis;
                case TES4Lib.Enums.MagicEffect.ResistPoison:
                    return TES3Lib.Enums.MagicEffect.ResistPoison;
                case TES4Lib.Enums.MagicEffect.ResistShock:
                    return TES3Lib.Enums.MagicEffect.ResistShock;
                case TES4Lib.Enums.MagicEffect.SpellAbsorption:
                    return TES3Lib.Enums.MagicEffect.SpellAbsorption;
                case TES4Lib.Enums.MagicEffect.ScriptEffect:
                    return TES3Lib.Enums.MagicEffect.None;
                case TES4Lib.Enums.MagicEffect.ShockDamage:
                    return TES3Lib.Enums.MagicEffect.ShockDamage;
                case TES4Lib.Enums.MagicEffect.Shield:
                    return TES3Lib.Enums.MagicEffect.Shield;
                case TES4Lib.Enums.MagicEffect.Silence:
                    return TES3Lib.Enums.MagicEffect.Silence;
                case TES4Lib.Enums.MagicEffect.StuntedMagicka:
                    return TES3Lib.Enums.MagicEffect.StuntedMagicka;
                case TES4Lib.Enums.MagicEffect.SoulTrap:
                    return TES3Lib.Enums.MagicEffect.SoulTrap;
                case TES4Lib.Enums.MagicEffect.SunDamage:
                    return TES3Lib.Enums.MagicEffect.SunDamage;
                case TES4Lib.Enums.MagicEffect.Telekinesis:
                    return TES3Lib.Enums.MagicEffect.Telekinesis;
                case TES4Lib.Enums.MagicEffect.TurnUndead:
                    return TES3Lib.Enums.MagicEffect.TurnUndead;
                case TES4Lib.Enums.MagicEffect.Vampirism:
                    return TES3Lib.Enums.MagicEffect.Vampirism;
                case TES4Lib.Enums.MagicEffect.WaterBreathing:
                    return TES3Lib.Enums.MagicEffect.WaterBreathing;
                case TES4Lib.Enums.MagicEffect.WaterWalking:
                    return TES3Lib.Enums.MagicEffect.WaterWalking;
                case TES4Lib.Enums.MagicEffect.WeaknesstoDisease:
                    return TES3Lib.Enums.MagicEffect.WeaknessToCommonDisease;
                case TES4Lib.Enums.MagicEffect.WeaknesstoFire:
                    return TES3Lib.Enums.MagicEffect.WeaknessToFire;
                case TES4Lib.Enums.MagicEffect.WeaknesstoFrost:
                    return TES3Lib.Enums.MagicEffect.WeaknessToFrost;
                case TES4Lib.Enums.MagicEffect.WeaknesstoMagic:
                    return TES3Lib.Enums.MagicEffect.WeaknessToMagicka;
                case TES4Lib.Enums.MagicEffect.WeaknesstoNormalWeapons:
                    return TES3Lib.Enums.MagicEffect.WeaknessToNormalWeapons;
                case TES4Lib.Enums.MagicEffect.WeaknesstoPoison:
                    return TES3Lib.Enums.MagicEffect.WeaknessToPoison;
                case TES4Lib.Enums.MagicEffect.WeaknesstoShock:
                    return TES3Lib.Enums.MagicEffect.WeaknessToShock;
                case TES4Lib.Enums.MagicEffect.SummonClannfear:
                    return TES3Lib.Enums.MagicEffect.SummonClannfear;
                case TES4Lib.Enums.MagicEffect.SummonDaedroth:
                    return TES3Lib.Enums.MagicEffect.SummonDaedroth;
                case TES4Lib.Enums.MagicEffect.SummonDremora:
                    return TES3Lib.Enums.MagicEffect.SummonDremora;
                case TES4Lib.Enums.MagicEffect.SummonFlameAtronach:
                    return TES3Lib.Enums.MagicEffect.SummonFlameAtronach;
                case TES4Lib.Enums.MagicEffect.SummonFrostAtronach:
                    return TES3Lib.Enums.MagicEffect.SummonFrostAtronach;
                case TES4Lib.Enums.MagicEffect.SummonGhost:
                    return TES3Lib.Enums.MagicEffect.SummonGhost;
                case TES4Lib.Enums.MagicEffect.SummonLich:
                    return TES3Lib.Enums.MagicEffect.SummonBonelord;
                case TES4Lib.Enums.MagicEffect.SummonScamp:
                    return TES3Lib.Enums.MagicEffect.SummonScamp;
                case TES4Lib.Enums.MagicEffect.SummonSkeleton:
                    return TES3Lib.Enums.MagicEffect.SummonSkeleton;
                case TES4Lib.Enums.MagicEffect.SummonStormAtronach:
                    return TES3Lib.Enums.MagicEffect.SummonStormAtronach;
                case TES4Lib.Enums.MagicEffect.SummonFadedWraith:
                    return TES3Lib.Enums.MagicEffect.SummonGhost;
                case TES4Lib.Enums.MagicEffect.SummonZombie:
                    return TES3Lib.Enums.MagicEffect.SummonLeastBonewalker;
                default:
                    return TES3Lib.Enums.MagicEffect.None;
            }

            //internal static TES3Lib.Records.REFR ConvertACHR(TES4Lib.Records.REFR obREFR, string baseId, int refrNumber)
            //{

            //}

            //internal static TES3Lib.Records.REFR ConvertACRE(TES4Lib.Records.REFR obREFR, string baseId, int refrNumber)
            //{

            //}
        }

        static TES3Lib.Enums.Attribute CastActorValueToAttributeMW(TES4Lib.Enums.ActorValue actorValue, TES3Lib.Enums.MagicEffect magicEffect)
        {
            bool isAttributeEffect =
                magicEffect.Equals(TES3Lib.Enums.MagicEffect.AbsorbAttribute) ||
                magicEffect.Equals(TES3Lib.Enums.MagicEffect.DamageAttribute) ||
                magicEffect.Equals(TES3Lib.Enums.MagicEffect.DrainAttribute) ||
                magicEffect.Equals(TES3Lib.Enums.MagicEffect.FortifyAttribute) ||
                magicEffect.Equals(TES3Lib.Enums.MagicEffect.RestoreAttribute);
            if (!isAttributeEffect) return TES3Lib.Enums.Attribute.Unused;

            return (TES3Lib.Enums.Attribute)Enum.Parse(typeof(TES3Lib.Enums.Attribute), actorValue.ToString());
        }

        static TES3Lib.Enums.Skill CastActorValueToSkillMW(TES4Lib.Enums.ActorValue actorValue, TES3Lib.Enums.MagicEffect magicEffect)
        {
            bool isSkillEffect =
               magicEffect.Equals(TES3Lib.Enums.MagicEffect.AbsorbSkill) ||
               magicEffect.Equals(TES3Lib.Enums.MagicEffect.DamageSkill) ||
               magicEffect.Equals(TES3Lib.Enums.MagicEffect.DrainSkill) ||
               magicEffect.Equals(TES3Lib.Enums.MagicEffect.FortifySkill) ||
               magicEffect.Equals(TES3Lib.Enums.MagicEffect.RestoreSkill);
            if (!isSkillEffect) return TES3Lib.Enums.Skill.Unused;

            var rnd = new Random(DateTime.Now.Millisecond).Next(0, 2);
            switch (actorValue)
            {
                case TES4Lib.Enums.ActorValue.Armorer:
                    return TES3Lib.Enums.Skill.Armorer;
                case TES4Lib.Enums.ActorValue.Athletics:
                    return TES3Lib.Enums.Skill.Athletics;
                case TES4Lib.Enums.ActorValue.Blade:
                    return rnd.Equals(0) ? TES3Lib.Enums.Skill.ShortBlade : TES3Lib.Enums.Skill.LongBlade;
                case TES4Lib.Enums.ActorValue.Block:
                    return TES3Lib.Enums.Skill.Block;
                case TES4Lib.Enums.ActorValue.Blunt:
                    return rnd.Equals(0) ? TES3Lib.Enums.Skill.BluntWeapon : TES3Lib.Enums.Skill.Axe;
                case TES4Lib.Enums.ActorValue.HandToHand:
                    return TES3Lib.Enums.Skill.HandToHand;
                case TES4Lib.Enums.ActorValue.HeavyArmor:
                    return rnd.Equals(0) ? TES3Lib.Enums.Skill.HeavyArmor : TES3Lib.Enums.Skill.MediumArmor;
                case TES4Lib.Enums.ActorValue.Alchemy:
                    return TES3Lib.Enums.Skill.Alchemy;
                case TES4Lib.Enums.ActorValue.Alteration:
                    return TES3Lib.Enums.Skill.Alteration;
                case TES4Lib.Enums.ActorValue.Conjuration:
                    return TES3Lib.Enums.Skill.Conjuration;
                case TES4Lib.Enums.ActorValue.Destruction:
                    return TES3Lib.Enums.Skill.Destruction;
                case TES4Lib.Enums.ActorValue.Illusion:
                    return TES3Lib.Enums.Skill.Illusion;
                case TES4Lib.Enums.ActorValue.Mysticism:
                    return TES3Lib.Enums.Skill.Mysticism;
                case TES4Lib.Enums.ActorValue.Restoration:
                    return TES3Lib.Enums.Skill.Restoration;
                case TES4Lib.Enums.ActorValue.Acrobatics:
                    return TES3Lib.Enums.Skill.Acrobatics;
                case TES4Lib.Enums.ActorValue.LightArmor:
                    return rnd.Equals(0) ? TES3Lib.Enums.Skill.LightArmor : TES3Lib.Enums.Skill.MediumArmor;
                case TES4Lib.Enums.ActorValue.Marksman:
                    return TES3Lib.Enums.Skill.Marksman;
                case TES4Lib.Enums.ActorValue.Mercantile:
                    return TES3Lib.Enums.Skill.Mercantile;
                case TES4Lib.Enums.ActorValue.Security:
                    return TES3Lib.Enums.Skill.Security;
                case TES4Lib.Enums.ActorValue.Sneak:
                    return TES3Lib.Enums.Skill.Sneak;
                case TES4Lib.Enums.ActorValue.Speechcraft:
                    return TES3Lib.Enums.Skill.Speechcraft;
                default:
                    return TES3Lib.Enums.Skill.Unused;
            }
        }

        static TES3Lib.Enums.Skill CastSkillToMW(TES4Lib.Enums.Skill skill)
        {
            var rnd = new Random(DateTime.Now.Millisecond).Next(0, 2);
            switch (skill)
            {
                case TES4Lib.Enums.Skill.Armorer:
                    return TES3Lib.Enums.Skill.Armorer;
                case TES4Lib.Enums.Skill.Athletics:
                    return TES3Lib.Enums.Skill.Athletics;
                case TES4Lib.Enums.Skill.Blade:
                    return rnd.Equals(0) ? TES3Lib.Enums.Skill.ShortBlade : TES3Lib.Enums.Skill.LongBlade;
                case TES4Lib.Enums.Skill.Block:
                    return TES3Lib.Enums.Skill.Block;
                case TES4Lib.Enums.Skill.Blunt:
                    return rnd.Equals(0) ? TES3Lib.Enums.Skill.BluntWeapon : TES3Lib.Enums.Skill.Axe;
                case TES4Lib.Enums.Skill.HandToHand:
                    return TES3Lib.Enums.Skill.HandToHand;
                case TES4Lib.Enums.Skill.HeavyArmor:
                    return rnd.Equals(0) ? TES3Lib.Enums.Skill.HeavyArmor : TES3Lib.Enums.Skill.MediumArmor;
                case TES4Lib.Enums.Skill.Alchemy:
                    return TES3Lib.Enums.Skill.Alchemy;
                case TES4Lib.Enums.Skill.Alteration:
                    return TES3Lib.Enums.Skill.Alteration;
                case TES4Lib.Enums.Skill.Conjuration:
                    return TES3Lib.Enums.Skill.Conjuration;
                case TES4Lib.Enums.Skill.Destruction:
                    return TES3Lib.Enums.Skill.Destruction;
                case TES4Lib.Enums.Skill.Illusion:
                    return TES3Lib.Enums.Skill.Illusion;
                case TES4Lib.Enums.Skill.Mysticism:
                    return TES3Lib.Enums.Skill.Mysticism;
                case TES4Lib.Enums.Skill.Restoration:
                    return TES3Lib.Enums.Skill.Restoration;
                case TES4Lib.Enums.Skill.Acrobatics:
                    return TES3Lib.Enums.Skill.Acrobatics;
                case TES4Lib.Enums.Skill.LightArmor:
                    return rnd.Equals(0) ? TES3Lib.Enums.Skill.LightArmor : TES3Lib.Enums.Skill.MediumArmor;
                case TES4Lib.Enums.Skill.Marksman:
                    return TES3Lib.Enums.Skill.Marksman;
                case TES4Lib.Enums.Skill.Mercantile:
                    return TES3Lib.Enums.Skill.Mercantile;
                case TES4Lib.Enums.Skill.Security:
                    return TES3Lib.Enums.Skill.Security;
                case TES4Lib.Enums.Skill.Sneak:
                    return TES3Lib.Enums.Skill.Sneak;
                case TES4Lib.Enums.Skill.Speechcraft:
                    return TES3Lib.Enums.Skill.Speechcraft;
                default:
                    return TES3Lib.Enums.Skill.Unused;
            }
        }

        static TES3Lib.Enums.CreatureType CastCreatureTypeToMW(TES4Lib.Enums.CreatureType creatureType)
        {
            switch (creatureType)
            {
                case TES4Lib.Enums.CreatureType.Creature:
                    return TES3Lib.Enums.CreatureType.Creature;
                case TES4Lib.Enums.CreatureType.Daedra:
                    return TES3Lib.Enums.CreatureType.Daedra;
                case TES4Lib.Enums.CreatureType.Undead:
                    return TES3Lib.Enums.CreatureType.Undead;
                case TES4Lib.Enums.CreatureType.Humanoid:
                    return TES3Lib.Enums.CreatureType.Humanoid;
                case TES4Lib.Enums.CreatureType.Horse:
                    return TES3Lib.Enums.CreatureType.Creature;
                case TES4Lib.Enums.CreatureType.Giant:
                    return TES3Lib.Enums.CreatureType.Humanoid;
                default:
                    return TES3Lib.Enums.CreatureType.Creature;
            }
        }

        static TES3Lib.Enums.EnchantmentType CastEnchantmentTypeToMW(TES4Lib.Enums.EnchantmentType enchantmentType)
        {
            switch (enchantmentType)
            {
                case TES4Lib.Enums.EnchantmentType.Scroll:
                    return TES3Lib.Enums.EnchantmentType.CastOnce;
                case TES4Lib.Enums.EnchantmentType.Staff:
                    return TES3Lib.Enums.EnchantmentType.CastOnStrike;
                case TES4Lib.Enums.EnchantmentType.Weapon:
                    return TES3Lib.Enums.EnchantmentType.CastOnStrike;
                case TES4Lib.Enums.EnchantmentType.Apparel:
                    return TES3Lib.Enums.EnchantmentType.ConstantEffect;
                default:
                    return TES3Lib.Enums.EnchantmentType.CastWhenUsed;
            }
        }

        static HashSet<TES3Lib.Enums.Flags.ServicesFlag> CastServicesToMW(HashSet<TES4Lib.Enums.Flags.ServicesFlag> obFLag)
        {
            var mwFlags = new HashSet<TES3Lib.Enums.Flags.ServicesFlag>();

            foreach (var flag in obFLag)
            {
                switch (flag)
                {
                    case TES4Lib.Enums.Flags.ServicesFlag.Weapons:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Weapon);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Armor:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Armor);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Clothing:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Clothing);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Books:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Books);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Ingredients:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Ingredients);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Lights:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Lights);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Apparatus:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Apparatus);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Misc:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Misc);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Spells:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Spells);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.MagicItems:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.MagicItems);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Potions:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Potions);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Training:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Training);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Recharge:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Spellmaking);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Repair:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Repair);
                        break;
                }
            }

            return mwFlags;
        }

        static HashSet<TES3Lib.Enums.Flags.LightFlag> CastLightFlagsToMW(HashSet<TES4Lib.Enums.Flags.LightFlag> obFlags)
        {
            var mwFlags = new HashSet<TES3Lib.Enums.Flags.LightFlag>();
            foreach (var flag in obFlags)
            {
                switch (flag)
                {
                    case TES4Lib.Enums.Flags.LightFlag.Dynamic:
                        mwFlags.Add(TES3Lib.Enums.Flags.LightFlag.Dynamic);
                        break;
                    case TES4Lib.Enums.Flags.LightFlag.CanBeCarried:
                        mwFlags.Add(TES3Lib.Enums.Flags.LightFlag.CanCarry);
                        break;
                    case TES4Lib.Enums.Flags.LightFlag.Negative:
                        mwFlags.Add(TES3Lib.Enums.Flags.LightFlag.Negative);
                        break;
                    case TES4Lib.Enums.Flags.LightFlag.Flicker:
                        mwFlags.Add(TES3Lib.Enums.Flags.LightFlag.Flicker);
                        break;
                    case TES4Lib.Enums.Flags.LightFlag.OffByDefault:
                        mwFlags.Add(TES3Lib.Enums.Flags.LightFlag.OffDefault);
                        break;
                    case TES4Lib.Enums.Flags.LightFlag.FlickerSlow:
                        mwFlags.Add(TES3Lib.Enums.Flags.LightFlag.FlickerSlow);
                        break;
                    case TES4Lib.Enums.Flags.LightFlag.Pulse:
                        mwFlags.Add(TES3Lib.Enums.Flags.LightFlag.Pulse);
                        break;
                    case TES4Lib.Enums.Flags.LightFlag.PulseSlow:
                        mwFlags.Add(TES3Lib.Enums.Flags.LightFlag.PulseSlow);
                        break;
                }
            }

            return mwFlags;
        }

        static HashSet<TES3Lib.Enums.Flags.CreatureFlag> CastCreatureFlagsToMW(HashSet<TES4Lib.Enums.Flags.CreatureFlag> obFlags)
        {
            var convertedFlags = new HashSet<TES3Lib.Enums.Flags.CreatureFlag>();

            foreach (var flag in obFlags)
            {
                switch (flag)
                {
                    case TES4Lib.Enums.Flags.CreatureFlag.Biped:
                        convertedFlags.Add(TES3Lib.Enums.Flags.CreatureFlag.Biped);
                        break;
                    case TES4Lib.Enums.Flags.CreatureFlag.Essential:
                        convertedFlags.Add(TES3Lib.Enums.Flags.CreatureFlag.Essential);
                        break;
                    case TES4Lib.Enums.Flags.CreatureFlag.WeaponAndShield:
                        convertedFlags.Add(TES3Lib.Enums.Flags.CreatureFlag.WeaponAndShield);
                        break;
                    case TES4Lib.Enums.Flags.CreatureFlag.Respawn:
                        convertedFlags.Add(TES3Lib.Enums.Flags.CreatureFlag.Respawn);
                        break;
                    case TES4Lib.Enums.Flags.CreatureFlag.Swims:
                        convertedFlags.Add(TES3Lib.Enums.Flags.CreatureFlag.Swims);
                        break;
                    case TES4Lib.Enums.Flags.CreatureFlag.Flies:
                        convertedFlags.Add(TES3Lib.Enums.Flags.CreatureFlag.Flies);
                        break;
                    case TES4Lib.Enums.Flags.CreatureFlag.Walks:
                        convertedFlags.Add(TES3Lib.Enums.Flags.CreatureFlag.Walks);
                        break;
                }
            }
            return convertedFlags;
        }

        static int GetSoulValue(TES4Lib.Enums.SoulGemType soul)
        {
            switch (soul)
            {
                case SoulGemType.None: // gingers here?
                    return 0;
                case SoulGemType.Petty:
                    return 30;
                case SoulGemType.Lesser:
                    return 60;
                case SoulGemType.Common:
                    return 105;
                case SoulGemType.Greather:
                    return 165;
                case SoulGemType.Grand:
                    return 400;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Gets EditorId for FormId, if referenced record is not converted, then it converts it as well
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        private static string GetBaseId(string formId)
        {
            var BaseId = GetBaseIdFromFormId(formId);
            if (string.IsNullOrEmpty(BaseId))
            {
                TES4Lib.Base.Record record;
                TES4Lib.TES4.TES4RecordIndex.TryGetValue(formId, out record);
                if (!IsNull(record))
                {
                    var mwRecordFromREFR = ConvertRecordFromFormId(formId);
                    if (!ConvertedRecords.ContainsKey(mwRecordFromREFR.Type)) ConvertedRecords.Add(mwRecordFromREFR.Type, new List<ConvertedRecordData>());
                    ConvertedRecords[mwRecordFromREFR.Type].Add(mwRecordFromREFR);
                    BaseId = mwRecordFromREFR.EditorId;
                }
            }
            return BaseId;
        }
    }
}
