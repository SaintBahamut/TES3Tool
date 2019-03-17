using System;
using System.Linq;
using TES4Lib.Base;
using Utility;

namespace TES4Lib.Subrecords.MGEF
{
    public class DATA : Subrecord
    {
        /// <summary>
        /// Possible valuess (bits set)
        /// 0x00000001 = Hostile
        /// 0x00000002 = Recover
        /// 0x00000004 = Detrimental
        /// 0x00000008 = Magnitude %
        /// 0x00000010 = Self
        /// 0x00000020 = Touch
        /// 0x00000040 = Target
        /// 0x00000080 = No duration
        /// 0x00000100 = No magnitude
        /// 0x00000200 = No area
        /// 0x00000400 = FX persist
        /// 0x00000800 = Spellmaking
        /// 0x00001000 = Enchanting
        /// 0x00002000 = No Ingredient
        /// 0x00010000 = Use weapon
        /// 0x00020000 = Use armor
        /// 0x00040000 = Use creature
        /// 0x00080000 = Use skill
        /// 0x00100000 = Use attribute
        /// 0x02000000 = Spray projectile type(Ball if Spray, Bolt or Fog is not specified)
        /// 0x04000000 = Bolt projectile type
        /// 0x06000000 = Fog projectile type
        /// 0x08000000 = No hit effect
        /// </summary>
        public int Flags { get; set; }

        public float BaseCost { get; set; }

        public int Unknown { get; set; }

        public SpellSchool School { get; set; }

        public ResistanceType ResistanceType { get; set; }

        public int Unknown2 { get; set; }

        public string LightFormId { get; set; }

        public float ProjectileSpeed { get; set; }

        public string EffectShaderFormId { get; set; }

        public string CastingSoundFormId { get; set; }

        public string BoltSoundFormId { get; set; }

        public string HitSoundFormId { get; set; }

        public string AreaSoundFormId { get; set; }

        public float ConstantEffectEnchFactor { get; set; }

        public float ConstantEffectBarterFactor { get; set; }

        public DATA(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Flags = reader.ReadBytes<int>(base.Data);
            BaseCost = reader.ReadBytes<float>(base.Data);
            Unknown = reader.ReadBytes<int>(base.Data);
            School = (SpellSchool)reader.ReadBytes<int>(base.Data);
            ResistanceType = (ResistanceType)reader.ReadBytes<int>(base.Data);
            Unknown2 = reader.ReadBytes<int>(base.Data);         
            LightFormId = BitConverter.ToString(reader.ReadBytes<byte[]>(base.Data, FORMID_LENGTH).Reverse().ToArray()).Replace("-", "");
            ProjectileSpeed = reader.ReadBytes<float>(base.Data);
            EffectShaderFormId = BitConverter.ToString(reader.ReadBytes<byte[]>(base.Data, FORMID_LENGTH).Reverse().ToArray()).Replace("-", "");
            CastingSoundFormId = BitConverter.ToString(reader.ReadBytes<byte[]>(base.Data, FORMID_LENGTH).Reverse().ToArray()).Replace("-", "");
            BoltSoundFormId = BitConverter.ToString(reader.ReadBytes<byte[]>(base.Data, FORMID_LENGTH).Reverse().ToArray()).Replace("-", "");
            HitSoundFormId = BitConverter.ToString(reader.ReadBytes<byte[]>(base.Data, FORMID_LENGTH).Reverse().ToArray()).Replace("-", "");
            AreaSoundFormId = BitConverter.ToString(reader.ReadBytes<byte[]>(base.Data, FORMID_LENGTH).Reverse().ToArray()).Replace("-", "");
            //ConstantEffectEnchFactor = reader.ReadBytes<float>(base.Data);
            //ConstantEffectBarterFactor = reader.ReadBytes<float>(base.Data);
        }
    }
}