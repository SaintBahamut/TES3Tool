using System;
using System.Collections.Generic;
using TES4Lib.Enums;
using TES3Lib.Enums;

namespace TES3Tool.TES4RecordConverter.Records
{
    public static class TypeConverters
    {
        public static byte CalcThrust(TES3Lib.Enums.WeaponType type, short damage)
        {
            float penalty = 0.75f;
            switch (type)
            {
                case TES3Lib.Enums.WeaponType.BluntTwoClose:
                case TES3Lib.Enums.WeaponType.BluntOneHand:
                case TES3Lib.Enums.WeaponType.AxeOneHand:
                case TES3Lib.Enums.WeaponType.AxeTwoHand:
                    return (byte)(penalty * damage);
                case TES3Lib.Enums.WeaponType.MarksmanBow:
                    return 0;

                default:
                    return (byte)damage;
            }
        }

        public static TES3Lib.Enums.ArmorType CastArmorTypeToMW(HashSet<TES4Lib.Enums.BodySlot> bipedObjectFlags)
        {
            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.UpperBody)) return TES3Lib.Enums.ArmorType.Cuirass;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Foot)) return TES3Lib.Enums.ArmorType.Boots;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Hand)) return TES3Lib.Enums.ArmorType.LGauntlet;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Head) || bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Hair))
                return TES3Lib.Enums.ArmorType.Helmet;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.LowerBody)) return TES3Lib.Enums.ArmorType.Greaves;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Shield)) return TES3Lib.Enums.ArmorType.Shield;

            return TES3Lib.Enums.ArmorType.Cuirass;
        }

        public static HashSet<TES3Lib.Enums.Flags.NPCFlag> CastNPCFlagsToMW(HashSet<TES4Lib.Enums.Flags.NPCFlag> obFlag)
        {
            var mwFlags = new HashSet<TES3Lib.Enums.Flags.NPCFlag>();
            foreach (var flag in obFlag)
            {
                switch (flag)
                {
                    case TES4Lib.Enums.Flags.NPCFlag.Female:
                        mwFlags.Add(TES3Lib.Enums.Flags.NPCFlag.Female);
                        break;
                    case TES4Lib.Enums.Flags.NPCFlag.Essential:
                        mwFlags.Add(TES3Lib.Enums.Flags.NPCFlag.Essential);
                        break;
                    case TES4Lib.Enums.Flags.NPCFlag.Respawn:
                        mwFlags.Add(TES3Lib.Enums.Flags.NPCFlag.Respawn);
                        break;
                    case TES4Lib.Enums.Flags.NPCFlag.AutoCalcStats:
                        mwFlags.Add(TES3Lib.Enums.Flags.NPCFlag.AutoCalc);
                        break;
                    default:
                        break;
                }
            }

            return mwFlags;
        }

        public static TES3Lib.Enums.ClothingType CastClothingTypeToMW(HashSet<TES4Lib.Enums.BodySlot> bipedObjectFlags)
        {
            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Amulet))
                return TES3Lib.Enums.ClothingType.Amulet;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.RightRing) || bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.LeftRing))
                return TES3Lib.Enums.ClothingType.Ring;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.UpperBody))
                return TES3Lib.Enums.ClothingType.Skirt;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Foot))
                return TES3Lib.Enums.ClothingType.Shoes;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Hand))
                return TES3Lib.Enums.ClothingType.LeftGlove;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Head) || bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.Hair))
                return TES3Lib.Enums.ClothingType.Belt;

            if (bipedObjectFlags.Contains(TES4Lib.Enums.BodySlot.LowerBody))
                return TES3Lib.Enums.ClothingType.Pants;

            return TES3Lib.Enums.ClothingType.Robe;
        }

        public static TES3Lib.Enums.WeaponType CastWeaponTypeToMw(TES4Lib.Records.WEAP obWEAP)
        {
            if (obWEAP.DATA.Type.Equals(TES4Lib.Enums.WeaponType.BladeOneHand))//blade one hand
            {
                if (obWEAP.EDID.EditorId.ToLower().Contains("dagger") || obWEAP.FULL.DisplayName.ToLower().Contains("dagger"))
                    return TES3Lib.Enums.WeaponType.ShortBladeOneHand;

                return TES3Lib.Enums.WeaponType.LongBladeOneHand;
            }
            if (obWEAP.DATA.Type.Equals(TES4Lib.Enums.WeaponType.BladeTwoHand))//blade two hand
            {
                return TES3Lib.Enums.WeaponType.LongBladeTwoClose;
            }
            if (obWEAP.DATA.Type.Equals(TES4Lib.Enums.WeaponType.BluntOneHand))//blunt one hand
            {
                if (obWEAP.EDID.EditorId.ToLower().Contains("axe") || obWEAP.FULL.DisplayName.ToLower().Contains("axe"))
                    return TES3Lib.Enums.WeaponType.AxeOneHand;

                return TES3Lib.Enums.WeaponType.BluntOneHand;
            }
            if (obWEAP.DATA.Type.Equals(TES4Lib.Enums.WeaponType.BluntTwoHand))//blunt two hand
            {
                if (obWEAP.EDID.EditorId.ToLower().Contains("axe") || obWEAP.FULL.DisplayName.ToLower().Contains("axe"))
                    return TES3Lib.Enums.WeaponType.AxeTwoHand;

                return TES3Lib.Enums.WeaponType.BluntTwoClose;
            }
            if (obWEAP.DATA.Type.Equals(TES4Lib.Enums.WeaponType.Staff))//staff
            {
                return TES3Lib.Enums.WeaponType.BluntTwoWide;
            }
            else
            {
                return TES3Lib.Enums.WeaponType.MarksmanBow;
            }
        }

        public static TES3Lib.Enums.MagicEffect CastMagicEffectToMW(TES4Lib.Enums.MagicEffect magicEffect)
        {
            switch (magicEffect)
            {
                case TES4Lib.Enums.MagicEffect.BoundPriestDagger:
                    return TES3Lib.Enums.MagicEffect.BoundDagger;
                case TES4Lib.Enums.MagicEffect.SummonSewnFleshAtronach:
                    return TES3Lib.Enums.MagicEffect.SummonLeastBonewalker;
                case TES4Lib.Enums.MagicEffect.SummonStitchedFleshAtronach:
                    return TES3Lib.Enums.MagicEffect.SummonLeastBonewalker;
                case TES4Lib.Enums.MagicEffect.SummonTornFleshAtronach:
                    return TES3Lib.Enums.MagicEffect.SummonGreaterBonewalker;
                case TES4Lib.Enums.MagicEffect.SummonMangledFleshAtronach:
                    return TES3Lib.Enums.MagicEffect.SummonGreaterBonewalker;
                case TES4Lib.Enums.MagicEffect.SummonHunger:
                    return TES3Lib.Enums.MagicEffect.SummonHunger;
                case TES4Lib.Enums.MagicEffect.BoundOrderWeapon6:
                    return TES3Lib.Enums.MagicEffect.BoundLongsword;
                case TES4Lib.Enums.MagicEffect.BoundOrderWeapon5:
                    return TES3Lib.Enums.MagicEffect.BoundLongsword;
                case TES4Lib.Enums.MagicEffect.BoundOrderWeapon4:
                    return TES3Lib.Enums.MagicEffect.BoundLongsword;
                case TES4Lib.Enums.MagicEffect.BoundOrderWeapon3:
                    return TES3Lib.Enums.MagicEffect.BoundLongsword;
                case TES4Lib.Enums.MagicEffect.BoundOrderWeapon2:
                    return TES3Lib.Enums.MagicEffect.BoundLongsword;
                case TES4Lib.Enums.MagicEffect.BoundOrderWeapon1:
                    return TES3Lib.Enums.MagicEffect.BoundLongsword;
                case TES4Lib.Enums.MagicEffect.SummonStaffofSheogorath:
                    return TES3Lib.Enums.MagicEffect.BoundSpear;
                case TES4Lib.Enums.MagicEffect.WabbaSummon:
                    return TES3Lib.Enums.MagicEffect.BoundMace;
                case TES4Lib.Enums.MagicEffect.SummonGoldenSaint:
                    return TES3Lib.Enums.MagicEffect.SummonGoldenSaint;
                case TES4Lib.Enums.MagicEffect.SummonVoraciousHunger:
                    return TES3Lib.Enums.MagicEffect.SummonHunger;
                case TES4Lib.Enums.MagicEffect.SummonRavenousHunger:
                    return TES3Lib.Enums.MagicEffect.SummonHunger;
                case TES4Lib.Enums.MagicEffect.SummonDarkSeducer:
                    return TES3Lib.Enums.MagicEffect.SummonGoldenSaint;
                case TES4Lib.Enums.MagicEffect.SummonGluttonousHunger:
                    return TES3Lib.Enums.MagicEffect.SummonHunger;
                case TES4Lib.Enums.MagicEffect.SummonRepleteShambles:
                    return TES3Lib.Enums.MagicEffect.SummonSkeleton;
                case TES4Lib.Enums.MagicEffect.SummonShambles:
                    return TES3Lib.Enums.MagicEffect.SummonSkeleton;
                case TES4Lib.Enums.MagicEffect.SummonDecrepitShambles:
                    return TES3Lib.Enums.MagicEffect.SummonSkeleton;
                case TES4Lib.Enums.MagicEffect.DONOTUSEDarkness:
                    return TES3Lib.Enums.MagicEffect.Blind;
                case TES4Lib.Enums.MagicEffect.Rally:
                    return TES3Lib.Enums.MagicEffect.RallyHumanoid;
                case TES4Lib.Enums.MagicEffect.Frenzy:
                    return TES3Lib.Enums.MagicEffect.FrenzyHumanoid;
                case TES4Lib.Enums.MagicEffect.Charm:
                    return TES3Lib.Enums.MagicEffect.Charm;
                case TES4Lib.Enums.MagicEffect.Calm:
                    return TES3Lib.Enums.MagicEffect.CalmCreature;
                case TES4Lib.Enums.MagicEffect.Demoralize:
                    return TES3Lib.Enums.MagicEffect.DemoralizeHumanoid;
                case TES4Lib.Enums.MagicEffect.SummonMythicDawnHelm:
                    return TES3Lib.Enums.MagicEffect.BoundHelm;
                case TES4Lib.Enums.MagicEffect.PoisonInfo:
                    return TES3Lib.Enums.MagicEffect.Poison;
                case TES4Lib.Enums.MagicEffect.SummonBear:
                    return TES3Lib.Enums.MagicEffect.SummonBear;
                case TES4Lib.Enums.MagicEffect.SummonFleshAtronach:
                    return TES3Lib.Enums.MagicEffect.SummonGreaterBonewalker;
                case TES4Lib.Enums.MagicEffect.SummonSpiderling:
                    return TES3Lib.Enums.MagicEffect.SummonScamp;
                case TES4Lib.Enums.MagicEffect.SummonAncestorGuardian:
                    return TES3Lib.Enums.MagicEffect.SummonGhost;
                case TES4Lib.Enums.MagicEffect.SummonRufiosGhost:
                    return TES3Lib.Enums.MagicEffect.SummonGhost;
                case TES4Lib.Enums.MagicEffect.Reanimate:
                    return TES3Lib.Enums.MagicEffect.SummonGhost;
                case TES4Lib.Enums.MagicEffect.SummonXivilai:
                    return TES3Lib.Enums.MagicEffect.SummonDremora;
                case TES4Lib.Enums.MagicEffect.SummonGloomWraith:
                    return TES3Lib.Enums.MagicEffect.SummonBonelord;
                case TES4Lib.Enums.MagicEffect.SummonSpiderDaedra:
                    return TES3Lib.Enums.MagicEffect.SummonTwilight;
                case TES4Lib.Enums.MagicEffect.SummonSkeletonHero:
                    return TES3Lib.Enums.MagicEffect.SummonSkeleton;
                case TES4Lib.Enums.MagicEffect.SummonSkeletonChampion:
                    return TES3Lib.Enums.MagicEffect.SummonSkeleton;
                case TES4Lib.Enums.MagicEffect.SummonSkeletonGuardian:
                    return TES3Lib.Enums.MagicEffect.SummonSkeleton;
                case TES4Lib.Enums.MagicEffect.SummonHeadlessZombie:
                    return TES3Lib.Enums.MagicEffect.SummonLeastBonewalker;
                case TES4Lib.Enums.MagicEffect.SummonDremoraLord:
                    return TES3Lib.Enums.MagicEffect.SummonDremora;
                case TES4Lib.Enums.MagicEffect.SummonMythicDawnArmor:
                    return TES3Lib.Enums.MagicEffect.BoundCuirass;
                case TES4Lib.Enums.MagicEffect.AbsorbHealth:
                    return TES3Lib.Enums.MagicEffect.AbsorbHealth;
                case TES4Lib.Enums.MagicEffect.AbsorbSkill:
                    return TES3Lib.Enums.MagicEffect.AbsorbSkill;
                case TES4Lib.Enums.MagicEffect.AbsorbMagicka:
                    return TES3Lib.Enums.MagicEffect.AbsorbMagicka;
                case TES4Lib.Enums.MagicEffect.AbsorbAttribute:
                    return TES3Lib.Enums.MagicEffect.AbsorbAttribute;
                case TES4Lib.Enums.MagicEffect.BoundBoots:
                    return TES3Lib.Enums.MagicEffect.BoundBoots;
                case TES4Lib.Enums.MagicEffect.AbsorbFatigue:
                    return TES3Lib.Enums.MagicEffect.AbsorbFatigue;
                case TES4Lib.Enums.MagicEffect.BoundCuirass:
                    return TES3Lib.Enums.MagicEffect.BoundCuirass;
                case TES4Lib.Enums.MagicEffect.BoundGauntlets:
                    return TES3Lib.Enums.MagicEffect.BoundGloves;
                case TES4Lib.Enums.MagicEffect.BoundGreaves:
                    return TES3Lib.Enums.MagicEffect.BoundSpear;
                case TES4Lib.Enums.MagicEffect.BoundHelmet:
                    return TES3Lib.Enums.MagicEffect.BoundHelm;
                case TES4Lib.Enums.MagicEffect.BoundShield:
                    return TES3Lib.Enums.MagicEffect.BoundShield;
                case TES4Lib.Enums.MagicEffect.Burden:
                    return TES3Lib.Enums.MagicEffect.Burden;
                case TES4Lib.Enums.MagicEffect.BoundAxe:
                    return TES3Lib.Enums.MagicEffect.BoundBattleAxe;
                case TES4Lib.Enums.MagicEffect.BoundBow:
                    return TES3Lib.Enums.MagicEffect.BoundLongbow;
                case TES4Lib.Enums.MagicEffect.BoundDagger:
                    return TES3Lib.Enums.MagicEffect.BoundDagger;
                case TES4Lib.Enums.MagicEffect.BoundMace:
                    return TES3Lib.Enums.MagicEffect.BoundMace;
                case TES4Lib.Enums.MagicEffect.BoundSword:
                    return TES3Lib.Enums.MagicEffect.BoundLongsword;
                case TES4Lib.Enums.MagicEffect.Chameleon:
                    return TES3Lib.Enums.MagicEffect.Chameleon;
                case TES4Lib.Enums.MagicEffect.CommandCreature:
                    return TES3Lib.Enums.MagicEffect.CommandCreature;
                case TES4Lib.Enums.MagicEffect.CommandHumanoid:
                    return TES3Lib.Enums.MagicEffect.CommandHumanoid;
                case TES4Lib.Enums.MagicEffect.CureDisease:
                    return TES3Lib.Enums.MagicEffect.CureCommonDisease;
                case TES4Lib.Enums.MagicEffect.CureParalysis:
                    return TES3Lib.Enums.MagicEffect.CureParalyzation;
                case TES4Lib.Enums.MagicEffect.CurePoison:
                    return TES3Lib.Enums.MagicEffect.CurePoison;
                case TES4Lib.Enums.MagicEffect.DamageAttribute:
                    return TES3Lib.Enums.MagicEffect.DamageAttribute;
                case TES4Lib.Enums.MagicEffect.DamageFatigue:
                    return TES3Lib.Enums.MagicEffect.DamageFatigue;
                case TES4Lib.Enums.MagicEffect.DamageHealth:
                    return TES3Lib.Enums.MagicEffect.DamageHealth;
                case TES4Lib.Enums.MagicEffect.DamageMagicka:
                    return TES3Lib.Enums.MagicEffect.DamageMagicka;
                case TES4Lib.Enums.MagicEffect.DisintegrateArmor:
                    return TES3Lib.Enums.MagicEffect.DisintegrateArmor;
                case TES4Lib.Enums.MagicEffect.DisintegrateWeapon:
                    return TES3Lib.Enums.MagicEffect.DisintegrateWeapon;
                case TES4Lib.Enums.MagicEffect.DrainAttribute:
                    return TES3Lib.Enums.MagicEffect.DrainAttribute;
                case TES4Lib.Enums.MagicEffect.DrainFatigue:
                    return TES3Lib.Enums.MagicEffect.DrainFatigue;
                case TES4Lib.Enums.MagicEffect.DrainHealth:
                    return TES3Lib.Enums.MagicEffect.DrainHealth;
                case TES4Lib.Enums.MagicEffect.DrainSkill:
                    return TES3Lib.Enums.MagicEffect.DrainSkill;
                case TES4Lib.Enums.MagicEffect.DrainMagicka:
                    return TES3Lib.Enums.MagicEffect.DrainMagicka;
                case TES4Lib.Enums.MagicEffect.Dispel:
                    return TES3Lib.Enums.MagicEffect.Dispel;
                case TES4Lib.Enums.MagicEffect.DetectLife:
                    return TES3Lib.Enums.MagicEffect.DetectAnimal;
                case TES4Lib.Enums.MagicEffect.FireDamage:
                    return TES3Lib.Enums.MagicEffect.FireDamage;
                case TES4Lib.Enums.MagicEffect.FireShield:
                    return TES3Lib.Enums.MagicEffect.FireShield;
                case TES4Lib.Enums.MagicEffect.FortifyAttribute:
                    return TES3Lib.Enums.MagicEffect.FortifyAttribute;
                case TES4Lib.Enums.MagicEffect.FortifyFatigue:
                    return TES3Lib.Enums.MagicEffect.FortifyFatigue;
                case TES4Lib.Enums.MagicEffect.FortifyHealth:
                    return TES3Lib.Enums.MagicEffect.FortifyHealth;
                case TES4Lib.Enums.MagicEffect.FortifyMagickaMultiplier:
                    return TES3Lib.Enums.MagicEffect.FortifyMagickaMultiplier;
                case TES4Lib.Enums.MagicEffect.FortifySkill:
                    return TES3Lib.Enums.MagicEffect.FortifySkill;
                case TES4Lib.Enums.MagicEffect.FortifyMagicka:
                    return TES3Lib.Enums.MagicEffect.FortifyMagicka;
                case TES4Lib.Enums.MagicEffect.FrostDamage:
                    return TES3Lib.Enums.MagicEffect.FortifyAttackBonus;
                case TES4Lib.Enums.MagicEffect.FrostShield:
                    return TES3Lib.Enums.MagicEffect.FrostShield;
                case TES4Lib.Enums.MagicEffect.Feather:
                    return TES3Lib.Enums.MagicEffect.Feather;
                case TES4Lib.Enums.MagicEffect.Invisibility:
                    return TES3Lib.Enums.MagicEffect.Invisibility;
                case TES4Lib.Enums.MagicEffect.Light:
                    return TES3Lib.Enums.MagicEffect.Light;
                case TES4Lib.Enums.MagicEffect.ShockShield:
                    return TES3Lib.Enums.MagicEffect.LightningShield;
                case TES4Lib.Enums.MagicEffect.DONOTUSELock:
                    return TES3Lib.Enums.MagicEffect.Lock;
                case TES4Lib.Enums.MagicEffect.NightEye:
                    return TES3Lib.Enums.MagicEffect.NightEye;
                case TES4Lib.Enums.MagicEffect.Open:
                    return TES3Lib.Enums.MagicEffect.Open;
                case TES4Lib.Enums.MagicEffect.Paralyze:
                    return TES3Lib.Enums.MagicEffect.Paralyze;
                case TES4Lib.Enums.MagicEffect.RestoreAttribute:
                    return TES3Lib.Enums.MagicEffect.RestoreAttribute;
                case TES4Lib.Enums.MagicEffect.ReflectDamage:
                    return TES3Lib.Enums.MagicEffect.Reflect;
                case TES4Lib.Enums.MagicEffect.RestoreFatigue:
                    return TES3Lib.Enums.MagicEffect.RestoreFatigue;
                case TES4Lib.Enums.MagicEffect.RestoreHealth:
                    return TES3Lib.Enums.MagicEffect.RestoreHealth;
                case TES4Lib.Enums.MagicEffect.RestoreMagicka:
                    return TES3Lib.Enums.MagicEffect.RestoreMagicka;
                case TES4Lib.Enums.MagicEffect.ReflectSpell:
                    return TES3Lib.Enums.MagicEffect.Reflect;
                case TES4Lib.Enums.MagicEffect.ResistDisease:
                    return TES3Lib.Enums.MagicEffect.ResistCommonDisease;
                case TES4Lib.Enums.MagicEffect.ResistFire:
                    return TES3Lib.Enums.MagicEffect.ResistFire;
                case TES4Lib.Enums.MagicEffect.ResistFrost:
                    return TES3Lib.Enums.MagicEffect.ResistFrost;
                case TES4Lib.Enums.MagicEffect.ResistMagic:
                    return TES3Lib.Enums.MagicEffect.ResistMagicka;
                case TES4Lib.Enums.MagicEffect.ResistNormalWeapons:
                    return TES3Lib.Enums.MagicEffect.ResistNormalWeapons;
                case TES4Lib.Enums.MagicEffect.ResistParalysis:
                    return TES3Lib.Enums.MagicEffect.ResistParalysis;
                case TES4Lib.Enums.MagicEffect.ResistPoison:
                    return TES3Lib.Enums.MagicEffect.ResistPoison;
                case TES4Lib.Enums.MagicEffect.ResistShock:
                    return TES3Lib.Enums.MagicEffect.ResistShock;
                case TES4Lib.Enums.MagicEffect.SpellAbsorption:
                    return TES3Lib.Enums.MagicEffect.SpellAbsorption;
                case TES4Lib.Enums.MagicEffect.ScriptEffect:
                    return TES3Lib.Enums.MagicEffect.None;
                case TES4Lib.Enums.MagicEffect.ShockDamage:
                    return TES3Lib.Enums.MagicEffect.ShockDamage;
                case TES4Lib.Enums.MagicEffect.Shield:
                    return TES3Lib.Enums.MagicEffect.Shield;
                case TES4Lib.Enums.MagicEffect.Silence:
                    return TES3Lib.Enums.MagicEffect.Silence;
                case TES4Lib.Enums.MagicEffect.StuntedMagicka:
                    return TES3Lib.Enums.MagicEffect.StuntedMagicka;
                case TES4Lib.Enums.MagicEffect.SoulTrap:
                    return TES3Lib.Enums.MagicEffect.SoulTrap;
                case TES4Lib.Enums.MagicEffect.SunDamage:
                    return TES3Lib.Enums.MagicEffect.SunDamage;
                case TES4Lib.Enums.MagicEffect.Telekinesis:
                    return TES3Lib.Enums.MagicEffect.Telekinesis;
                case TES4Lib.Enums.MagicEffect.TurnUndead:
                    return TES3Lib.Enums.MagicEffect.TurnUndead;
                case TES4Lib.Enums.MagicEffect.Vampirism:
                    return TES3Lib.Enums.MagicEffect.Vampirism;
                case TES4Lib.Enums.MagicEffect.WaterBreathing:
                    return TES3Lib.Enums.MagicEffect.WaterBreathing;
                case TES4Lib.Enums.MagicEffect.WaterWalking:
                    return TES3Lib.Enums.MagicEffect.WaterWalking;
                case TES4Lib.Enums.MagicEffect.WeaknesstoDisease:
                    return TES3Lib.Enums.MagicEffect.WeaknessToCommonDisease;
                case TES4Lib.Enums.MagicEffect.WeaknesstoFire:
                    return TES3Lib.Enums.MagicEffect.WeaknessToFire;
                case TES4Lib.Enums.MagicEffect.WeaknesstoFrost:
                    return TES3Lib.Enums.MagicEffect.WeaknessToFrost;
                case TES4Lib.Enums.MagicEffect.WeaknesstoMagic:
                    return TES3Lib.Enums.MagicEffect.WeaknessToMagicka;
                case TES4Lib.Enums.MagicEffect.WeaknesstoNormalWeapons:
                    return TES3Lib.Enums.MagicEffect.WeaknessToNormalWeapons;
                case TES4Lib.Enums.MagicEffect.WeaknesstoPoison:
                    return TES3Lib.Enums.MagicEffect.WeaknessToPoison;
                case TES4Lib.Enums.MagicEffect.WeaknesstoShock:
                    return TES3Lib.Enums.MagicEffect.WeaknessToShock;
                case TES4Lib.Enums.MagicEffect.SummonClannfear:
                    return TES3Lib.Enums.MagicEffect.SummonClannfear;
                case TES4Lib.Enums.MagicEffect.SummonDaedroth:
                    return TES3Lib.Enums.MagicEffect.SummonDaedroth;
                case TES4Lib.Enums.MagicEffect.SummonDremora:
                    return TES3Lib.Enums.MagicEffect.SummonDremora;
                case TES4Lib.Enums.MagicEffect.SummonFlameAtronach:
                    return TES3Lib.Enums.MagicEffect.SummonFlameAtronach;
                case TES4Lib.Enums.MagicEffect.SummonFrostAtronach:
                    return TES3Lib.Enums.MagicEffect.SummonFrostAtronach;
                case TES4Lib.Enums.MagicEffect.SummonGhost:
                    return TES3Lib.Enums.MagicEffect.SummonGhost;
                case TES4Lib.Enums.MagicEffect.SummonLich:
                    return TES3Lib.Enums.MagicEffect.SummonBonelord;
                case TES4Lib.Enums.MagicEffect.SummonScamp:
                    return TES3Lib.Enums.MagicEffect.SummonScamp;
                case TES4Lib.Enums.MagicEffect.SummonSkeleton:
                    return TES3Lib.Enums.MagicEffect.SummonSkeleton;
                case TES4Lib.Enums.MagicEffect.SummonStormAtronach:
                    return TES3Lib.Enums.MagicEffect.SummonStormAtronach;
                case TES4Lib.Enums.MagicEffect.SummonFadedWraith:
                    return TES3Lib.Enums.MagicEffect.SummonGhost;
                case TES4Lib.Enums.MagicEffect.SummonZombie:
                    return TES3Lib.Enums.MagicEffect.SummonLeastBonewalker;
                default:
                    return TES3Lib.Enums.MagicEffect.None;
            }
        }

        public static TES3Lib.Enums.Attribute CastActorValueToAttributeEffectMW(TES4Lib.Enums.ActorValue actorValue, TES3Lib.Enums.MagicEffect magicEffect)
        {
            bool isAttributeEffect =
                magicEffect.Equals(TES3Lib.Enums.MagicEffect.AbsorbAttribute) ||
                magicEffect.Equals(TES3Lib.Enums.MagicEffect.DamageAttribute) ||
                magicEffect.Equals(TES3Lib.Enums.MagicEffect.DrainAttribute) ||
                magicEffect.Equals(TES3Lib.Enums.MagicEffect.FortifyAttribute) ||
                magicEffect.Equals(TES3Lib.Enums.MagicEffect.RestoreAttribute);
            if (!isAttributeEffect) return TES3Lib.Enums.Attribute.Unused;

            return (TES3Lib.Enums.Attribute)Enum.Parse(typeof(TES3Lib.Enums.Attribute), actorValue.ToString());
        }

        public static TES3Lib.Enums.Skill CastActorValueToSkillEffectMW(TES4Lib.Enums.ActorValue actorValue, TES3Lib.Enums.MagicEffect magicEffect)
        {
            bool isSkillEffect =
               magicEffect.Equals(TES3Lib.Enums.MagicEffect.AbsorbSkill) ||
               magicEffect.Equals(TES3Lib.Enums.MagicEffect.DamageSkill) ||
               magicEffect.Equals(TES3Lib.Enums.MagicEffect.DrainSkill) ||
               magicEffect.Equals(TES3Lib.Enums.MagicEffect.FortifySkill) ||
               magicEffect.Equals(TES3Lib.Enums.MagicEffect.RestoreSkill);
            if (!isSkillEffect) return TES3Lib.Enums.Skill.Unused;

            var rnd = new Random(DateTime.Now.Millisecond).Next(0, 2);
            switch (actorValue)
            {
                case TES4Lib.Enums.ActorValue.Armorer:
                    return TES3Lib.Enums.Skill.Armorer;
                case TES4Lib.Enums.ActorValue.Athletics:
                    return TES3Lib.Enums.Skill.Athletics;
                case TES4Lib.Enums.ActorValue.Blade:
                    return rnd.Equals(0) ? TES3Lib.Enums.Skill.ShortBlade : TES3Lib.Enums.Skill.LongBlade;
                case TES4Lib.Enums.ActorValue.Block:
                    return TES3Lib.Enums.Skill.Block;
                case TES4Lib.Enums.ActorValue.Blunt:
                    return rnd.Equals(0) ? TES3Lib.Enums.Skill.BluntWeapon : TES3Lib.Enums.Skill.Axe;
                case TES4Lib.Enums.ActorValue.HandToHand:
                    return TES3Lib.Enums.Skill.HandToHand;
                case TES4Lib.Enums.ActorValue.HeavyArmor:
                    return rnd.Equals(0) ? TES3Lib.Enums.Skill.HeavyArmor : TES3Lib.Enums.Skill.MediumArmor;
                case TES4Lib.Enums.ActorValue.Alchemy:
                    return TES3Lib.Enums.Skill.Alchemy;
                case TES4Lib.Enums.ActorValue.Alteration:
                    return TES3Lib.Enums.Skill.Alteration;
                case TES4Lib.Enums.ActorValue.Conjuration:
                    return TES3Lib.Enums.Skill.Conjuration;
                case TES4Lib.Enums.ActorValue.Destruction:
                    return TES3Lib.Enums.Skill.Destruction;
                case TES4Lib.Enums.ActorValue.Illusion:
                    return TES3Lib.Enums.Skill.Illusion;
                case TES4Lib.Enums.ActorValue.Mysticism:
                    return TES3Lib.Enums.Skill.Mysticism;
                case TES4Lib.Enums.ActorValue.Restoration:
                    return TES3Lib.Enums.Skill.Restoration;
                case TES4Lib.Enums.ActorValue.Acrobatics:
                    return TES3Lib.Enums.Skill.Acrobatics;
                case TES4Lib.Enums.ActorValue.LightArmor:
                    return rnd.Equals(0) ? TES3Lib.Enums.Skill.LightArmor : TES3Lib.Enums.Skill.MediumArmor;
                case TES4Lib.Enums.ActorValue.Marksman:
                    return TES3Lib.Enums.Skill.Marksman;
                case TES4Lib.Enums.ActorValue.Mercantile:
                    return TES3Lib.Enums.Skill.Mercantile;
                case TES4Lib.Enums.ActorValue.Security:
                    return TES3Lib.Enums.Skill.Security;
                case TES4Lib.Enums.ActorValue.Sneak:
                    return TES3Lib.Enums.Skill.Sneak;
                case TES4Lib.Enums.ActorValue.Speechcraft:
                    return TES3Lib.Enums.Skill.Speechcraft;
                default:
                    return TES3Lib.Enums.Skill.Unused;
            }
        }

        public static TES3Lib.Enums.Skill CastActorValueToSkillMW(TES4Lib.Enums.ActorValue actorValue)
        {
            switch (actorValue)
            {
                case ActorValue.Armorer:
                    return TES3Lib.Enums.Skill.Armorer;
                case ActorValue.Athletics:
                    return TES3Lib.Enums.Skill.Athletics;
                case ActorValue.Blade:
                    return TES3Lib.Enums.Skill.LongBlade;
                case ActorValue.Block:
                    return TES3Lib.Enums.Skill.Block;
                case ActorValue.Blunt:
                    return TES3Lib.Enums.Skill.BluntWeapon;
                case ActorValue.HandToHand:
                    return TES3Lib.Enums.Skill.HandToHand;
                case ActorValue.HeavyArmor:
                    return TES3Lib.Enums.Skill.HeavyArmor;
                case ActorValue.Alchemy:
                    return TES3Lib.Enums.Skill.Alchemy;
                case ActorValue.Alteration:
                    return TES3Lib.Enums.Skill.Alteration;
                case ActorValue.Conjuration:
                    return TES3Lib.Enums.Skill.Conjuration;
                case ActorValue.Destruction:
                    return TES3Lib.Enums.Skill.Destruction;
                case ActorValue.Illusion:
                    return TES3Lib.Enums.Skill.Illusion;
                case ActorValue.Mysticism:
                    return TES3Lib.Enums.Skill.Mysticism;
                case ActorValue.Restoration:
                    return TES3Lib.Enums.Skill.Restoration;
                case ActorValue.Acrobatics:
                    return TES3Lib.Enums.Skill.Acrobatics;
                case ActorValue.LightArmor:
                    return TES3Lib.Enums.Skill.LightArmor;
                case ActorValue.Marksman:
                    return TES3Lib.Enums.Skill.Marksman;
                case ActorValue.Mercantile:
                    return TES3Lib.Enums.Skill.Mercantile;
                case ActorValue.Security:
                    return TES3Lib.Enums.Skill.Security;
                case ActorValue.Sneak:
                    return TES3Lib.Enums.Skill.Sneak;
                case ActorValue.Speechcraft:
                    return TES3Lib.Enums.Skill.Speechcraft;
                default:
                    return TES3Lib.Enums.Skill.Unused;
            }
        }

        public static TES3Lib.Enums.Attribute CastActorValueToAttributeMW(TES4Lib.Enums.ActorValue actorValue)
        {
            switch (actorValue)
            {
                case ActorValue.Strength:
                    return TES3Lib.Enums.Attribute.Strength;
                case ActorValue.Intelligence:
                    return TES3Lib.Enums.Attribute.Intelligence;
                case ActorValue.Willpower:
                    return TES3Lib.Enums.Attribute.Willpower;
                case ActorValue.Agility:
                    return TES3Lib.Enums.Attribute.Agility;
                case ActorValue.Speed:
                    return TES3Lib.Enums.Attribute.Speed;
                case ActorValue.Endurance:
                    return TES3Lib.Enums.Attribute.Endurance;
                case ActorValue.Personality:
                    return TES3Lib.Enums.Attribute.Personality;
                case ActorValue.Luck:
                    return TES3Lib.Enums.Attribute.Luck;
                default:
                    return TES3Lib.Enums.Attribute.Strength;             
            }
        }

        public static TES3Lib.Enums.Skill CastSkillToMW(TES4Lib.Enums.Skill skill)
        {
            var rnd = new Random(DateTime.Now.Millisecond).Next(0, 2);
            switch (skill)
            {
                case TES4Lib.Enums.Skill.Armorer:
                    return TES3Lib.Enums.Skill.Armorer;
                case TES4Lib.Enums.Skill.Athletics:
                    return TES3Lib.Enums.Skill.Athletics;
                case TES4Lib.Enums.Skill.Blade:
                    return rnd.Equals(0) ? TES3Lib.Enums.Skill.ShortBlade : TES3Lib.Enums.Skill.LongBlade;
                case TES4Lib.Enums.Skill.Block:
                    return TES3Lib.Enums.Skill.Block;
                case TES4Lib.Enums.Skill.Blunt:
                    return rnd.Equals(0) ? TES3Lib.Enums.Skill.BluntWeapon : TES3Lib.Enums.Skill.Axe;
                case TES4Lib.Enums.Skill.HandToHand:
                    return TES3Lib.Enums.Skill.HandToHand;
                case TES4Lib.Enums.Skill.HeavyArmor:
                    return rnd.Equals(0) ? TES3Lib.Enums.Skill.HeavyArmor : TES3Lib.Enums.Skill.MediumArmor;
                case TES4Lib.Enums.Skill.Alchemy:
                    return TES3Lib.Enums.Skill.Alchemy;
                case TES4Lib.Enums.Skill.Alteration:
                    return TES3Lib.Enums.Skill.Alteration;
                case TES4Lib.Enums.Skill.Conjuration:
                    return TES3Lib.Enums.Skill.Conjuration;
                case TES4Lib.Enums.Skill.Destruction:
                    return TES3Lib.Enums.Skill.Destruction;
                case TES4Lib.Enums.Skill.Illusion:
                    return TES3Lib.Enums.Skill.Illusion;
                case TES4Lib.Enums.Skill.Mysticism:
                    return TES3Lib.Enums.Skill.Mysticism;
                case TES4Lib.Enums.Skill.Restoration:
                    return TES3Lib.Enums.Skill.Restoration;
                case TES4Lib.Enums.Skill.Acrobatics:
                    return TES3Lib.Enums.Skill.Acrobatics;
                case TES4Lib.Enums.Skill.LightArmor:
                    return rnd.Equals(0) ? TES3Lib.Enums.Skill.LightArmor : TES3Lib.Enums.Skill.MediumArmor;
                case TES4Lib.Enums.Skill.Marksman:
                    return TES3Lib.Enums.Skill.Marksman;
                case TES4Lib.Enums.Skill.Mercantile:
                    return TES3Lib.Enums.Skill.Mercantile;
                case TES4Lib.Enums.Skill.Security:
                    return TES3Lib.Enums.Skill.Security;
                case TES4Lib.Enums.Skill.Sneak:
                    return TES3Lib.Enums.Skill.Sneak;
                case TES4Lib.Enums.Skill.Speechcraft:
                    return TES3Lib.Enums.Skill.Speechcraft;
                default:
                    return TES3Lib.Enums.Skill.Unused;
            }
        }

        public static TES3Lib.Enums.CreatureType CastCreatureTypeToMW(TES4Lib.Enums.CreatureType creatureType)
        {
            switch (creatureType)
            {
                case TES4Lib.Enums.CreatureType.Creature:
                    return TES3Lib.Enums.CreatureType.Creature;
                case TES4Lib.Enums.CreatureType.Daedra:
                    return TES3Lib.Enums.CreatureType.Daedra;
                case TES4Lib.Enums.CreatureType.Undead:
                    return TES3Lib.Enums.CreatureType.Undead;
                case TES4Lib.Enums.CreatureType.Humanoid:
                    return TES3Lib.Enums.CreatureType.Humanoid;
                case TES4Lib.Enums.CreatureType.Horse:
                    return TES3Lib.Enums.CreatureType.Creature;
                case TES4Lib.Enums.CreatureType.Giant:
                    return TES3Lib.Enums.CreatureType.Humanoid;
                default:
                    return TES3Lib.Enums.CreatureType.Creature;
            }
        }

        public static TES3Lib.Enums.EnchantmentType CastEnchantmentTypeToMW(TES4Lib.Enums.EnchantmentType enchantmentType)
        {
            switch (enchantmentType)
            {
                case TES4Lib.Enums.EnchantmentType.Scroll:
                    return TES3Lib.Enums.EnchantmentType.CastOnce;
                case TES4Lib.Enums.EnchantmentType.Staff:
                    return TES3Lib.Enums.EnchantmentType.CastOnStrike;
                case TES4Lib.Enums.EnchantmentType.Weapon:
                    return TES3Lib.Enums.EnchantmentType.CastOnStrike;
                case TES4Lib.Enums.EnchantmentType.Apparel:
                    return TES3Lib.Enums.EnchantmentType.ConstantEffect;
                default:
                    return TES3Lib.Enums.EnchantmentType.CastWhenUsed;
            }
        }

        public static TES3Lib.Enums.SpellType CastSpellTypeToMW(TES4Lib.Enums.SpellType spellType)
        {
            switch (spellType)
            {
                case TES4Lib.Enums.SpellType.Spell:
                    return TES3Lib.Enums.SpellType.Spell;
                case TES4Lib.Enums.SpellType.Disease:
                    return TES3Lib.Enums.SpellType.Disease;
                case TES4Lib.Enums.SpellType.Power:
                    return TES3Lib.Enums.SpellType.Power;
                case TES4Lib.Enums.SpellType.LesserPower:
                    return TES3Lib.Enums.SpellType.Power;
                case TES4Lib.Enums.SpellType.Ability:
                    return TES3Lib.Enums.SpellType.Ability;
                case TES4Lib.Enums.SpellType.Poision:
                    return TES3Lib.Enums.SpellType.Spell;
                default:
                    return TES3Lib.Enums.SpellType.Spell;
            }
        }

        public static HashSet<TES3Lib.Enums.Flags.SpellFlag> CastSpellFlagToMW(HashSet<TES4Lib.Enums.Flags.SpellFlag> obFlag)
        {
            var mwFlags = new HashSet<TES3Lib.Enums.Flags.SpellFlag>();

            if (!obFlag.Contains(TES4Lib.Enums.Flags.SpellFlag.ManualSpellCost))
            {
                mwFlags.Add(TES3Lib.Enums.Flags.SpellFlag.AutoCalc);
            }

            if(obFlag.Contains(TES4Lib.Enums.Flags.SpellFlag.PlayerStartSpell))
            {
                mwFlags.Add(TES3Lib.Enums.Flags.SpellFlag.PlayerStartSpell);
            }

            return mwFlags;
        }

        public static HashSet<TES3Lib.Enums.Flags.ServicesFlag> CastServicesToMW(HashSet<TES4Lib.Enums.Flags.ServicesFlag> obFlag)
        {
            var mwFlags = new HashSet<TES3Lib.Enums.Flags.ServicesFlag>();

            foreach (var flag in obFlag)
            {
                switch (flag)
                {
                    case TES4Lib.Enums.Flags.ServicesFlag.Weapons:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Weapon);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Armor:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Armor);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Clothing:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Clothing);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Books:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Books);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Ingredients:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Ingredients);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Lights:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Lights);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Apparatus:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Apparatus);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Misc:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Misc);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Spells:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Spells);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.MagicItems:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.MagicItems);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Potions:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Potions);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Training:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Training);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Recharge:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Spellmaking);
                        break;
                    case TES4Lib.Enums.Flags.ServicesFlag.Repair:
                        mwFlags.Add(TES3Lib.Enums.Flags.ServicesFlag.Repair);
                        break;
                }
            }

            return mwFlags;
        }

        public static HashSet<TES3Lib.Enums.Flags.LightFlag> CastLightFlagsToMW(HashSet<TES4Lib.Enums.Flags.LightFlag> obFlags)
        {
            var mwFlags = new HashSet<TES3Lib.Enums.Flags.LightFlag>();
            foreach (var flag in obFlags)
            {
                switch (flag)
                {
                    case TES4Lib.Enums.Flags.LightFlag.Dynamic:
                        mwFlags.Add(TES3Lib.Enums.Flags.LightFlag.Dynamic);
                        break;
                    case TES4Lib.Enums.Flags.LightFlag.CanBeCarried:
                        mwFlags.Add(TES3Lib.Enums.Flags.LightFlag.CanCarry);
                        break;
                    case TES4Lib.Enums.Flags.LightFlag.Negative:
                        mwFlags.Add(TES3Lib.Enums.Flags.LightFlag.Negative);
                        break;
                    case TES4Lib.Enums.Flags.LightFlag.Flicker:
                        mwFlags.Add(TES3Lib.Enums.Flags.LightFlag.Flicker);
                        break;
                    case TES4Lib.Enums.Flags.LightFlag.OffByDefault:
                        mwFlags.Add(TES3Lib.Enums.Flags.LightFlag.OffDefault);
                        break;
                    case TES4Lib.Enums.Flags.LightFlag.FlickerSlow:
                        mwFlags.Add(TES3Lib.Enums.Flags.LightFlag.FlickerSlow);
                        break;
                    case TES4Lib.Enums.Flags.LightFlag.Pulse:
                        mwFlags.Add(TES3Lib.Enums.Flags.LightFlag.Pulse);
                        break;
                    case TES4Lib.Enums.Flags.LightFlag.PulseSlow:
                        mwFlags.Add(TES3Lib.Enums.Flags.LightFlag.PulseSlow);
                        break;
                }
            }

            return mwFlags;
        }

        public static HashSet<TES3Lib.Enums.Flags.CreatureFlag> CastCreatureFlagsToMW(HashSet<TES4Lib.Enums.Flags.CreatureFlag> obFlags)
        {
            var convertedFlags = new HashSet<TES3Lib.Enums.Flags.CreatureFlag>();

            foreach (var flag in obFlags)
            {
                switch (flag)
                {
                    case TES4Lib.Enums.Flags.CreatureFlag.Biped:
                        convertedFlags.Add(TES3Lib.Enums.Flags.CreatureFlag.Biped);
                        break;
                    case TES4Lib.Enums.Flags.CreatureFlag.Essential:
                        convertedFlags.Add(TES3Lib.Enums.Flags.CreatureFlag.Essential);
                        break;
                    case TES4Lib.Enums.Flags.CreatureFlag.WeaponAndShield:
                        convertedFlags.Add(TES3Lib.Enums.Flags.CreatureFlag.WeaponAndShield);
                        break;
                    case TES4Lib.Enums.Flags.CreatureFlag.Respawn:
                        convertedFlags.Add(TES3Lib.Enums.Flags.CreatureFlag.Respawn);
                        break;
                    case TES4Lib.Enums.Flags.CreatureFlag.Swims:
                        convertedFlags.Add(TES3Lib.Enums.Flags.CreatureFlag.Swims);
                        break;
                    case TES4Lib.Enums.Flags.CreatureFlag.Flies:
                        convertedFlags.Add(TES3Lib.Enums.Flags.CreatureFlag.Flies);
                        break;
                    case TES4Lib.Enums.Flags.CreatureFlag.Walks:
                        convertedFlags.Add(TES3Lib.Enums.Flags.CreatureFlag.Walks);
                        break;
                }
            }
            return convertedFlags;
        }

        public static int GetSoulValue(TES4Lib.Enums.SoulGemType soul)
        {
            switch (soul)
            {
                case SoulGemType.None: // gingers here?
                    return 0;
                case SoulGemType.Petty:
                    return 30;
                case SoulGemType.Lesser:
                    return 60;
                case SoulGemType.Common:
                    return 105;
                case SoulGemType.Greather:
                    return 165;
                case SoulGemType.Grand:
                    return 400;
                default:
                    return 0;
            }
        }


    }
}
