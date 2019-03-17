using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.MGEF
{
    /// <summary>
    /// COunter magic effects
    /// </summary>
    public class ESCE : Subrecord
    {
        /// <summary>
        /// Array of 4 char effect names
        /// </summary>
        public string[] CounterEffects { get; set; }

        public ESCE(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            int effectCount = base.Size / 4;
            CounterEffects = new string[effectCount];
            for (int i = 0; i < effectCount; i++)
            {
                CounterEffects[i] = reader.ReadBytes<string>(base.Data, 4);
            }
        }
    }
}