

using BattleRoyale.Data.Models;
using BattleRoyale.Models.Heroes;
using System;
using static BattleRoyale.Data.Constants;
using static BattleRoyale.Data.Constants.HeroConstants;
using static BattleRoyale.Data.Constants.ItemConstants;

namespace BattleRoyale.Services.HeroServices
{
    public class HeroService : IHeroService
    {
        public string GetHeroType(Hero hero)
        {
            if (hero.HeroType.ToString() == Assassin)
            {
                return Assassin;
            }
            else if (hero.HeroType.ToString() == Tank)
            {
                return Tank;
            }
            else if (hero.HeroType.ToString() == Mage)
            {
                return Mage;
            }
            else
            {
                return new InvalidOperationException(InvalidHero).ToString();
            }
        }
        

        public void SetHeroStats(Hero hero)
        {
            var heroType = GetHeroType(hero);

            hero.Level = InitialHeroLevel;
            hero.RequiredExperiencePoints = InitialRequiredExperience;

            if (heroType == Assassin)
            {
                hero.Attack = InitialAssassinAttack;
                hero.MagicAttack = InitialAssassinMagicAttack;
                hero.Health = InitialAssassinHealth;
                hero.Armor = InitialAssassinArmor;
                hero.MagicResistance = InitialAssassinMagicResistance;
                hero.Speed = InitialAssassinSpeed;
            }
            else if (heroType == Tank)
            {
                hero.Attack = InitialTankAttack;
                hero.MagicAttack = InitialTankMagicAttack;
                hero.Health = InitialTankHealth;
                hero.Armor = InitialTankArmor;
                hero.MagicResistance = InitialTankMagicResistance;
                hero.Speed = InitialTankSpeed;
            }
            else if (heroType == Mage)
            {
                hero.Attack = InitialMageAttack;
                hero.MagicAttack = InitialMageMagicAttack;
                hero.Health = InitialMageHealth;
                hero.Armor = InitialMageArmor;
                hero.MagicResistance = InitialMageMagicResistance;
                hero.Speed = InitialMageSpeed;
            }

            hero.OverallPower = hero.Attack + hero.MagicAttack+ hero.Health + hero.Armor + hero.MagicResistance + hero.Speed;

            SetHeroImage(hero);
        }
        public void SetHeroImage(Hero hero)
        {
            var heroType = GetHeroType(hero);

            if (heroType == Assassin)
            {
                hero.ImageUrl = AssassinImage;
            }
            else if (heroType == Tank)
            {
                hero.ImageUrl = TankImage;
            }
            else if (heroType == Mage)
            {
                hero.ImageUrl = MageImage;
            }
        }

        public void EquipItem(Hero hero, Item item)
        {
            if (item.ItemType.ToString() == Weapon)
            {
                hero.Attack += item.Stats;
                hero.HasWeapon = true;
            }
            else if (item.ItemType.ToString() == Necklace)
            {
                hero.MagicAttack += item.Stats;
                hero.HasNecklace = true;
            }
            else if (item.ItemType.ToString() == Armor)
            {
                hero.Armor += item.Stats;
                hero.HasArmorItem = true;
            }
            else if (item.ItemType.ToString() == MagicResistance)
            {
                hero.MagicResistance += item.Stats;
                hero.HasMagicResistItem = true;
            }
            else if (item.ItemType.ToString() == Boots)
            {
                hero.Speed += item.Stats;
                hero.HasBoots = true;
            }
            SetAdditionalEffectFromItem(hero, item);
            item.IsEquipped = true;
            hero.OverallPower += item.Stats;
        }

        public void UnequipItem(Hero hero, Item item)
        {
            if (item.ItemType.ToString() == Weapon)
            {
                hero.Attack -= item.Stats;
                hero.HasWeapon = false;
            }
            else if (item.ItemType.ToString() == Armor)
            {
                hero.Armor -= item.Stats;
                hero.HasArmorItem = false;
            }
            else if (item.ItemType.ToString() == MagicResistance)
            {
                hero.MagicResistance -= item.Stats;
                hero.HasMagicResistItem = false;
            }
            else if (item.ItemType.ToString() == Necklace)
            {
                hero.MagicAttack -= item.Stats;
                hero.HasNecklace = false;
            }
            else if (item.ItemType.ToString() == Boots)
            {
                hero.Speed -= item.Stats;
                hero.HasBoots = false;
            }
            RemoveAdditionalEffectFromItem(hero, item);
            item.IsEquipped = false;
            hero.OverallPower -= item.Stats;
        }

        public void Attack(HeroFightViewModel attacker, HeroFightViewModel defender)
        {
               var remainingArmor = ReturnRemainingArmor(attacker, defender);

               var remainingMagicResistance = ReturnRemainingMagicResistance(attacker, defender);

                if (remainingArmor == 0)
                {
                    defender.RemainingHealth -= attacker.Attack;
                }
                if (remainingMagicResistance == 0)
                {
                    defender.RemainingHealth -= attacker.MagicAttack;
                }

        }

