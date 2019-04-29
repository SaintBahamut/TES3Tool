using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utility.Common;

namespace TES3Tool.TES4RecordConverter.Records
{
    internal static class Helpers
    {
        internal static Dictionary<string, List<ConvertedRecordData>> ConvertedRecords = new Dictionary<string, List<ConvertedRecordData>>();

        internal static List<TES3Lib.Records.REFR> DoorReferences = new List<TES3Lib.Records.REFR>();

        internal static List<ConvertedCellReference> CellReferences = new List<ConvertedCellReference>();

        internal static string GenerateSoundScript(string SoundEditorId)
        {
            string template = "begin Sound__PLACEHOLDER_\r\n\r\nif(CellChanged == 0)\r\n\tif(GetSoundPlaying \"_PLACEHOLDER_\" == 0 )\r\n\t\tPlayLoopSound3DVP \"_PLACEHOLDER_\", 1.0, 1.0\r\n\tendif\r\nendif\r\n\r\nend";
            return template.Replace("_PLACEHOLDER_", SoundEditorId);
        }  

        internal static void UpdateDoorReferences()
        {
            Parallel.ForEach(DoorReferences, doorREFR =>
            {
                var reference = CellReferences
               .FirstOrDefault(x => x.ReferenceFormId.Equals(doorREFR.DNAM.InteriorCellName));

                if (IsNull(reference))
                {
                    return;
                }

                var record = ConvertedRecords["CELL"]
                .FirstOrDefault(x => x.OriginFormId.Contains(reference.ParentCellFormId)) ;

                if (IsNull(record))
                {
                    return;
                }

                var cell = record.Record as TES3Lib.Records.CELL;

                //Here we can try support outliners

                if(!cell.DATA.Flags.Contains(TES3Lib.Enums.Flags.CellFlag.IsInteriorCell))
                {

                    float shiftX = (Config.cellShiftX * Config.mwCellSize);
                    float shiftY = (Config.cellShiftY * Config.mwCellSize);
                    doorREFR.DODT.PositionX += shiftX;
                    doorREFR.DODT.PositionY += shiftY;
                    doorREFR.DNAM = null;
                }
                else
                {
                    doorREFR.DNAM.InteriorCellName = cell.NAME.EditorId;
                }            
            });
        }

        internal static string GetBaseIdFromFormId(string formId)
        {
            string standardRecord = GetDefaultIdFromFormId(formId);
            if (!string.IsNullOrEmpty(standardRecord)) return standardRecord;

            string BaseId = string.Empty;

            var group = ConvertedRecords.AsParallel()
                .FirstOrDefault(x => x.Value.Exists(y => y.OriginFormId.Equals(formId)));
            var result = !IsNull(group.Key) ? group.Value.AsParallel().FirstOrDefault(x => x.OriginFormId.Equals(formId)) : null;
            BaseId = !IsNull(result) ?  result.EditorId : string.Empty;

            return BaseId;
        }

        internal static string PathFormater(string sourcePath, string containingFolder)
        {
            string fileName = sourcePath.Split('\\').Last();

            string outputPath = $"{Config.convertedRootFolder}\\{containingFolder}\\{fileName}";

            int pathSize = outputPath.Count();
            if (pathSize <= 32)
                return outputPath;

            int diff = pathSize - 32;
            string newFileName = fileName.Remove(0, diff);

            return $"{Config.convertedRootFolder}\\{containingFolder}\\{newFileName}";
        }

        static string NameShortnernerMapper(string name)
        {
            return name.Replace("Standard", "Std")
            .Replace("Restore", "Rest")
            .Replace("Apprentice", "Appr")
            .Replace("Journeyman", "Jour")
            .Replace("Intelligence", "Int")
            .Replace("Personality", "Pers")
            .Replace("Endurance", "End")
            .Replace("Willpower", "Will")
            .Replace("Summon", "Summ")
            .Replace("Expert", "Expr")
            .Replace("Common", "Comm")
            .Replace("Strength", "Str")
            .Replace("HeavyArmor", "HArmor")
            .Replace("HandToHand", "HH");
        }

        static string CreatureNameShortnernerMapper(string name)
        {
            return name.Replace("Standard", "Std")
            .Replace("Creature", "Cr")
            .Replace("Grummite", "Grum")
            .Replace("Blackroot", "Br")
            .Replace("Test", "Ts")
            .Replace("Custom", "Cus")
            .Replace("Atronach", "Atr")
            .Replace("Dementia", "Dem");
        }


        internal static string CreatureIdFormater(string sourceEditorId)
        {
            int idSize = sourceEditorId.Count();
            if (idSize <= 24)
                return sourceEditorId;

            var result = CreatureNameShortnernerMapper(sourceEditorId);

            idSize = result.Count();
            if (result.Length <= 24)
                return result;

            int diff = idSize - 24;

            return result.Remove(0, diff);
        }


        internal static string EditorIdFormater(string sourceEditorId)
        {
            int idSize = sourceEditorId.Count();
            if (idSize <= 32)
                return sourceEditorId;

            var result = NameShortnernerMapper(sourceEditorId);

            idSize = result.Count();
            if (result.Length <= 32)
                return result;

            int diff = idSize - 32;

            return result.Remove(0, diff);
        }

        internal static string NameFormater(string recordDisplayName)
        {
            int idSize = recordDisplayName.Count();
            if (idSize <= 32)
                return recordDisplayName;

            int diff = idSize - 32;
            return recordDisplayName.Replace("\0", "").Remove(recordDisplayName.Length-diff-1,diff)+"\0";
        }

        internal static string CellNameFormatter(string recordDisplayName)
        {
            int idSize = recordDisplayName.Count();
            if (idSize <= 64)
                return recordDisplayName;

            int diff = idSize - 64;
            return recordDisplayName.Replace("\0", "").Remove(recordDisplayName.Length-1- diff, diff) + "\0";
        }

        internal static string SoundIdFormater(string sourceEditorId)
        {
            int idSize = sourceEditorId.Count();
            if (idSize <= 31)
                return $"s{sourceEditorId}";

            int diff = idSize - 31;
            string path =  $"s{sourceEditorId.Remove(0, diff)}";
            return path;
        }

        /// <summary>
        /// Some default mappings of oblivion objects to their morrowind counterparts
        /// </summary>
        /// <param name="formId">oblivion form id</param>
        /// <returns></returns>
        static string GetDefaultIdFromFormId(string formId)
        {
            string mwEditorId = string.Empty;
            Config.OblivionMorrowindRecordsMap.TryGetValue(formId, out mwEditorId);
            return mwEditorId;
        }
    }

    public class ConvertedRecordData
    {
        public string OriginFormId;
        public readonly string Type;
        public readonly string EditorId;
        public TES3Lib.Base.Record Record;

        public ConvertedRecordData(string originFormId, string type, string editorId, TES3Lib.Base.Record record)
        {
            OriginFormId = originFormId;
            Type = type;
            EditorId = editorId;
            Record = record;
        }
    }

    public class ConvertedCellReference
    {
        public string ParentCellFormId;
        public readonly string ReferenceFormId;
        public readonly string Type;
        public readonly string EditorId;
        public readonly TES3Lib.Records.REFR Reference;

        public ConvertedCellReference(string parentCellFormId, string referenceFormId, TES3Lib.Records.REFR reference)
        {
            ParentCellFormId = parentCellFormId;
            ReferenceFormId = referenceFormId;
            Reference = reference;
        }
    }
}
