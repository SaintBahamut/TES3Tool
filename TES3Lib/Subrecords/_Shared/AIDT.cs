using TES3Lib.Base;
using Utility;

namespace TES3Lib.Subrecords.Shared
{
    /// <summary>
    /// NPC AI data subrecord
    /// </summary>
    public class AIDT : Subrecord
    {
        public byte Hello { get; set; }

        public byte Unknown1 { get; set; }

        public byte Fight { get; set; }

        public byte Flee { get; set; }

        public byte Alarm { get; set; }

        public byte Unknown2 { get; set; }

        public byte Unknown3 { get; set; }

        public byte Unknown4 { get; set; }

        /// <summary>
        /// NPC Service flags
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
        public int Flags { get; set; }

        public AIDT()
        {

        }

        public AIDT(byte[] rawData) : base(rawData)
        {
            var reader = new ByteReader();
            Hello = reader.ReadBytes<byte>(base.Data);
            Unknown1 = reader.ReadBytes<byte>(base.Data);
            Fight = reader.ReadBytes<byte>(base.Data);
            Flee = reader.ReadBytes<byte>(base.Data);
            Alarm = reader.ReadBytes<byte>(base.Data);
            Unknown2 = reader.ReadBytes<byte>(base.Data);
            Unknown3 = reader.ReadBytes<byte>(base.Data);
            Unknown4 = reader.ReadBytes<byte>(base.Data);
            Flags = reader.ReadBytes<int>(base.Data);
        }

        #region flags handling
        //flag checkers
        public bool IsDealingWeapons() => 0 != (Flags & 0x00001);
        public bool IsDealingArmor() => 0 != (Flags & 0x00002);
        public bool IsDealingClothing() => 0 != (Flags & 0x00004);
        public bool IsDealingBooks() => 0 != (Flags & 0x00008);
        public bool IsDealingIngredients() => 0 != (Flags & 0x00010);
        public bool IsDealingPicks() => 0 != (Flags & 0x00020);
        public bool IsDealingProbes() => 0 != (Flags & 0x00040);
        public bool IsDealingLights() => 0 != (Flags & 0x00080);
        public bool IsDealingApparatus() => 0 != (Flags & 0x00100);
        public bool IsDealingRepair() => 0 != (Flags & 0x00200);
        public bool IsDealingMisc() => 0 != (Flags & 0x00400);
        public bool IsDealingSpells() => 0 != (Flags & 0x00800);
        public bool IsDealingMagicItems() => 0 != (Flags & 0x01000);
        public bool IsDealingPotions() => 0 != (Flags & 0x02000);
        public bool IsTrainer() => 0 != (Flags & 0x04000);
        public bool IsSpellmaker() => 0 != (Flags & 0x08000);
        public bool IsEnchanter() => 0 != (Flags & 0x10000);
        public bool IsRepairer() => 0 != (Flags & 0x20000);

        //flag setters TODO: make idempotent
        public void SetDealingWeapons() => Flags = (Flags & 0x00001);
        public void SetDealingArmor() => Flags = (Flags & 0x00002);
        public void SetDealingClothing() => Flags = (Flags & 0x00004);
        public void SetDealingBooks() => Flags = (Flags & 0x00008);
        public void SetDealingIngredients() => Flags = (Flags & 0x00010);
        public void SetDealingPicks() => Flags = (Flags & 0x00020);
        public void SetDealingProbes() => Flags = (Flags & 0x00040);
        public void SetDealingLights() => Flags = (Flags & 0x00080);
        public void SetDealingApparatus() => Flags = (Flags & 0x00100);
        public void SetDealingRepair() => Flags = (Flags & 0x00200);
        public void SetDealingMisc() =>  Flags = (Flags & 0x00400);
        public void SetDealingSpells() => Flags = (Flags & 0x00800);
        public void SetDealingMagicItems() => Flags = (Flags & 0x01000);
        public void SetDealingPotions() => Flags = (Flags & 0x02000);
        public void SetTrainer() => Flags = (Flags & 0x04000);
        public void SetSpellmaker() => Flags = (Flags & 0x08000);
        public void SetEnchanter() => Flags = (Flags & 0x10000);
        public void SetRepairer() => Flags = (Flags & 0x20000);
        #endregion
    }
}