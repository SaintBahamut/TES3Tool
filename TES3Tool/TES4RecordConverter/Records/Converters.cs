using static TES3Tool.TES4RecordConverter.Records.Helpers;
using static Utility.Common;
using static TES3Tool.TES4RecordConverter.Oblivion2Morrowind;
using static TES3Tool.TES4RecordConverter.Records.TypeConverters;
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

            //NPC
            if (recordType.Equals("NPC_"))
            {
                var mwNPC = ConvertNPC((TES4Lib.Records.NPC_)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwNPC.GetType().Name, mwNPC.NAME.EditorId, mwNPC);
            }

            //RACE
            if (recordType.Equals("RACE"))
            {
                var mwRACE = ConvertRACE((TES4Lib.Records.RACE)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwRACE.GetType().Name, mwRACE.NAME.EditorId, mwRACE);
            }

            //CLASS
            if (recordType.Equals("CLAS"))
            {
                var mwCLAS = ConvertCLAS((TES4Lib.Records.CLAS)obRecord);
                return new ConvertedRecordData(obRecord.FormId, mwCLAS.GetType().Name, mwCLAS.NAME.EditorId, mwCLAS);
            }

            return null;
        }

        private static TES3Lib.Records.RACE ConvertRACE(TES4Lib.Records.RACE obRACE)
        {
            var mwRACE = new TES3Lib.Records.RACE
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = EditorIdFormater(obRACE.EDID.EditorId) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = obRACE.FULL.DisplayName },
                DESC = new TES3Lib.Subrecords.Shared.DESC { Description = obRACE.DESC.Description },
                RADT = new TES3Lib.Subrecords.RACE.RADT
                {
                    Male = new TES3Lib.Subrecords.RACE.RADT.Attributes
                    {
                        Strength = obRACE.ATTR.Male.Strength,
                        Intelligence = obRACE.ATTR.Male.Intelligence,
                        Willpower = obRACE.ATTR.Male.Willpower,
                        Agility = obRACE.ATTR.Male.Agility,
                        Speed = obRACE.ATTR.Male.Speed,
                        Endurance = obRACE.ATTR.Male.Endurance,
                        Personality = obRACE.ATTR.Male.Personality,
                        Luck = obRACE.ATTR.Male.Luck,
                        Height = obRACE.DATA.MaleHeight,
                        Weight = obRACE.DATA.MaleWeight
                    },
                    Female = new TES3Lib.Subrecords.RACE.RADT.Attributes
                    {
                        Strength = obRACE.ATTR.Female.Strength,
                        Intelligence = obRACE.ATTR.Female.Intelligence,
                        Willpower = obRACE.ATTR.Female.Willpower,
                        Agility = obRACE.ATTR.Female.Agility,
                        Speed = obRACE.ATTR.Female.Speed,
                        Endurance = obRACE.ATTR.Female.Endurance,
                        Personality = obRACE.ATTR.Female.Personality,
                        Luck = obRACE.ATTR.Female.Luck,
                        Height = obRACE.DATA.FemaleHeight,
                        Weight = obRACE.DATA.FemaleWeight
                    },
                    SkillBonuses = new TES3Lib.Subrecords.RACE.RADT.SkillBonus[7],
                    Flags = new HashSet<RaceFlags>()
                    
                },
                NPCS = new List<TES3Lib.Subrecords.Shared.NPCS>()
            };

            if (obRACE.DATA.IsPlayable) mwRACE.RADT.Flags.Add(TES3Lib.Enums.Flags.RaceFlags.Playable);

            for (int i = 0; i < obRACE.DATA.SkillBoosts.Length; i++)
            {
                mwRACE.RADT.SkillBonuses[i].Skill = CastActorValueToSkillMW(obRACE.DATA.SkillBoosts[i].Skill);
                mwRACE.RADT.SkillBonuses[i].Bonus = obRACE.DATA.SkillBoosts[i].Bonus;
            }
             
            if (!IsNull(obRACE.SPLO))
            {
                foreach (var spell in obRACE.SPLO)
                {
                    var BaseId = GetBaseId(spell.SpellFormId);
                    if (!string.IsNullOrEmpty(BaseId))
                    {
                        mwRACE.NPCS.Add(new TES3Lib.Subrecords.Shared.NPCS { SpellId = BaseId });
                    }
                }
            }

            return mwRACE;
        }

        private static TES3Lib.Records.CLAS ConvertCLAS(TES4Lib.Records.CLAS obCLAS)
        {
            var mwCLAS = new TES3Lib.Records.CLAS
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = CreatureIdFormater(obCLAS.EDID.EditorId) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(!IsNull(obCLAS.FULL) ? obCLAS.FULL.DisplayName : string.Empty) },
                DESC = new TES3Lib.Subrecords.Shared.DESC {  Description = obCLAS.DESC.Description },
                CLDT = new TES3Lib.Subrecords.CLAS.CLDT
                {
                    PrimaryAttribute1 = CastActorValueToAttributeMW(obCLAS.DATA.PrimaryAttribute1),
                    PrimaryAttribute2 = CastActorValueToAttributeMW(obCLAS.DATA.PrimaryAttribute2),
                    Specialization = (TES3Lib.Enums.Specialization)obCLAS.DATA.Specialization,
                    IsPlayable = obCLAS.DATA.Flags.Contains(ClassFlag.Playable) ? 1 : 0,
                    Services = CastServicesToMW(obCLAS.DATA.Services),
                    Major1 = CastActorValueToSkillMW(obCLAS.DATA.MajorSkills[0]),
                    Major2 = CastActorValueToSkillMW(obCLAS.DATA.MajorSkills[1]),
                    Major3 = CastActorValueToSkillMW(obCLAS.DATA.MajorSkills[2]),
                    Major4 = CastActorValueToSkillMW(obCLAS.DATA.MajorSkills[3]),
                    Major5 = CastActorValueToSkillMW(obCLAS.DATA.MajorSkills[4]),
                    Minor1 = CastActorValueToSkillMW(obCLAS.DATA.MajorSkills[5]),
                    Minor2 = CastActorValueToSkillMW(obCLAS.DATA.MajorSkills[6]),
                    Minor3 = TES3Lib.Enums.Skill.ShortBlade,
                    Minor4 = TES3Lib.Enums.Skill.Axe,
                    Minor5 = TES3Lib.Enums.Skill.Unarmored,
                }
            };

            return mwCLAS;
        }

        private static TES3Lib.Records.NPC_ ConvertNPC(TES4Lib.Records.NPC_ obNPC)
        {
            var mwNPC = new TES3Lib.Records.NPC_
            {
                NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = CreatureIdFormater(obNPC.EDID.EditorId) },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(!IsNull(obNPC.FULL) ? obNPC.FULL.DisplayName : string.Empty) },
                ANAM = new TES3Lib.Subrecords.Shared.ANAM { EditorId = "\0" },
                KNAM = new TES3Lib.Subrecords.NPC_.KNAM { HairModel = "\0" },
                BNAM = new TES3Lib.Subrecords.Shared.BNAM { EditorId = "\0" },
                CNAM = new TES3Lib.Subrecords.NPC_.CNAM { ClassName = "Warrior\0" },
                FLAG = new TES3Lib.Subrecords.NPC_.FLAG { Flags = CastNPCFlagsToMW(obNPC.ACBS.Flags) },
                NPCO = new List<TES3Lib.Subrecords.Shared.NPCO>(),
                NPCS = new List<TES3Lib.Subrecords.Shared.NPCS>(),
                AIDT = new TES3Lib.Subrecords.Shared.AIDT
                {
                    Hello = obNPC.AIDT.EnergyLevel,
                    Fight = obNPC.AIDT.Aggression,
                    Flee = obNPC.AIDT.Confidence,
                    Alarm = obNPC.AIDT.Responsibility,
                    Flags = CastServicesToMW(obNPC.AIDT.Services)
                },
              
            };

            string ClassName = GetBaseId(obNPC.CNAM.FormId);
            if (!string.IsNullOrEmpty(ClassName))
            {
                mwNPC.CNAM.ClassName = ClassName;
            }
            

            string RaceId = GetBaseId(obNPC.RNAM.RaceFormId);
            if (string.IsNullOrEmpty(RaceId))
            {
                RaceId = "Dark Elf\0";
                //throw new Exception($"Can't find race of form id {obNPC.RNAM.RaceFormId}");
            }
            mwNPC.RNAM = new TES3Lib.Subrecords.NPC_.RNAM { RaceName = RaceId };

            //oblivion actors have many factions, im getting just first one
            if (!IsNull(obNPC.SNAM))
            {
                string FactionId = GetBaseId(obNPC.SNAM[0].FormId);
                mwNPC.ANAM.EditorId = FactionId;
            }

            //oblivion actors have many factions, im getting just first one
            if (!IsNull(obNPC.SNAM))
            {
                string FactionId = GetBaseId(obNPC.SNAM[0].FormId);
                mwNPC.ANAM.EditorId = FactionId;
            }

            if (IsStandardMWRace(RaceId))
            {
                var rnd = new Random(DateTime.Now.Millisecond);
                var gender = obNPC.ACBS.Flags.Contains(TES4Lib.Enums.Flags.NPCFlag.Female) ? 'F' : 'M';
                var key = $"{RaceId} {gender}";
                int headTypesCount = Config.MWRaceFaces[key].Count;
                int hairTypesCount = Config.MWRaceHairs[key].Count;

                mwNPC.KNAM = new TES3Lib.Subrecords.NPC_.KNAM { HairModel = Config.MWRaceHairs[key][rnd.Next(0, hairTypesCount)] };
                mwNPC.BNAM = new TES3Lib.Subrecords.Shared.BNAM { EditorId = Config.MWRaceFaces[key][rnd.Next(0, headTypesCount)] };
            }
            mwNPC.NPDT = new TES3Lib.Subrecords.NPC_.NPDT
            {
                Level = obNPC.ACBS.Flags.Contains(TES4Lib.Enums.Flags.NPCFlag.PCLevelOffset) ? (short)Math.Max(obNPC.ACBS.CalcMax, obNPC.ACBS.CalcMin) : (short)obNPC.ACBS.Level,
                Strength = obNPC.DATA.Strength,
                Intelligence = obNPC.DATA.Intelligence,
                Willpower = obNPC.DATA.Willpower,
                Agility = obNPC.DATA.Agility,
                Speed = obNPC.DATA.Speed,
                Endurance = obNPC.DATA.Endurance,
                Personality = obNPC.DATA.Personality,
                Luck = obNPC.DATA.Luck,
                Skills = new byte[] {
                    obNPC.DATA.Block, obNPC.DATA.Armorer, obNPC.DATA.LightArmor, obNPC.DATA.HeavyArmor,
                    obNPC.DATA.Blunt, obNPC.DATA.Blade, obNPC.DATA.Blunt, obNPC.DATA.Blunt, obNPC.DATA.Athletics,
                    obNPC.DATA.Mysticism, obNPC.DATA.Destruction, obNPC.DATA.Alteration, obNPC.DATA.Illusion,
                    obNPC.DATA.Conjuration, obNPC.DATA.Mysticism, obNPC.DATA.Restoration, obNPC.DATA.Alchemy,
                    10 ,obNPC.DATA.Security, obNPC.DATA.Sneak, obNPC.DATA.Acrobatics,
                    obNPC.DATA.LightArmor, obNPC.DATA.Blade, obNPC.DATA.Marksman,obNPC.DATA.Mercantile,
                    obNPC.DATA.Speechcraft, obNPC.DATA.HandToHand},
                Disposition = 50,
                Health = obNPC.ACBS.Flags.Contains(TES4Lib.Enums.Flags.NPCFlag.PCLevelOffset) ? (short)(Math.Max(obNPC.ACBS.CalcMax, obNPC.ACBS.CalcMin) * obNPC.DATA.Health / 2) : (short)obNPC.DATA.Health,
                SpellPts = obNPC.ACBS.Flags.Contains(TES4Lib.Enums.Flags.NPCFlag.PCLevelOffset) ? (short)(Math.Max(obNPC.ACBS.CalcMax, obNPC.ACBS.CalcMin) * obNPC.ACBS.BaseSpellPoints / 2) : (short)obNPC.ACBS.BaseSpellPoints,
                Fatigue = obNPC.ACBS.Flags.Contains(TES4Lib.Enums.Flags.NPCFlag.PCLevelOffset) ? (short)(Math.Max(obNPC.ACBS.CalcMax, obNPC.ACBS.CalcMin) * obNPC.ACBS.Fatigue / 2) : (short)obNPC.ACBS.Fatigue,
                Reputation = 0,
                Rank = !IsNull(obNPC.SNAM) ? obNPC.SNAM[0].Rank : (byte)0,
                Gold = obNPC.ACBS.Gold,
            };

            if (!IsNull(obNPC.INAM))
            {
                var BaseId = GetBaseId(obNPC.INAM.ItemFormId);
                if (!string.IsNullOrEmpty(BaseId))
                {
                    mwNPC.NPCO.Add(new TES3Lib.Subrecords.Shared.NPCO { ItemId = BaseId, Count = 1 });
                }
            }

            if (!IsNull(obNPC.CNTO))
            {
                foreach (var item in obNPC.CNTO)
                {
                    var BaseId = GetBaseId(item.ItemId);
                    if (!string.IsNullOrEmpty(BaseId))
                    {
                        mwNPC.NPCO.Add(new TES3Lib.Subrecords.Shared.NPCO { ItemId = BaseId, Count = item.ItemCount });
                    }
                }
            }

            if (!IsNull(obNPC.SPLO))
            {
                foreach (var spell in obNPC.SPLO)// leveled spells not supported, might add
                {
                    var BaseId = GetBaseId(spell.SpellFormId);
                    if (!string.IsNullOrEmpty(BaseId))
                    {
                        mwNPC.NPCS.Add(new TES3Lib.Subrecords.Shared.NPCS { SpellId = BaseId });
                    }
                }
            }

            return mwNPC;
        }

        static TES3Lib.Records.CREA ConvertCREA(TES4Lib.Records.CREA obCREA)
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
                    Health = obCREA.ACBS.Flags.Contains(TES4Lib.Enums.Flags.CreatureFlag.PCLevelOffset) ? (int)Math.Max(obCREA.ACBS.CalcMax, obCREA.ACBS.CalcMin) * obCREA.DATA.Health / 2 : obCREA.DATA.Health,
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
            else if (mwCREA.FLAG.Flags.Contains(TES3Lib.Enums.Flags.CreatureFlag.Walks) && mwCREA.FLAG.Flags.Contains(TES3Lib.Enums.Flags.CreatureFlag.Swims))
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
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = !IsNull(obCLOT.MODL) ? PathFormater(obCLOT.MODL.ModelPath, Config.CLOTPath) : "\0" },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(!IsNull(obCLOT.FULL) ? obCLOT.FULL.DisplayName : string.Empty) },
                ITEX = new TES3Lib.Subrecords.Shared.ITEX { IconPath = !IsNull(obCLOT.ICON) ? PathFormater(obCLOT.ICON.IconFilePath, Config.CLOTPath) : "\0" },
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
                MODL = new TES3Lib.Subrecords.Shared.MODL { ModelPath = !IsNull(obARMO.MODL) ? PathFormater(obARMO.MODL.ModelPath, Config.ARMOPath) : "\0" },
                FNAM = new TES3Lib.Subrecords.Shared.FNAM { Name = NameFormater(!IsNull(obARMO.FULL) ? obARMO.FULL.DisplayName : string.Empty) },
                ITEX = new TES3Lib.Subrecords.Shared.ITEX { IconPath = !IsNull(obARMO.ICON) ? PathFormater(obARMO.ICON.IconFilePath, Config.ARMOPath) : "\0" },
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
                enam.Skill = CastActorValueToSkillEffectMW(effect.EFIT.ActorValue, enam.MagicEffect);
                enam.Attribute = CastActorValueToAttributeEffectMW(effect.EFIT.ActorValue, enam.MagicEffect);
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
                enam.Skill = CastActorValueToSkillEffectMW(effect.EFIT.ActorValue, enam.MagicEffect);
                enam.Attribute = CastActorValueToAttributeEffectMW(effect.EFIT.ActorValue, enam.MagicEffect);
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
                    IRDT.SkillIds[i] = CastActorValueToSkillEffectMW(obINGR.EFFECT[i].EFIT.ActorValue, IRDT.EffectIds[i]);
                    IRDT.AttributeIds[i] = CastActorValueToAttributeEffectMW(obINGR.EFFECT[i].EFIT.ActorValue, IRDT.EffectIds[i]);
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

                mwDOOR.SNAM = new TES3Lib.Subrecords.Shared.SNAM { SoundEditorId = BaseId };
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

                mwDOOR.ANAM = new TES3Lib.Subrecords.Shared.ANAM { EditorId = BaseId };
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
                        qnt = (int)obFLOR.PFPC.SpringProd / 12;
                        break;
                    case 1:
                        qnt = (int)obFLOR.PFPC.SummerProd / 12;
                        break;
                    case 2:
                        qnt = (int)obFLOR.PFPC.FallProd / 12;
                        break;
                    case 4:
                        qnt = (int)obFLOR.PFPC.WinterProd / 12;
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

                LIGH.SNAM = new TES3Lib.Subrecords.Shared.SNAM { SoundEditorId = BaseId };
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
                FNAM = new TES3Lib.Subrecords.Shared.FNAM() { Name = !IsNull(obACTI.FULL) ? NameFormater(obACTI.FULL.DisplayName) : "\0" },
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
                    Name = $"Sound_{BaseId}", //write formatter dawg
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

            mwCELL.NAME = new TES3Lib.Subrecords.Shared.NAME { EditorId = !IsNull(obCELL.FULL) ? obCELL.FULL.DisplayName : (!IsNull(obCELL.EDID) ? obCELL.EDID.EditorId : "\0") };

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

            if (!IsNull(obCELL.XCLC)) //exterior only
            {
                int mwScale = 2;
                mwCELL.DATA.GridX = (obCELL.XCLC.GridX / mwScale) + Config.cellShiftX;
                mwCELL.DATA.GridY = (obCELL.XCLC.GridY / mwScale) + Config.cellShiftY;
            }

            if (!IsNull(obCELL.XCLR)) //TODO region entry converter
            {
                ///obCELL.XCLR.RegionsContainingCell
                mwCELL.RGNN = new TES3Lib.Subrecords.CELL.RGNN { RegionName = "Sheogorad\0" };//TEEEEEEEEEEST
            }

            // not needed? cell.NAM0 = new TES3Lib.Subrecords.CELL.NAM0();
            // exterior only cell.NAM5 = new TES3Lib.Subrecords.CELL.NAM5();


            if (!IsNull(obCELL.XCLW))
            {
                mwCELL.WHGT = new TES3Lib.Subrecords.CELL.WHGT();
                mwCELL.WHGT.WaterHeight = obCELL.XCLW.WaterHeight;
            }

            if (!IsNull(obCELL.XCLL))
            {
                mwCELL.AMBI = new TES3Lib.Subrecords.CELL.AMBI();
                mwCELL.AMBI.AmbientColor = obCELL.XCLL.Ambient;
                mwCELL.AMBI.SunlightColor = obCELL.XCLL.Directional; //mabye not a good idea
                mwCELL.AMBI.FogColor = obCELL.XCLL.Fog; //missing
                mwCELL.AMBI.FogDensity = 1.0f;
            }
            //else
            //{
            //    mwCELL.AMBI.AmbientColor = 0;
            //    mwCELL.AMBI.SunlightColor = 0;
            //    mwCELL.AMBI.FogColor = 0;
            //    mwCELL.AMBI.FogDensity = 1.0f;
            //}

            return mwCELL;
        }

        public static TES3Lib.Records.REFR ConvertREFR(TES4Lib.Records.REFR obREFR, string baseId, int refrNumber, bool IsInteriorCell)
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

            if (!IsNull(obREFR.XTEL))
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
                    EditorId = obREFR.XTEL.DestinationDoorReference //pass only formId, we will get cell names at later stages of conversion
                };

                DoorReferences.Add(mwREFR);
            }

            if (!IsNull(obREFR.XLOC))
            {
                //if (obREFR.XLOC.LockLevel > 0)
                //{
                //    mwREFR.FLTV = new TES3Lib.Subrecords.REFR.FLTV { LockLevel = obREFR.XLOC.LockLevel };
                //}

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
                    //mwREFR.KNAM = new TES3Lib.Subrecords.REFR.KNAM { DoorKeyId = BaseId };

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
                int offsetX = IsInteriorCell ? 0 : (Config.mwCellSize * Config.cellShiftX);
                int offsetY = IsInteriorCell ? 0 : (Config.mwCellSize * Config.cellShiftY);

                mwREFR.DATA = new TES3Lib.Subrecords.REFR.DATA();
                mwREFR.DATA.XPos = obREFR.DATA.LocX + offsetX;
                mwREFR.DATA.YPos = obREFR.DATA.LocY + offsetY;
                mwREFR.DATA.ZPos = obREFR.DATA.LocZ;
                mwREFR.DATA.XRotate = obREFR.DATA.RotX;
                mwREFR.DATA.YRotate = obREFR.DATA.RotY;
                mwREFR.DATA.ZRotate = obREFR.DATA.RotZ;
            }

            return mwREFR;
        }

        public static TES3Lib.Records.REFR ConvertACRE(TES4Lib.Records.ACRE obACRE, string baseId, int refrNumber, bool IsInteriorCell)
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
                int offsetX = IsInteriorCell ? 0 : (Config.mwCellSize * Config.cellShiftX);
                int offsetY = IsInteriorCell ? 0 : (Config.mwCellSize * Config.cellShiftY);

                mwREFR.DATA = new TES3Lib.Subrecords.REFR.DATA();
                mwREFR.DATA.XPos = obACRE.DATA.LocX + offsetX;
                mwREFR.DATA.YPos = obACRE.DATA.LocY + offsetY;
                mwREFR.DATA.ZPos = obACRE.DATA.LocZ;
                mwREFR.DATA.XRotate = obACRE.DATA.RotX;
                mwREFR.DATA.YRotate = obACRE.DATA.RotY;
                mwREFR.DATA.ZRotate = obACRE.DATA.RotZ;
            }

            return mwREFR;
        }

        public static TES3Lib.Records.REFR ConvertACHR(TES4Lib.Records.ACHR obACHR, string baseId, int refrNumber, bool IsInteriorCell)
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


            //if (!IsNull(obACHR.XSCL))
            //{
            //    mwREFR.XSCL = new TES3Lib.Subrecords.Shared.XSCL
            //    {
            //        Scale = obACHR.XSCL.Scale <= 0.5f ? 0.5f : obACHR.XSCL.Scale
            //    };
            //}

            if (!IsNull(obACHR.DATA))
            {
                int offsetX = IsInteriorCell ? 0 : (Config.mwCellSize * Config.cellShiftX);
                int offsetY = IsInteriorCell ? 0 : (Config.mwCellSize * Config.cellShiftY);

                mwREFR.DATA = new TES3Lib.Subrecords.REFR.DATA
                {
                    XPos = obACHR.DATA.XPos + offsetX,
                    YPos = obACHR.DATA.YPos + offsetY,
                    ZPos = obACHR.DATA.ZPos,
                    XRotate = obACHR.DATA.RotX,
                    YRotate = obACHR.DATA.RotY,
                    ZRotate = obACHR.DATA.RotZ
                };
            }

            return mwREFR;
        }

        public static TES3Lib.Records.PGRD ConvertPGRD(TES4Lib.Records.PGRD obPGRD, TES3Lib.Records.CELL mwCELL)
        {
            var mwPGRD = new TES3Lib.Records.PGRD();

            bool isExterior = !mwCELL.DATA.Flags.Contains(TES3Lib.Enums.Flags.CellFlag.IsInteriorCell);
            int offsetX = isExterior ? Config.cellShiftX * Config.mwCellSize : 0;
            int offsetY = isExterior ? Config.cellShiftY * Config.mwCellSize : 0;


            mwPGRD.DATA = new TES3Lib.Subrecords.PGRD.DATA
            {
                GridX = isExterior ? mwCELL.DATA.GridX : 0,
                GridY = isExterior ? mwCELL.DATA.GridY : 0,
                Granularity = 256, //just insert whatever, might work
                Points = obPGRD.DATA.NumberOfNodes
            };

            mwPGRD.NAME = new TES3Lib.Subrecords.Shared.NAME();
            if (!IsNull(mwCELL.NAME) && !mwCELL.NAME.EditorId.Equals("\0"))
            {
                mwPGRD.NAME.EditorId = mwCELL.NAME.EditorId;
            }
            else if (!IsNull(mwCELL.RGNN))
            {
                mwPGRD.NAME.EditorId = mwCELL.RGNN.RegionName;
            }
            else
            {
                mwPGRD.NAME.EditorId = "Wilderness\0"; //just a guess
            }

            mwPGRD.PGRP = new TES3Lib.Subrecords.PGRD.PGRP();
            mwPGRD.PGRP.Points = new TES3Lib.Subrecords.PGRD.PGRP.Point[obPGRD.PGRP.Points.Length];
            for (int i = 0; i < mwPGRD.PGRP.Points.Length; i++)
            {
                mwPGRD.PGRP.Points[i].X = Convert.ToInt32(obPGRD.PGRP.Points[i].X) + offsetX;
                mwPGRD.PGRP.Points[i].Y = Convert.ToInt32(obPGRD.PGRP.Points[i].Y) + offsetY;
                mwPGRD.PGRP.Points[i].Z = Convert.ToInt32(obPGRD.PGRP.Points[i].Z);
                mwPGRD.PGRP.Points[i].IsUserPoint = 1;
                mwPGRD.PGRP.Points[i].EdgeCount = obPGRD.PGRP.Points[i].EdgeCount;
                mwPGRD.PGRP.Points[i].Unknown1 = 0;
                mwPGRD.PGRP.Points[i].Unknown2 = 0;
            }

            if (!IsNull(obPGRD.PGRR))
            {
                mwPGRD.PGRC = new TES3Lib.Subrecords.PGRD.PGRC
                {
                    Edges = new int[obPGRD.PGRR.Edges.Length]
                };

                for (int i = 0; i < mwPGRD.PGRC.Edges.Length; i++)
                {
                    mwPGRD.PGRC.Edges[i] = (int)obPGRD.PGRR.Edges[i];
                }
            }

            return mwPGRD;
        }

        /// <summary>
        /// Gets EditorId for FormId, if referenced record is not converted, then it converts it as well
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public static string GetBaseId(string formId)
        {
            var BaseId = GetBaseIdFromFormId(formId);
            if (string.IsNullOrEmpty(BaseId))
            {
                var mwRecordFromREFR = ConvertRecordFromFormId(formId);

                if (IsNull(mwRecordFromREFR)) return string.Empty;

                if (!ConvertedRecords.ContainsKey(mwRecordFromREFR.Type)) ConvertedRecords.Add(mwRecordFromREFR.Type, new List<ConvertedRecordData>());
                ConvertedRecords[mwRecordFromREFR.Type].Add(mwRecordFromREFR);
                BaseId = mwRecordFromREFR.EditorId;
            }
            return BaseId;
        }
    }
}
