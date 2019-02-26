using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TES3Tool.TES4RecordConverter.Records
{
    internal static class Helpers
    {
        internal static string GenerateSoundScript(string SoundEditorId)
        {
            string template = "begin Sound__PLACEHOLDER_\r\n\r\nif(CellChanged == 0)\r\n\tif(GetSoundPlaying \"_PLACEHOLDER_\" == 0 )\r\n\t\tPlayLoopSound3DVP \"_PLACEHOLDER_\", 1.0, 1.0\r\n\tendif\r\nendif\r\n\r\nend";
            return template.Replace("_PLACEHOLDER_", SoundEditorId);
        }

        internal static Dictionary<string, List<ConvertedRecordData>> ConvertedRecords = new Dictionary<string, List<ConvertedRecordData>>();

        internal static string GetBaseIdFromFormId(string formId)
        {
            string BaseId = string.Empty;

            var group = ConvertedRecords.AsParallel()
                .FirstOrDefault(x => x.Value.Exists(y => y.OriginFormId.Equals(formId)));
            var result = !IsNull(group.Key) ? group.Value.AsParallel().FirstOrDefault(x => x.OriginFormId.Equals(formId)) : null;
            BaseId = !IsNull(result) ?  result.EditorId : string.Empty;

            return BaseId;
        }

        internal static int GetTES4DeletedRecordFlag(int recordFlags)
        {
            return recordFlags & 0x00000020;
        }

        internal static int GetTES4CantWaitRecordFlag(int recordFlags)
        {
            return recordFlags & 0x080000;
        }

        internal static int GetTES4IgnoredRecordFlag(int recordFlags)
        {
            return recordFlags & 0x01000;
        }

        /// <summary>
        /// For TES4.Records.CELL.Flag
        /// </summary>
        /// <param name="recordFlags"></param>
        /// <returns></returns>
        internal static int GetTES4HasWaterCellFlag(int recordFlags)
        {
            return recordFlags & 0x02;
        }

        /// <summary>
        /// For TES4.Records.CELL.Flag
        /// </summary>
        /// <param name="recordFlags"></param>
        /// <returns></returns>
        internal static int GetTES4BehavesLikeExteriorCellFlag(int recordFlags)
        {
            return recordFlags & 0x80;
        }

        internal static bool IsNull(object tested) => tested == null ? true : false;

        internal static string ModelPathFormater(string sourcePath)
        {
            var pathSplit = sourcePath.Split('\\');

            for (int i = 0; i < pathSplit.Count()-1; i++)
            {
                pathSplit[i] = pathSplit[i].First().ToString();
            }

            var newPath = "SI\\" + string.Join("\\", pathSplit);

            if(newPath.Count() > 32)
            {
                int diff = newPath.Count() - 32;
                int pos = pathSplit.Count() - 1;
                pathSplit[pos] = pathSplit[pos].Remove(0, diff);
                newPath = "SI\\" + string.Join("\\", pathSplit);
            }

            return newPath;
        }

        internal static string PathFormater(string sourcePath, string containingFolder)
        {
            string fileName = sourcePath.Split('\\').Last();

            string outputPath = $"{TES3Tool.Config.convertedRootFolder}\\{containingFolder}\\{fileName}";

            int pathSize = outputPath.Count();
            if (pathSize <= 32)
                return outputPath;

            int diff = pathSize - 32;
            string newFileName = fileName.Remove(0, diff);

            return $"{TES3Tool.Config.convertedRootFolder}\\{containingFolder}\\{newFileName}";
        }

        internal static string EditorIdFormater(string sourceEditorId)
        {
            int idSize = sourceEditorId.Count();
            if (idSize <= 32)
                return sourceEditorId;

            int diff = idSize - 32;
            return sourceEditorId.Remove(0, diff);
        }

        internal static string NameFormater(string recordDisplayName)
        {
            int idSize = recordDisplayName.Count();
            if (idSize <= 32)
                return recordDisplayName;

            int diff = idSize - 32;
            return recordDisplayName.Replace("\0", "").Remove(recordDisplayName.Length-diff,diff)+"\0";
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

    }

    public class ConvertedRecordData
    {
        public readonly string OriginFormId;
        public readonly string Type;
        public readonly string EditorId;
        public readonly TES3Lib.Base.Record Record;

        public ConvertedRecordData(string originFormId, string type, string editorId, TES3Lib.Base.Record record)
        {
            OriginFormId = originFormId;
            Type = type;
            EditorId = editorId;
            Record = record;
        }
    }
}
