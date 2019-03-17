using static Utility.Common;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.NPC_
{
    public class ACBS : Subrecord
    {
        /// <summary>
        /// 0x000001 	Female
        /// 0x000002 	Essential
        /// 0x000008 	Respawn
        /// 0x000010 	Auto-calc stats
        /// 0x000080 	PC Level Offset
        /// 0x000200 	No Low Level Processing
        /// 0x002000 	No Rumors
        /// 0x004000 	Summonable
        /// 0x008000 	No Persuasion
        /// 0x100000 	Can Corpse Check
        /// </summary>
        public int Flags { get; set; }

        public short BaseSpellPoints { get; set; }

        public short Fatigue { get; set; }

        public short Gold { get; set; }

        public short Level { get; set; }

        public short CalcMin { get; set; }

        public short CalcMax { get; set; }

        public ACBS(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flags = reader.ReadBytes<int>(this.Data);
            BaseSpellPoints = reader.ReadBytes<short>(this.Data);
            Fatigue = reader.ReadBytes<short>(this.Data);
            Gold = reader.ReadBytes<short>(this.Data);
            Level = reader.ReadBytes<short>(this.Data);
            CalcMin = reader.ReadBytes<short>(this.Data);
            CalcMax = reader.ReadBytes<short>(this.Data);
        }

        #region flag handlers
        public bool IsFemale() => CheckIfByteSet(Flags, 0x000001);
        public bool IsEssential() => CheckIfByteSet(Flags, 0x000002);
        public bool IsRespawn() => CheckIfByteSet(Flags, 0x000008);
        public bool IsAutoCalcStats() => CheckIfByteSet(Flags, 0x000010);
        public bool IsPCLevelOffseted() => CheckIfByteSet(Flags, 0x000080);
        public bool IsNoLowLevelProcessing() => CheckIfByteSet(Flags, 0x000200);
        public bool IsNoRumors() => CheckIfByteSet(Flags, 0x002000);
        public bool IsSummonable() => CheckIfByteSet(Flags, 0x004000);
        public bool IsNoPersuation() => CheckIfByteSet(Flags, 0x008000);
        public bool IsCanCorpseCheck() => CheckIfByteSet(Flags, 0x100000);
        #endregion
    }
}