        public void LevelUp(Hero hero)
        {
            var heroType = GetHeroType(hero);

            hero.Level++;

            if(heroType == Assassin)
            {
                hero.Attack += AssassinAttackOnLevelUp;
                hero.MagicAttack += AssassinMagicAttackOnLevelUp;
                hero.Health += AssassinHealthOnLevelUp;
                hero.Armor += AssassinArmorOnLevelUp;
                hero.MagicResistance += AssassinMagicResistanceOnLevelUp;
                hero.Speed += AssassinSpeedOnLevelUp;
            }
            else if (heroType == Tank)
            {
                hero.Attack += TankAttackOnLevelUp;
                hero.MagicAttack += TankMagicAttackOnLevelUp;
                hero.Health += TankHealthOnLevelUp;
                hero.Armor += TankArmorOnLevelUp;
                hero.MagicResistance += TankMagicResistanceOnLevelUp;
                hero.Speed += TankSpeedOnLevelUp;
            }
            else if (heroType == Mage)
            {
                hero.Attack += MageAttackOnLevelUp;
                hero.MagicAttack += MageMagicAttackOnLevelUp;
                hero.Health +=MageHealthOnLevelUp;
                hero.Armor += MageArmorOnLevelUp;
                hero.MagicResistance += MageMagicResistanceOnLevelUp;
                hero.Speed += MageSpeedOnLevelUp;
            }

            hero.OverallPower = hero.Attack + hero.MagicAttack+ hero.Health + hero.Armor + hero.MagicResistance + hero.Speed;
            hero.RequiredExperiencePoints =hero.RequiredExperiencePoints +(int)(hero.RequiredExperiencePoints* AdditionalRequiredExperienceAfterLevelUp);
            
        }

        private int ReturnRemainingArmor(HeroFightViewModel attacker, HeroFightViewModel defender)
        {
            var remainingArmor = defender.RemainingArmor - attacker.Attack;
            if (remainingArmor > 0)
            {
                defender.RemainingArmor = remainingArmor;
            }
            else
            {
                defender.RemainingArmor = 0;
                defender.RemainingHealth -= Math.Abs(remainingArmor);
            }

            return defender.RemainingArmor;
        }

        private int ReturnRemainingMagicResistance(HeroFightViewModel attacker, HeroFightViewModel defender)
        {
            var remainingMagicResistance = defender.RemainingMagicResistance - attacker.MagicAttack;

            if (remainingMagicResistance > 0)
            {
                defender.RemainingMagicResistance = remainingMagicResistance;
            }
            else
            {
                defender.RemainingMagicResistance = 0;
                defender.RemainingHealth -= Math.Abs(remainingMagicResistance);
            }
            return defender.RemainingMagicResistance;
        }

        private void SetAdditionalEffectFromItem(Hero hero,Item item)
        {
            if (item.AdditionalEffect.ToString() == ItemConstants.Attack)
            {
                hero.Attack += AdditionalAttackFromItem;
            }
            else if (item.AdditionalEffect.ToString() == MagicAttack)
            {
                hero.MagicAttack += AdditionalMagicAttackFromItem;
            }
            else if (item.AdditionalEffect.ToString() == Health)
            {
                hero.Health += AdditionalHealthFromItem;
            }
            else if (item.AdditionalEffect.ToString() == Armor)
            {
                hero.Armor += AdditionalArmorFromItem;
            }
            else if (item.AdditionalEffect.ToString() == MagicResistance)
            {
                hero.MagicResistance=AdditionalMagicResistanceFromItem;
            }
            else if (item.AdditionalEffect.ToString() == Speed)
            {
                hero.Speed += AdditionalSpeedFromItem;
            }
        }

        private void RemoveAdditionalEffectFromItem(Hero hero, Item item)
        {
            if (item.AdditionalEffect.ToString() == ItemConstants.Attack)
            {
                hero.Attack -= AdditionalAttackFromItem;
            }
            else if (item.AdditionalEffect.ToString() == MagicAttack)
            {
                hero.MagicAttack -= AdditionalMagicAttackFromItem;
            }
            else if (item.AdditionalEffect.ToString() == Health)
            {
                hero.Health -= AdditionalHealthFromItem;
            }
            else if (item.AdditionalEffect.ToString() == Armor)
            {
                hero.Armor -= AdditionalArmorFromItem;
            }
            else if (item.AdditionalEffect.ToString() == MagicResistance)
            {
                hero.MagicResistance -= AdditionalMagicResistanceFromItem;
            }
            else if (item.AdditionalEffect.ToString() == Speed)
            {
                hero.Speed -= AdditionalSpeedFromItem;
            }
        }
    }
}
