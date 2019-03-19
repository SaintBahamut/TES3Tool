using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.Shared
{
    /// <summary>
    /// Script effect name
    /// </summary>
    public class SCIT : Subrecord
    {
        public byte[] ScriptEffectData { get; set; }

        public SCIT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            ScriptEffectData = reader.ReadBytes<byte[]>(base.Data, base.Size);
        }
    }
}

//wbSCIT :=
//    wbRStructSK([0], 'Script effect', [
//      wbStructSK(SCIT, [0], 'Script effect data', [
//        wbFormIDCk('Script effect', [NULL, SCPT]),
//        wbInteger('Magic school', itU32, wbMagicSchoolEnum),
//        wbInteger('Visual effect name', itU32, wbChar4),
//        wbInteger('Flags', itU8, wbFlags(['Hostile'])),
//        wbByteArray('Unused', 3)
//      ], cpNormal, True, nil, 1),
//      wbFULLReq
//    ], []);
