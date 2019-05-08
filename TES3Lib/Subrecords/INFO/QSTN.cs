using System.Diagnostics;
using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.INFO
{
    /// <summary>
    /// Marks INFO entry as Quest name (Journal DIAL type)
    /// </summary>
    [DebuggerDisplay("QuestName: {IsQuestName}")]
    public class QSTN : Subrecord
    {
        public bool IsQuestName { get; set; }

        public QSTN()
        {
            IsQuestName = false;
        }

        public QSTN(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            IsQuestName = reader.ReadBytes<bool>(base.Data);
        }
    }
}
