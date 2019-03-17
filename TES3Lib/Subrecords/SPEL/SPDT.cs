using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;
using static Utility.Common;

namespace TES3Lib.Subrecords.SPEL
{
    /// <summary>
    /// Spell data
    /// </summary>
    public class SPDT : Subrecord
    {
        public Spell Type { get; set; }

        public int SpellCost { get; set; }

        /// <summary>
        /// 0x0001 = AutoCalc
		///	0x0002 = PC Start
		///	0x0004 = Always Succeeds
        /// </summary>
        public int Flags { get; set; }

        public SPDT()
        {

        }

        public SPDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Type = (Spell)reader.ReadBytes<int>(base.Data);
        }

        #region flag handlers
        public bool IsAutoCalc() => CheckIfByteSet(Flags, 0x0001);
        public bool IsPCStart() => CheckIfByteSet(Flags, 0x0002);
        public bool IsAlwaysSucceed() => CheckIfByteSet(Flags, 0x0004);

        public void CheckAutoCalc() => Flags = SetByte(Flags, 0x0001);
        public void CheckPCStart() => Flags = SetByte(Flags, 0x0002);
        public void CheckAlwaysSUcceed() => Flags = SetByte(Flags, 0x0004);

        public void UnCheckAutoCalc() => Flags = UnsetByte(Flags, 0x0001);
        public void UnCheckPCStart() => Flags = UnsetByte(Flags, 0x0002);
        public void UnCheckAlwaysSUcceed() => Flags = UnsetByte(Flags, 0x0004);
        #endregion
    }
}
