using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.ARMO
{
    /// <summary>
    /// Flags
    /// </summary>
    public class BMDT : Subrecord
    {
        public int BodySlot { get; set; }

        public short GeneralFlags { get; set; }

        public byte Unused { get; set; }

        public BMDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            BodySlot = reader.ReadBytes<int>(base.Data);
            GeneralFlags = reader.ReadBytes<short>(base.Data);
            Unused = reader.ReadBytes<byte>(base.Data);
        }

        #region flag handlers
        public bool IsCalculateForAlllevels() => CheckIfByteSet(Flags, 0x01);
        public bool IsCalculateForEachItemInCount() => CheckIfByteSet(Flags, 0x02);
        #endregion

        //        wbStruct(BMDT, '', [
        //      wbInteger('Biped Flags', itU16, wbFlags([
        //        { 0x00000001} 'Head',
        //        {0x00000002} 'Hair',
        //        {0x00000004} 'Upper Body',
        //        {0x00000008} 'Lower Body',
        //        {0x00000010} 'Hand',
        //        {0x00000020} 'Foot',
        //        {0x00000040} 'Right Ring',
        //        {0x00000080} 'Left Ring',
        //        {0x00000100} 'Amulet',
        //        {0x00000200} 'Weapon',
        //        {0x00000400} 'Back Weapon',
        //        {0x00000800} 'Side Weapon',
        //        {0x00001000} 'Quiver',
        //        {0x00002000} 'Shield',
        //        {0x00004000} 'Torch',
        //        {0x00008000} 'Tail'
        //      ])),
        //      wbInteger('General Flags', itU8, wbFlags([
        //        { 0x0001} 'Hide Rings',
        //        {0x0002} 'Hide Amulets',
        //        {0x0004} '',
        //        {0x0008} '',
        //        {0x0010} '',
        //        {0x0020} '',
        //        {0x0040} 'Non-Playable',
        //        {0x0080} 'Heavy armor'
        //      ])),
        //      wbByteArray('Unused', 1)
        //], cpNormal, True),
    }
}
