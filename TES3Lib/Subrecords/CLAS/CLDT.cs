using TES3Lib.Base;
using TES3Lib.Enums;
using Utility;

namespace TES3Lib.Subrecords.CLAS
{
    public class CLDT : Subrecord
    {
        public Attribute Attribute1 { get; set; }

        public Attribute Attribute2 { get; set; }

        public Specialization Specialization { get; set; }

        public Skill Minor1 { get; set; }

        public Skill Major1 { get; set; }

        public Skill Minor2 { get; set; }

        public Skill Major2 { get; set; }

        public Skill Minor3 { get; set; }

        public Skill Major3 { get; set; }

        public Skill Minor4 { get; set; }

        public Skill Major4 { get; set; }

        public Skill Minor5 { get; set; }

        public Skill Major5 { get; set; }

        /// <summary>
        /// Flags
		///	0x0001 = Playable
        /// </summary>
        public int IsPlayable { get; set; }

        /// <summary>
        /// NPC Service  autocalc flags
        /// 0x00001 = Weapon
		///	0x00002 = Armor
		///	0x00004 = Clothing
		///	0x00008 = Books
		///	0x00010 = Ingrediant
		///	0x00020 = Picks
		///	0x00040 = Probes
		///	0x00080 = Lights
		///	0x00100 = Apparatus
		///	0x00200 = Repair
		///	0x00400 = Misc
		///	0x00800 = Spells
		///	0x01000 = Magic Items
		///	0x02000 = Potions
		///	0x04000 = Training
		///	0x08000 = Spellmaking
		///	0x10000 = Enchanting
		///	0x20000 = Repair Item
        /// </summary>
        public int AutoCalcFlags { get; set; }

        public CLDT()
        {
        }

        public CLDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Attribute1 = (Attribute)reader.ReadBytes<int>(base.Data);
            Attribute2 = (Attribute)reader.ReadBytes<int>(base.Data);
            Minor1 = (Skill)reader.ReadBytes<int>(base.Data);
            Major1 = (Skill)reader.ReadBytes<int>(base.Data);
            Minor2 = (Skill)reader.ReadBytes<int>(base.Data);
            Major2 = (Skill)reader.ReadBytes<int>(base.Data);
            Minor3 = (Skill)reader.ReadBytes<int>(base.Data);
            Major3 = (Skill)reader.ReadBytes<int>(base.Data);
            Minor4 = (Skill)reader.ReadBytes<int>(base.Data);
            Major4 = (Skill)reader.ReadBytes<int>(base.Data);
            Minor5 = (Skill)reader.ReadBytes<int>(base.Data);
            Major5 = (Skill)reader.ReadBytes<int>(base.Data);
            IsPlayable = reader.ReadBytes<int>(base.Data);
            AutoCalcFlags = reader.ReadBytes<int>(base.Data);
        }

        #region flags handling
        //flag checkers
        public bool IsDealingWeapons() => 0 != (AutoCalcFlags & 0x00001);
        public bool IsDealingArmor() => 0 != (AutoCalcFlags & 0x00002);
        public bool IsDealingClothing() => 0 != (AutoCalcFlags & 0x00004);
        public bool IsDealingBooks() => 0 != (AutoCalcFlags & 0x00008);
        public bool IsDealingIngredients() => 0 != (AutoCalcFlags & 0x00010);
        public bool IsDealingPicks() => 0 != (AutoCalcFlags & 0x00020);
        public bool IsDealingProbes() => 0 != (AutoCalcFlags & 0x00040);
        public bool IsDealingLights() => 0 != (AutoCalcFlags & 0x00080);
        public bool IsDealingApparatus() => 0 != (AutoCalcFlags & 0x00100);
        public bool IsDealingRepair() => 0 != (AutoCalcFlags & 0x00200);
        public bool IsDealingMisc() => 0 != (AutoCalcFlags & 0x00400);
        public bool IsDealingSpells() => 0 != (AutoCalcFlags & 0x00800);
        public bool IsDealingMagicItems() => 0 != (AutoCalcFlags & 0x01000);
        public bool IsDealingPotions() => 0 != (AutoCalcFlags & 0x02000);
        public bool IsTrainer() => 0 != (AutoCalcFlags & 0x04000);
        public bool IsSpellmaker() => 0 != (AutoCalcFlags & 0x08000);
        public bool IsEnchanter() => 0 != (AutoCalcFlags & 0x10000);
        public bool IsRepairer() => 0 != (AutoCalcFlags & 0x20000);

        //flag setters TODO: make idempotent
        public void SetDealingWeapons() => AutoCalcFlags = (AutoCalcFlags & 0x00001);
        public void SetDealingArmor() => AutoCalcFlags = (AutoCalcFlags & 0x00002);
        public void SetDealingClothing() => AutoCalcFlags = (AutoCalcFlags & 0x00004);
        public void SetDealingBooks() => AutoCalcFlags = (AutoCalcFlags & 0x00008);
        public void SetDealingIngredients() => AutoCalcFlags = (AutoCalcFlags & 0x00010);
        public void SetDealingPicks() => AutoCalcFlags = (AutoCalcFlags & 0x00020);
        public void SetDealingProbes() => AutoCalcFlags = (AutoCalcFlags & 0x00040);
        public void SetDealingLights() => AutoCalcFlags = (AutoCalcFlags & 0x00080);
        public void SetDealingApparatus() => AutoCalcFlags = (AutoCalcFlags & 0x00100);
        public void SetDealingRepair() => AutoCalcFlags = (AutoCalcFlags & 0x00200);
        public void SetDealingMisc() => AutoCalcFlags = (AutoCalcFlags & 0x00400);
        public void SetDealingSpells() => AutoCalcFlags = (AutoCalcFlags & 0x00800);
        public void SetDealingMagicItems() => AutoCalcFlags = (AutoCalcFlags & 0x01000);
        public void SetDealingPotions() => AutoCalcFlags = (AutoCalcFlags & 0x02000);
        public void SetTrainer() => AutoCalcFlags = (AutoCalcFlags & 0x04000);
        public void SetSpellmaker() => AutoCalcFlags = (AutoCalcFlags & 0x08000);
        public void SetEnchanter() => AutoCalcFlags = (AutoCalcFlags & 0x10000);
        public void SetRepairer() => AutoCalcFlags = (AutoCalcFlags & 0x20000);
        #endregion
    }
}
