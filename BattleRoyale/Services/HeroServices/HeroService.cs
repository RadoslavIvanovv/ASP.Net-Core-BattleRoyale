

using BattleRoyale.Data.Models;
using BattleRoyale.Models.Heroes;
using System;

namespace BattleRoyale.Services.HeroServices
{
    public class HeroService : IHeroService
    {
        public string GetHeroType(Hero hero)
        {
            if (hero.HeroType.ToString() == "Assassin")
            {
                return "Assassin";
            }
            else if (hero.HeroType.ToString() == "Tank")
            {
                return "Tank";
            }
            else if (hero.HeroType.ToString() == "Mage")
            {
                return "Mage";
            }
            else
            {
                return new InvalidOperationException("Invalid hero type.").ToString();
            }
        }
        

        public void SetHeroStats(Hero hero)
        {
            var heroType = GetHeroType(hero);

            hero.Level = 1;
            hero.RequiredExperiencePoints = 2000;

            if (heroType == "Assassin")
            {
                hero.Attack = 50;
                hero.MagicAttack = 0;
                hero.Health = 500;
                hero.Armor = 30;
                hero.MagicResistance = 20;
                hero.Speed = 100;
            }
            else if (heroType == "Tank")
            {
                hero.Attack = 30;
                hero.MagicAttack = 10;
                hero.Health = 700;
                hero.Armor = 50;
                hero.MagicResistance = 30;
                hero.Speed = 50;
            }
            else if (heroType == "Mage")
            {
                hero.Attack = 20;
                hero.MagicAttack = 50;
                hero.Health = 400;
                hero.Armor = 30;
                hero.MagicResistance = 20;
                hero.Speed = 90;
            }

            hero.OverallPower = hero.Attack + hero.MagicAttack+ hero.Health + hero.Armor + hero.MagicResistance + hero.Speed;

            SetHeroImage(hero);
        }
        public void SetHeroImage(Hero hero)
        {
            var heroType = GetHeroType(hero);

            if (heroType == "Assassin")
            {
                hero.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTfggACYYPQYNnJB-ZzveDW4b1sildTHxHcxg&usqp=CAU";
            }
            else if (heroType == "Tank")
            {
                hero.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcShVcj2klYHeZ1sxhtFjAiUVfliDcpwpxNlr2vE_CkrwQPPuG2Z-1xR_LqtEsrxH4VfDcw&usqp=CAU";
            }
            else if (heroType == "Mage")
            {
                hero.ImageUrl = "https://www.watchmojo.com/uploads/thumbs720/VG-RP-Top10-Wizards-In-VideoGames-720p30.jpg";
            }
        }

        public void EquipItem(Hero hero, Item item)
        {
            if (item.ItemType.ToString() == "Weapon")
            {
                hero.Attack += item.Stats;
                hero.HasWeapon = true;
            }
            else if (item.ItemType.ToString() == "Armor")
            {
                hero.Armor += item.Stats;
                hero.HasArmorItem = true;
            }
            else if (item.ItemType.ToString() == "MagicResistance")
            {
                hero.MagicResistance += item.Stats;
                hero.HasMagicResistItem = true;
            }
            else if (item.ItemType.ToString() == "Necklace")
            {
                hero.MagicAttack += item.Stats;
                hero.HasNecklace = true;
            }
            else if (item.ItemType.ToString() == "Boots")
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
            if (item.ItemType.ToString() == "Weapon")
            {
                hero.Attack -= item.Stats;
                hero.HasWeapon = false;
            }
            else if (item.ItemType.ToString() == "Armor")
            {
                hero.Armor -= item.Stats;
                hero.HasArmorItem = false;
            }
            else if (item.ItemType.ToString() == "MagicResistance")
            {
                hero.MagicResistance -= item.Stats;
                hero.HasMagicResistItem = false;
            }
            else if (item.ItemType.ToString() == "Necklace")
            {
                hero.MagicAttack -= item.Stats;
                hero.HasNecklace = false;
            }
            else if (item.ItemType.ToString() == "Boots")
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

            if(heroType == "Assassin")
            {
                hero.Attack += 50;
                hero.MagicAttack += 0;
                hero.Health += 500;
                hero.Armor += 30;
                hero.MagicResistance += 20;
                hero.Speed += 100;
            }
            else if (heroType == "Tank")
            {
                hero.Attack += 30;
                hero.MagicAttack += 10;
                hero.Health += 700;
                hero.Armor += 50;
                hero.MagicResistance += 30;
                hero.Speed += 50;
            }
            else if (heroType == "Mage")
            {
                hero.Attack += 20;
                hero.MagicAttack += 50;
                hero.Health += 400;
                hero.Armor += 30;
                hero.MagicResistance += 20;
                hero.Speed += 90;
            }

            hero.OverallPower = hero.Attack + hero.MagicAttack+ hero.Health + hero.Armor + hero.MagicResistance + hero.Speed;
            hero.RequiredExperiencePoints =hero.RequiredExperiencePoints +(int)(hero.RequiredExperiencePoints* 0.33);
            
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
            if (item.AdditionalEffect.ToString() == "Attack")
            {
                hero.Attack += 30;
            }
            else if (item.AdditionalEffect.ToString() == "MagicAttack")
            {
                hero.MagicAttack += 20;
            }
            else if (item.AdditionalEffect.ToString() == "Health")
            {
                hero.Health += 50;
            }
            else if (item.AdditionalEffect.ToString() == "Armor")
            {
                hero.Armor += 30;
            }
            else if (item.AdditionalEffect.ToString() == "MagicResistance")
            {
                hero.MagicResistance += 40;
            }
            else if (item.AdditionalEffect.ToString() == "Speed")
            {
                hero.Speed += 30;
            }
        }

        private void RemoveAdditionalEffectFromItem(Hero hero, Item item)
        {
            if (item.AdditionalEffect.ToString() == "Attack")
            {
                hero.Attack -= 30;
            }
            else if (item.AdditionalEffect.ToString() == "MagicAttack")
            {
                hero.MagicAttack -= 20;
            }
            else if (item.AdditionalEffect.ToString() == "Health")
            {
                hero.Health -= 50;
            }
            else if (item.AdditionalEffect.ToString() == "Armor")
            {
                hero.Armor -= 30;
            }
            else if (item.AdditionalEffect.ToString() == "MagicResistance")
            {
                hero.MagicResistance -= 40;
            }
            else if (item.AdditionalEffect.ToString() == "Speed")
            {
                hero.Speed -= 30;
            }
        }
    }
}
