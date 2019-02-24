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
            Parallel.ForEach(ConvertedRecords, (record, state) =>
            {
                if (!string.IsNullOrEmpty(BaseId)) state.Break();
                var result = record.Value.FirstOrDefault(x => x.OriginFormId.Equals(formId));
                BaseId = !IsNull(result) ? result.EditorId : string.Empty;
            });


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
